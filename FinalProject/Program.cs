using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    /**
     * A program that demonstrates using a MySQL connection to insert records
     * from the Dataset CSV file into a table.
     * Author: Erik Njolstad
     * Date Modified: 11/10/2017
     */
    public class Program
    {
        private List<DataInfo> list = new List<DataInfo>();
        private DatasetLoader data = new DatasetLoader();
        private MySqlConnection connect;
        private MySqlCommand command;
        private MySqlDataReader reader;
        private string server;
        private string database;
        private string username;
        private string password;
        private string connectString;
        private int dataID;
        private int refDate;
        private string geo;
        private string origin;
        private string vector;
        private string coordinate;
        private string value;
        private const string TableName = "DatasetTable";
        private DataInfo info;
        private bool doesIDExist;

        /**
         * Loads the InitConnection() function from the constructor.
         */
        public Program()
        {
            info = new DataInfo();
            dataID = info.GetDataID();
            refDate = info.GetRefDate();
            geo = info.GetGeo();
            origin = info.GetOrigin();
            vector = info.GetVector();
            coordinate = info.GetCoordinate();
            value = info.GetValue();
            InitConnection();
        }

        /**
         * Creates a connection to the database.
         */
        public void InitConnection()
        {
            // The server name.
            server = "localhost";            

            // The name of the user.
            username = "finalproject";

            // The password.
            password = "password";

            // The name of the database.
            database = "FinalProject";

            // Creates the final project database.
            CreateDatabase();

            // This is used to connect to the MySQL server.
            string connectString = "SERVER=" + server + ";" + "DATABASE=" + database
                + ";" + "USERNAME=" + username + ";" + "PASSWORD=" + password
                + ";";

            connect = new MySqlConnection(connectString);
        }

        /**
         * Begins the process of creating the database.
         */
        public void CreateDatabase()
        {
            MySqlConnection initConnection = new MySqlConnection("Data Source="+server+";Username="+username+";Password="+password+";");
            command = new MySqlCommand("CREATE DATABASE " + database + ";", initConnection);
            try
            {
                initConnection.Open();
                var cmdExists = new MySqlCommand("SELECT COUNT(*) FROM information_schema.schemata WHERE SCHEMA_NAME = '" + database + "';", initConnection);
                using (reader = cmdExists.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        if (count == 0)
                        {
                            reader.Close();
                            command.ExecuteNonQuery();
                            Console.WriteLine(database + " has been created!\n");
                            initConnection.Close();
                            break;
                        }
                        initConnection = null;
                    }
                }
            } catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        /**
         * Creates the Dataset table. If the table already exists, then omit from creating the same table.
         */
        public void CreateTable()
        {
            connect.Open();
            try
            {
                var cmdExists = new MySqlCommand("SELECT COUNT(*) FROM information_schema.tables WHERE " +
                    "table_schema = '" + database + "' AND table_name = '" + TableName + "';", connect);
                using (reader = cmdExists.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        if (count == 0)
                        {
                            // Command line used to create the table with columns.
                            command = new MySqlCommand("CREATE TABLE " + TableName + " ("
                                + "DataID"          + " INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, "
                                + data.GetList()[0] + " INTEGER NOT NULL, "
                                + data.GetList()[1] + " VARCHAR(40), "
                                + data.GetList()[2] + " VARCHAR(150), "
                                + data.GetList()[3] + " VARCHAR(10), "
                                + data.GetList()[4] + " VARCHAR(5), "
                                + data.GetList()[5] + " VARCHAR(12)"
                                + ");", connect);

                            // The reader must be closed before we can execute the query.
                            reader.Close();

                            // Execute the query and create the Dataset Table.
                            command.ExecuteNonQuery();
                            Console.WriteLine(TableName + " has been created!\n");
                            break;
                        }
                    }
                }
            } catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        /**
         * Instantly inserts records into the Dataset Table upon execution. If the values from the
         * CSV file, then omit from re-inserting the same values.
         */
        public void AutoInsertDataToTable()
        {
            try
            {
                // This checks if the Dataset table has records inserted.
                var recordsExist = new MySqlCommand("SELECT COUNT(*) FROM " + TableName, connect);
                using (MySqlDataReader reader = recordsExist.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        if (count == 0)
                        {
                            reader.Close();
                            string[] line = data.GetLines();
                            string[] temp = null;
                            string[] temp2 = null;
                            var result = new string[6];

                            // This line prepares the command to insert the records into the Dataset Table.
                            for (int i = 0; i < data.GetLines().Length; i++)
                            {
                                // These few lines will split and use substrings to omit double quotes and commas
                                // so the records from the CSV file can be used to add to the Dataset Table.
                                temp = line[i].Substring(0, line[i].IndexOf('"') - 1).Split(',');
                                temp2 = Regex.Split(line[i], "\"");
                                refDate = Convert.ToInt32(result[0] = temp[0]);
                                geo = result[1] = temp[1];
                                origin = result[2] = temp2[1];
                                temp2 = Regex.Split(temp2[2], ",");
                                vector = result[3] = temp2[1];
                                coordinate = result[4] = temp2[2];
                                value = result[5] = temp2[3];

                                // Inserts the records while adding the parameters.
                                command = new MySqlCommand("INSERT INTO " + TableName + "(DataID," + data.GetColumnHeader() + ") VALUES (" + dataID + ", " + refDate + ", @geo, @origin, @vector, @coordinate, @value);", connect);
                                command.Parameters.AddWithValue("@geo", geo);
                                command.Parameters.AddWithValue("@origin", origin);
                                command.Parameters.AddWithValue("@vector", vector);
                                command.Parameters.AddWithValue("@coordinate", coordinate);
                                command.Parameters.AddWithValue("@value", value);

                                // Execute the query and have them inserted into the Dataset Table.
                                command.ExecuteNonQuery();
                            }
                            // Writes out the total number of records read and inserted into the table.
                            Console.WriteLine("Total records read: " + data.GetLines().Length + "\nRecords are inserted in " + TableName + "!\n");
                            connect.Close();
                            break;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        /**
         * Retrieves the distinct records from column GEO that are stored from the Dataset table,
         * calculates the total value for each GEO and displays them.
         */
        public MySqlCommand GetDistinctCommand()
        {
            command = new MySqlCommand("SELECT GEO, SUM(value) AS 'Total Value' FROM " + TableName + " WHERE GEO = GEO GROUP BY GEO;", connect);
            return command;
        }

        /**
         * Calls this function specifically for the RemoveData form class to delete a row from the table.
         */
        public MySqlCommand GetDeleteQuery(int dataID)
        {
            this.dataID = dataID;

            command = new MySqlCommand("DELETE FROM " + TableName + " WHERE DataID = " + dataID + ";", connect);
            connect.Open();
            try
            {
                MySqlCommand cmdExist = new MySqlCommand("SELECT * FROM " + TableName + " WHERE DataID = " + dataID + ";", connect);
                try
                {
                    using (MySqlDataReader reader = cmdExist.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                SetBoolValue(true);
                                reader.Close();
                                // Begins query execution of deleting a row from the Dataset Table.
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            SetBoolValue(false);
                        }
                    }
                } catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                connect.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return command;
        }

        /**
         * This function is used for the InsertData form class. It prompts the user to enter information and then
         * inserts all of the values into the Dataset Table.
         */
        public MySqlCommand GetInsertQuery(int dataID, int refDate, string geo, string origin, string vector, string coordinate, string value)
        {
            this.dataID = dataID;
            this.refDate = refDate;
            this.geo = geo;
            this.origin = origin;
            this.vector = vector;
            this.coordinate = coordinate;
            this.value = value;

            command = new MySqlCommand("INSERT INTO " + TableName + "(DataID," + data.GetColumnHeader() + ")"
                + " VALUES (" + dataID + ", " + refDate + ", '" + geo + "', '" + origin + "', '" + vector
                + "', '" + coordinate + "', '" + value + "');", connect);
            connect.Open();
            try
            {
                // Execute the query and have them inserted into the Dataset Table.
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return command;
        }

        /**
         * Returns the connection value.
         */
        public MySqlConnection GetConnection()
        {
            return connect;
        }

        /**
         * Returns the boolean value.
         */
        public bool GetBoolValue()
        {
            return doesIDExist;
        }

        public void SetBoolValue(bool doesIDExist)
        {
            this.doesIDExist = doesIDExist;
        }

        /**
         * Returns the username.
         */
        public string GetUsername()
        {
            return username;
        }

        /**
         * Sets the username.
         */
        public void SetUsername(string username)
        {
            this.username = username;
        }

        /**
         * Returns the password.
         */
        public string GetPassword()
        {
            return password;
        }

        /**
         * Sets the password.
         */
        public void SetPassword(string password)
        {
            this.password = password;
        }

        /**
         * Opens the connection to the database
         */
        private bool OpenConnection()
        {
            try
            {
                connect.Open();
                Console.WriteLine("Connection has been established!");
                return true;
            }

            // This exception is thrown if there is an issue with connecting to
            // the database or the username or password is incorrect.
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to the server. " + ex.Message);
                        Console.ReadKey();
                        break;
                }
                return false;
            }
        }

        /**
         * Closes the connection from the database.
         */
        private bool CloseConnection()
        {
            try
            {
                connect.Close();
                Console.WriteLine("\nConnection has been terminated!\n\nProgram written by: Erik Njolstad");
                Console.ReadKey();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return false;
            }
        }

        /**
         * Insert values to the table or disconnect from the database.
         */
        public void Execute()
        {
            connect.Close();
            if (this.OpenConnection() == true)
            {
                Console.WriteLine("Loading the login screen...\n");
                Application.Run(new Login());
                this.CloseConnection();
            }
        }
    }
}
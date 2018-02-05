using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    /**
     * Creates a form that displays on the right hand side a pie chart based off the province's/country's
     * origin for every potato production. The chart also shows a percentage of the total value of potatoes
     * produced. On the left hand side are options for the user to either insert more data, remove a row or
     * exit the application.
     * Author: Erik Njolstad
     * Date Modified: 30/11/2017
     */
    public partial class CreateForm : Form
    {
        private Form form = new Form();
        private MySqlDataReader reader;
        private Program prog = new Program();
        private List<string> geoList;
        private List<decimal> valueList;
        private InsertData form2 = new InsertData();
        private RemoveData form3 = new RemoveData();

        /**
         * Initializes all of the components and are loaded onto the form.
         */
        public CreateForm()
        {
            InitializeComponent();
        }

        /**
         * The form automatically loads the Dataset chart with options presented to the
         * user to either insert data, remove rows or shutdown the application.
         */
        private void Chart_Activated(object sender, EventArgs e)
        {
            DisplayDataStatistics();
        }

        /**
         * Opens another form which prompts the user to enter a data ID to remove a row
         * from the Dataset Table when clicking the insert data option.
         */
        private void btnInsert_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
            DisplayDataStatistics();
        }

        /**
         * Opens another form which prompts the user to enter a data ID to remove a row
         * from the Dataset Table when clicking the remove row option.
         */
        private void btnRemove_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
            DisplayDataStatistics();
        }

        /**
         * Triggers the application exit function when clicking the shutdown option.
         */
        private void btnShutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void DisplayDataStatistics()
        {
            var connect = prog.GetConnection();
            var distinctCommand = prog.GetDistinctCommand();
            connect.Open();
            reader = distinctCommand.ExecuteReader();
            var info = new DataInfo();
            var list = new List<DataInfo>();
            var storeGeo = "";
            geoList = new List<string>();
            valueList = new List<decimal>();
            this.chart.Legends.Clear();
            while (reader.Read())
            {
                storeGeo = reader.GetString("GEO");
                geoList.Add(storeGeo);
                valueList.Add(Convert.ToDecimal(reader.GetString("Total Value")));
            }
            int index = 0;
            for (int i = 0; i < geoList.Count; i++)
            {
                index = this.chart.Series["Value"].Points.AddXY(geoList[i], valueList[i]);
            }
            // This forces the GEO names to be outside of the pie chart.
            this.chart.Series["Value"]["PieLabelStyle"] = "Outside";

            this.chart.Series["Value"].SmartLabelStyle.Enabled = true;

            // Adds a border to the pie chart and lines pointing to each GEO.
            this.chart.Series["Value"].BorderWidth = 1;
            this.chart.Series["Value"].BorderColor = System.Drawing.Color.FromArgb(50, 50, 100);

            // Adds a new legend where the position of the legend is at the bottom displaying
            // the percentage of values for each GEO.
            this.chart.Legends.Add("Legend");
            this.chart.Legends[0].Enabled = true;
            this.chart.Legends[0].Docking = Docking.Bottom;
            this.chart.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.chart.Series["Value"].LegendText = "#PERCENT{P2}";
            this.chart.DataManipulator.Sort(PointSortOrder.Descending, chart.Series["Value"]);
            ListCleaner(geoList, valueList);
            connect.Close();
        }

        /**
         * Cleans out the geoList and valueList preventing duplicate values.
         */
        private void ListCleaner(List<string> list, List<decimal> value)
        {
            this.geoList = list;
            this.valueList = value;
            list.Clear();
            value.Clear();
        }
    }
}

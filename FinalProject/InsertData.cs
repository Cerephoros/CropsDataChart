using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalProject
{
    /**
     * Creates a form for users to input data. Once the user fills out all of the values inside
     * of the textboxes, they are inserted into the Dataset Table.
     * Author: Erik Njolstad
     * Date Modified: 30/11/2017
     */
    public partial class InsertData : Form
    {
        private CreateForm form;
        private MySqlConnection connect = new MySqlConnection();

        /**
         * Initializes all of the components and are loaded onto the form.
         */
        public InsertData()
        {
            InitializeComponent();
        }

        /**
         * This function fires a clickable event that either tells the user to enter
         * information if more than one field is empty or proceeds with inserting the
         * data to the Dataset Table.
         */
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)
                || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("You must enter a value for all of the fields.");
            }
            else
            {
                form = new CreateForm();
                DataInfo info = new DataInfo();
                Program prog = new Program();
                prog.GetInsertQuery(0, Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
                info.SetRefDate(Convert.ToInt32(textBox1.Text));
                info.SetGeo(textBox2.Text);
                info.SetOrigin(textBox3.Text);
                info.SetVector(textBox4.Text);
                info.SetCoordinate(textBox5.Text);
                info.SetValue(textBox6.Text);
                MessageBox.Show("New data has been added:\n" + info.ToString());
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                this.Close();
            }
        }

        /**
         * This function fires a clickable event that simply closes the form.
         */
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

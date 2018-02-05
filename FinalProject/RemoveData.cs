using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    /**
     * Creates a form that allows the user to remove a row from the Dataset Table.
     * Author: Erik Njolstad
     * Date Modified: 30/11/2017
     */
    public partial class RemoveData : Form
    {
        public RemoveData()
        {
            InitializeComponent();
        }

        /**
         * This function fires a clickable event that prompts the user to enter the dataID
         * from the Dataset Table and a message will populate whether the id exists or not.
         * If it does, then the row will be removed.
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("You must enter a value for DataID");
            }
            else
            {
                DataInfo info = new DataInfo();
                Program prog = new Program();
                prog.GetDeleteQuery(Convert.ToInt32(textBox1.Text));
                var doesIDExist = prog.GetBoolValue();
                info.SetDataID(Convert.ToInt32(textBox1.Text));
                if (doesIDExist)
                {
                    MessageBox.Show("DataID " + info.GetDataID() + " has been removed!");
                    textBox1.Clear();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data ID " + info.GetDataID() + " does not exist in the records.");
                    textBox1.Clear();
                }
            }
        }

        /**
         * This function fires a clickable event that simply closes the form.
         */
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

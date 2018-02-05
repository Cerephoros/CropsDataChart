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
     * Creates a login form where the user must enter the username and password before accessing
     * the Dataset Chart.
     * Author: Erik Njolstad
     * Date Modified: 30/11/2017
     */
    public partial class Login : Form
    {

        private Program prog = new Program();

        public Login()
        {
            InitializeComponent();
        }

        /**
         * This function fires a clickable event that asks the user to enter the username
         * and password. Unless the user enters the correct username and password, the
         * Dataset Chart form will become available.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == prog.GetUsername() && textBox2.Text == prog.GetPassword())
            {
                MessageBox.Show("You have successfully logged in!");
                textBox1.Clear();
                textBox2.Clear();
                this.Hide();
                Console.WriteLine("Loading the content for the dataset chart...");
                CreateForm form2 = new CreateForm();
                form2.ShowDialog();
                this.Close();
            }
            else if (textBox1.Text.Equals(string.Empty) && textBox2.Text.Equals(string.Empty))
            {
                MessageBox.Show("Both fields cannot be empty. Please enter a username and password!");
                textBox1.Clear();
                textBox2.Clear();
            }
            else if (!textBox1.Text.Equals(string.Empty) && textBox2.Text.Equals(string.Empty))
            {
                MessageBox.Show("The password field cannot be empty. Please enter a password!");
                textBox1.Clear();
                textBox2.Clear();
            }
            else if (textBox1.Text.Equals(string.Empty) && !textBox2.Text.Equals(string.Empty))
            {
                MessageBox.Show("The username field cannot be empty. Please enter a username!");
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("The username or password is invalid!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}
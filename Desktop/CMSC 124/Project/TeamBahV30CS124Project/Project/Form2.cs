/*
 * 
 * 
 * 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        Interpreter i;
        String variable_name;

        public Form2(Interpreter i, String variable_name)
        {
            InitializeComponent();
            this.i = i;
            this.variable_name = variable_name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Error: You cannot pass an empty value.");
            }

            else
            {
                this.i.passDataForBasicInput(richTextBox1.Text, variable_name);
                this.Close();
            }
        }
       
    }
}

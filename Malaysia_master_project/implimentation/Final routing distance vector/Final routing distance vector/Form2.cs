using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Final_routing_distance_vector
{
    public partial class Form2 : Form
    {
        private string x;
        public string IntefaceForOutput
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = x;
        }

        
    }
}

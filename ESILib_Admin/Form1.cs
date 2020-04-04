using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESILib_Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Ending The Application
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usr.Text == ESILib_Admin.Properties.Settings.Default.Username && pass.Text == ESILib_Admin.Properties.Settings.Default.Password)
            {
                Main main = new Main();
                main.Show();
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                MessageBox.Show("Wrong Credentials", "The Credentials Hat Have Been Entered Are Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

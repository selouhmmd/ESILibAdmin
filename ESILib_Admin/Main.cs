using Persistence;
using Scripts;
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
    public partial class Main : Form
    {
        // Initialising The Firebase Helper
        FirebaseHelper firebasehelper = new FirebaseHelper();
        public Main()
        {
            InitializeComponent();
        }

        // Getting The Books Details Using ISBN And Filling The Input Boxes
        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                Book bk = Isbn.GetBook(isbn.Text).Result;
                title.Text = bk.Title;
                author.Text = bk.Author;
                desc.Text = bk.Description;
                status.Text = "Available";
                status.ForeColor = Color.Green;

            }
            catch
            {
                status.Text = "Unavailable";
                status.ForeColor = Color.Red;
            }
        }


        
        // Adding The Book To The Online Database
        private async void button4_Click(object sender, EventArgs e)
        {
            await firebasehelper.AddBook(title.Text, author.Text, desc.Text, isbn.Text, (int)numericUpDown1.Value, "");
            MessageBox.Show("Book Added", "The Book Has Beed Added to The Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // Checking The Existance of A Book in The Database Using its ISBN
        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Book bk = await firebasehelper.GetBook(isbnup.Text);
                title1.Text = bk.Title;
                author1.Text = bk.Author;
                desc1.Text = bk.Description;
                status1.Text = "Available";
                status1.ForeColor = Color.Green;

            }
            catch
            {
                status1.Text = "Unavailable";
                status1.ForeColor = Color.Red;
            }
        }


        // Updating The Book Details
        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                await firebasehelper.UpdateBook(isbnup.Text,new Book {Title = title1.Text,Author=author1.Text,Description=desc1.Text,Available=(int)numericUpDown2.Value ,ISBN=isbnup.Text});
                MessageBox.Show("Book Updated", "The Book Has Beed Updated In The Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Book Couldn't Be Updated", "The Book Has Not Beed Updated to The Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        // Getting The List Of All Books That Are On The Database
        private async void button1_Click(object sender, EventArgs e)
        {
            var bkslist = await firebasehelper.GetAllBooks();
            foreach (var bk in bkslist)
            {
                listBox1.Items.Add(bk);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (oldusr.Text == ESILib_Admin.Properties.Settings.Default.Username && oldpass.Text == ESILib_Admin.Properties.Settings.Default.Password && !string.IsNullOrEmpty(newusr.Text) && !string.IsNullOrEmpty(newpass.Text))
            {
                ESILib_Admin.Properties.Settings.Default.Username = newusr.Text;
                ESILib_Admin.Properties.Settings.Default.Password = newpass.Text;
            }
            else
            {
                MessageBox.Show("Cannot Change Credentials","Wrong Credentials Or Empty New Credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}

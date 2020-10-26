using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P03_UserMaintenance.Entities;

namespace P03_UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> Users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();

            label1.Text = Resource1.Fullname;
            
            button1.Text = Resource1.Add;

            listBox1.DataSource = Users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Fullname";


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            User user = new User();

            user.Fullname = textBox1.Text;

            Users.Add(user);

            //textBox1.Clear();
        }
    }
}

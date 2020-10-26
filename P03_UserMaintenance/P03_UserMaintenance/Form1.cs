using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            button2.Text = Resource1.Save;
            button3.Text = Resource1.Delete;
            button4.Text = Resource1.Load;

            listBox1.DataSource = Users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Fullname";


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        

        private void Button1_Click_1(object sender, EventArgs e)
        {
            User user = new User();

            user.Fullname = textBox1.Text;

            Users.Add(user);

            textBox1.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (sf.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sf.FileName, false, Encoding.UTF8))
            {

                foreach (var s in Users)
                {

                    sw.Write(s.ID.ToString());
                    sw.Write(";");
                    sw.Write(s.Fullname);

                    sw.WriteLine();
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            User s = (User)listBox1.SelectedItem;
            Users.Remove(s);
            


            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split(';');

                    User s = new User();




                    try
                    {
                        s.ID = Guid.Parse(sor[0]);
                    }
                    catch
                    { }
                    s.Fullname = sor[1];

                    Users.Add(s);

                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P05_Var.Entities;

namespace P05_Var
{
    public partial class Form1 : Form
    {
        List<Tick> Ticks;
        PortfolioEntities context = new PortfolioEntities();

        List<PortfolioItem> Portfolios = new List<PortfolioItem>();

        public Form1()
        {

            InitializeComponent();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
            CreatePortfolio();
            

        }

        private void CreatePortfolio()
        {
            Portfolios.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolios.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolios.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolios;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

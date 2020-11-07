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
            decimal GetPortfolioValue(DateTime date)
            {
                decimal value = 0;
                foreach (var item in Portfolios)
                {
                    var last = (from x in Ticks
                                where item.Index == x.Index.Trim()
                                   && date <= x.TradingDay
                                select x)
                                .First();
                    value += (decimal)last.Price * item.Volume;
                }
                return value;
            }


            List<decimal> Nyereségek = new List<decimal>();
            int intervalum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - intervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);
            }

            var nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                                        .ToList();
            MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());


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

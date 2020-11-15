using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P02_Soduko
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CratePlayField();
            LoadSusokus();
            InitializeComponent();
            GetRandomQuize();
            NewGame();
        }

        private void NewGame()
        {
            throw new NotImplementedException();
        }

        private void GetRandomQuize()
        {
            throw new NotImplementedException();
        }

        private void LoadSusokus()
        {
            throw new NotImplementedException();
        }

        private void CratePlayField()
        {
            int lineWidth = 5;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; i < 9; j++)
                {
                    SudokuField s = new SudokuField();
                    s.Top = i * 30 + (int)(Math.Floor((double)(i / 3))) * lineWidth;
                    s.Left = j * 30 + (int)(Math.Floor((double)(j / 3))) * lineWidth;
                    mainPanel.Controls.Add(s);
                }
            }
        }
    }
}

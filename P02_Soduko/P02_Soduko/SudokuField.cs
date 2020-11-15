using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P02_Soduko
{
    class SudokuField : Button
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (_value < 0)
                    _value = 9;
                else if (_value > 9)
                    _value = 0;

                if (_value == 0)
                    Text = "";
                else
                    Text = _value.ToString();
            }
        }

        public SudokuField()
        {
            Value = 0;
            Height = 30;
            Width = Height;
            BackColor = Color.White;
            MouseDown += SudokuField_MouseDown;
        }

        private void SudokuField_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _value++;
            }
            if (e.Button == MouseButtons.Left)
            {
                _value--;
            }
        }
    }
}

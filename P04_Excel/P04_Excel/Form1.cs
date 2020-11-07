using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace P04_Excel
{
    public partial class Form1 : Form
    {
        List<Flat> flats;
        FlatEntities context = new FlatEntities();

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül


        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
            FormatTable();
        }

        private void FormatTable()
        {
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, 9));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            int lastRowID = xlSheet.UsedRange.Rows.Count;

            Excel.Range TableRange = xlSheet.get_Range(GetCell(1, 1), GetCell(lastRowID, 9));
            TableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range First = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 1));
            First.Font.Bold = true;
            First.Interior.Color = Color.LightYellow;

            Excel.Range Last = xlSheet.get_Range(GetCell(2, 9), GetCell(lastRowID,9));
            Last.Interior.Color = Color.LightGreen;



            string GetCell(int x, int y)
            {
                string ExcelCoordinate = "";
                int dividend = y;
                int modulo;

                while (dividend > 0)
                {
                    modulo = (dividend - 1) % 26;
                    ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                    dividend = (int)((dividend - modulo) / 26);
                }
                ExcelCoordinate += x.ToString();

                return ExcelCoordinate;
            }
        }

        private void CreateExcel()
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[] {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"};

            for (int i = 0; i < 9; i++)
            {
                xlSheet.Cells[1, i+1] = headers[i];
            }

            object[,] values = new object[flats.Count, headers.Length];

            int c = 0;

            foreach (Flat a in flats)
            {
                values[c, 0] = a.Code;
                values[c, 1] = a.Vendor;
                values[c, 2] = a.Side;
                values[c, 3] = a.District;
                values[c, 4] = a.Elevator;
                values[c, 5] = a.NumberOfRooms;
                values[c, 6] = a.FloorArea;
                values[c, 7] = a.Price;

                values[c, 8] = "=" + GetCell(c+2, 8)+"/"+GetCell(c+2, 7);



                c++;
            }

            string GetCell(int x, int y)
            {
                string ExcelCoordinate = "";
                int dividend = y;
                int modulo;

                while (dividend > 0)
                {
                    modulo = (dividend - 1) % 26;
                    ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                    dividend = (int)((dividend - modulo) / 26);
                }
                ExcelCoordinate += x.ToString();

                return ExcelCoordinate;
            }

            xlSheet.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;


        }

        private void LoadData()
        {
            flats = context.Flats.ToList();
        }
    }
}

using ClosedXML.Excel;
using System;
using System.Data;
using System.Windows.Forms;

namespace AnalisardorCartao
{
    public class ExportacaoExcel
    {
        public void ExportacaoListaExcel(DataGridView dg, string fileName)
        {
            XLWorkbook wb = new XLWorkbook();

            DataTable dt = new DataTable();

            foreach (DataGridViewColumn coluna in dg.Columns)
            {
                if (coluna.DisplayIndex == 11)
                {
                    dt.Columns.Add(coluna.HeaderText, typeof(double));
                }
                else
                {
                    dt.Columns.Add(coluna.HeaderText);
                }
            }

            foreach (DataGridViewRow row in dg.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 11)
                    {
                        if (double.TryParse(cell.Value.ToString(), out _))
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                        }
                    }
                    if (cell.ColumnIndex == 0)
                    {
                        if (DateTime.TryParse(cell.Value.ToString(), out DateTime data))
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = data;
                        }
                        else
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                    }
                    else
                    {
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value == null ? "" : cell.Value.ToString();
                    }
                }
            }

            wb.Worksheets.Add(dt, "Cartões");

            wb.Worksheet(1).Cells("A1:k1").Style.Fill.BackgroundColor = XLColor.LightGray;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string range = string.Format("A{0}:K{0}", i + 2);
                DataRow row = dt.Rows[i];
                if (double.Parse(row[11].ToString()) != 0D)
                {
                    wb.Worksheet(1).Cells(range).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }

            wb.Worksheet(1).Columns().AdjustToContents();

            wb.SaveAs(fileName);
        }
    }
}

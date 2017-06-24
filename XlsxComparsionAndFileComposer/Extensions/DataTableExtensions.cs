using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace XlsxComparsionAndFileComposer.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Export DataTable to Excel file
        /// </summary>
        /// <param name="dataTable">Source DataTable</param>
        /// <param name="excelFilePath">Path to result file name</param>
        public static void ExportToExcel(this DataTable dataTable, string excelFilePath = null)
        {
            try
            {
                int columnsCount;

                if (dataTable == null || (columnsCount = dataTable.Columns.Count) == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Application Excel = new Application();
                Excel.Workbooks.Add();

                // single worksheet
                _Worksheet worksheet = Excel.ActiveSheet;

                object[] header = new object[columnsCount];

                // column headings               
                for (var i = 0; i < columnsCount; i++)
                    header[i] = dataTable.Columns[i].ColumnName;

                var headerRange = worksheet.Range[(Range)(worksheet.Cells[1, 1]), (Range)(worksheet.Cells[1, columnsCount])];
                headerRange.Value = header;
                headerRange.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                headerRange.Font.Bold = true;

                // DataCells
                var rowsCount = dataTable.Rows.Count;
                object[,] cells = new object[rowsCount, columnsCount];

                for (var j = 0; j < rowsCount; j++)
                for (var i = 0; i < columnsCount; i++)
                    cells[j, i] = dataTable.Rows[j][i];

                worksheet.Range[(Range)(worksheet.Cells[2, 1]), (Range)(worksheet.Cells[rowsCount + 1, columnsCount])].Value = cells;

                // check fielpath
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        worksheet.SaveAs(excelFilePath);
                        Excel.Quit();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    Excel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    }
}

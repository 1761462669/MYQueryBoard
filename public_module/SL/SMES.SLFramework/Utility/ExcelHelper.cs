using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Utility
{
    public static class ExcelHelper
    {
        public static void ExportExcel(XamGrid grid)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel Workbook (.xlsx)|*.xlsx";

            if (saveDialog.ShowDialog().Value)
            {
                try
                {
                    Stream stream = saveDialog.OpenFile();

                    Workbook exportedWorkbook = ExportWorkbook(grid);
                    exportedWorkbook.Save(stream);
                    stream.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private static Workbook ExportWorkbook(XamGrid grid)
        {
            XamGridExcelExporter export = new XamGridExcelExporter();

            // Create a Workbook object and add a sheet in it.
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets.Add("Sheet1");
            workbook.SetCurrentFormat(WorkbookFormat.Excel2007);

            export.Export(grid, workbook);

            return workbook;
        }
    }
}

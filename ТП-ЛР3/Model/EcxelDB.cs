using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TP_LR3.Model
{
    class ExcelDB
    {
        static public StatisticalData GetDataFromExcel(string dataName, string path)
        {
            StatisticalData data = FindClassByName(dataName);
            data.CreateStatistic(); 

            using (var workbook = new XLWorkbook(path)) // Открыть Excel файл
            {
                // Выбор листа в соответствии с названием класса
                var worksheet = workbook.Worksheet(dataName);
                // Размеры листа
                var range = worksheet.RangeUsed();
                int rowCount = range.RowCount(); // Кол-во строк в выбранном листе
                int columnCount = range.ColumnCount(); // Кол-во столбцов

                foreach (Specifications specification in data.StatisticData)
                { // Для всех типов данных в data
                    for (int column = 1; column <= columnCount; column++)
                    { // Среди всех стобцов в файле Excel
                        if (specification.GetType().Name == worksheet.Cell(1, column).Value.ToString())
                        { // Искать данный тип данных
                            for (int row = 2; row <= rowCount; row++)
                            { // И тогда добавить все данные данного типа
                                specification.Set(worksheet.Cell(row, column).Value);
                            }
                            break;
                        }
                    }
                }
            }
            return data;
        }
        public static StatisticalData FindClassByName(string type) // Поиск класса по названию
        {
            switch (type) // Дополнять при увеличении кол-ва классов
            {
                case "InflationData":
                    return new InflationData();
                case "MigrationData":
                    return new MigrationData();
                case "SheetData":
                    return new SheetData();
            }
            return null;
        }
        public static string GetFilePath ()
        {
            string filePath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string pathNow = Directory.GetCurrentDirectory();
                pathNow = pathNow.Remove(pathNow.Length - 32);
                openFileDialog.InitialDirectory = pathNow;

                openFileDialog.Filter = "Excel Files|*.xls;*xlsx;*.xlsm";
                //openFileDialog.FilterIndex = 2;
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }
            return filePath;
        }
    }
}

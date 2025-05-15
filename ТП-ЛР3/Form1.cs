using TP_LR3.Model;

namespace TP_LR3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Test();
        }

        //Функция для настройки формы при загрузке
        public static void Initialization()
        {

        }
        public static void Test()
        {
            string path = ExcelDB.GetFilePath(); // Получаем путь до файла
            
            //string selectedDataType = "InflationData";
            //string selectedDataType = "MigrationData";
            string selectedDataType = "SheetData";

            StatisticalData statisticalData = ExcelDB.GetDataFromExcel(selectedDataType, path); // Считывание с Excel

            MessageBox.Show(statisticalData.ToString());
            MessageBox.Show(string.Join(" ", (List<float>)statisticalData.StatisticData[1].Get()));
            //dataGridView1.DataSource = statisticalData;
            //dataGridView1.DataSource = DataTableConverter.GetDataTable(statisticalData);
        }
    }
}

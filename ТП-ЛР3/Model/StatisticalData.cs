using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_LR3.Model
{
    public abstract class StatisticalData // Общий вид данных
    {
        public List<Specifications> StatisticData { get; set; }
        public abstract void CreateStatistic(); // Создание списка данных для анализа
        public override string ToString()
        {
            string data = "";
            foreach (Specifications specification in StatisticData)
                data += specification.ToString() + "\n";
            return data;
        }
    }
    public class InflationData : StatisticalData // Данные по инфляции
    {
        public override void CreateStatistic()
        {
            StatisticData = new List<Specifications>
            {
                new Date(), // Дата получения данных
                new Inflation() // Значение инфляции на эту дату
            };
        }
    }
    public class MigrationData : StatisticalData // Данные по миграции
    {
        public override void CreateStatistic()
        {
            StatisticData = new List<Specifications>
            {
                new Date(), // Дата получения данных
                new Immigrants(),
                new Emigrants()
            };
        }
    }
    public class SheetData : StatisticalData
    {
        public override void CreateStatistic()
        {
            StatisticData = new List<Specifications>
            {
                new Date(),
                new Currency_USD(),
                new Currency_EUR()
            };
        }
    }
}

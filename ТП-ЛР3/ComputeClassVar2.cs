using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_LR3
{
    public class Statistics
    {
        public DateTime dateTime;

        public float Rub_To_USD_change;
        public float Rub_To_EU_change;

        public Statistics(float Rub_To_USD_change, float Rub_To_EU_change, DateTime dateTime)
        {
            this.Rub_To_USD_change = Rub_To_USD_change;
            this.Rub_To_EU_change = Rub_To_EU_change;
            this.dateTime = dateTime;
        }
    }
    public class ComputeClassVar2
    {
        public List<DateTime> dateTimes;

        public List<float> Rub_To_USD_Сourse;
        public List<float> Rub_To_EU_Сourse;

        public List<Statistics> statistics = [];

        public void ComputeChange()
        {
            statistics[0].Rub_To_USD_change = 0;
            statistics[0].Rub_To_EU_change = 0;
            int max_index = dateTimes.Count;
            for (int i = 1; i < max_index; i++)
            {
                var usd_change = Rub_To_USD_Сourse[i] - Rub_To_USD_Сourse[i - 1];
                var eu_change = Rub_To_EU_Сourse[i] - Rub_To_EU_Сourse[i - 1];
                var date = dateTimes[i];

                Statistics stat = new(usd_change, eu_change, date);
                statistics.Add(stat);
            }
        }

        public void MovingAverageForecast(int monthsAhead, int windowSize)
        {
            for (int i = 0; i < monthsAhead; i++)
                MovingAverageMethod(windowSize);
        }


        public void MovingAverageMethod(int windowSize)
        {
            var first_index = statistics.Count - windowSize;
            var last_index = statistics.Count;

            DateTime NextDate = dateTimes[dateTimes.Count - 1].Date.AddMonths(1);
            var avg_USD = 0f;
            var avg_EU = 0f;

            for (int i = first_index; i < last_index; i++)
            {
                avg_USD += Rub_To_USD_Сourse[i];
                avg_EU += Rub_To_EU_Сourse[i];
            }

            avg_USD /= (windowSize);
            avg_EU /= (windowSize);

            Rub_To_USD_Сourse.Add(avg_USD);
            Rub_To_EU_Сourse.Add(avg_EU);
            dateTimes.Add(NextDate);

            var usd_change = Rub_To_USD_Сourse[last_index] - Rub_To_USD_Сourse[last_index - 1];
            var eu_change = Rub_To_EU_Сourse[last_index] - Rub_To_EU_Сourse[last_index - 1];
            
            Statistics stat = new(usd_change, eu_change, NextDate);
            statistics.Add(stat);
        }

        public ComputeClassVar2(List<float> Rub_To_USD_Сourse, List<float> Rub_To_EU_Сourse, List<DateTime> dateTimes)
        {
            this.Rub_To_USD_Сourse = Rub_To_USD_Сourse;
            this.Rub_To_EU_Сourse = Rub_To_EU_Сourse;
            this.dateTimes = dateTimes;
        }
    }
}

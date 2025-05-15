using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_LR3
{
    //Класс для хранения статистики
    public class Migration_Statistics
    {
        //Год
        public DateTime dateTime;

        //Процент изменения иммиграции
        public float Migration_Procent_Change;

        //Численное изменение за этот год мигрантов/эмигрантов
        public float Emigrants_Change;
        public float Immigrants_Change;

        //Изменение в населении с учетом мигрантов и эммигрантов (в сравнении с предыдущем годом)
        public float Difference;

        public Migration_Statistics()
        {
            Migration_Procent_Change = 0f;
            Emigrants_Change = 0f;
            Immigrants_Change = 0f;
            Difference = 0f;
        }

        //Конструктор для добавления методом экстраполяции
        public Migration_Statistics(float immigrant_change, float emigrant_change, DateTime date_time, float difference, float percent)
        {
            Emigrants_Change = immigrant_change;
            Immigrants_Change = emigrant_change;
            Difference = difference;
            Migration_Procent_Change = percent;
        }
    }

    //Основной класс (6 вариант)
    public class Migration
    {
        //Эммигранты
        public List<float> ? Emigrants;

        //Иммигранты
        public List<float> ? Immigrants;

        //Список дат (годов)
        public List<DateTime> ? dateTimes;

        //Список со статистикой
        public List<Migration_Statistics> statistics;

        //Рассчет статистики (может использоваться для вывода исходной статистики, полученной из файла)
        public void ComputeChange()
        {
            //Очищаем список статистики перед обновлением исходных данных
            statistics.Clear();

            //Подсчитываем количество дат для составления статистики
            int max_index = dateTimes.Count;

            if (dateTimes.Count > 0)
            {
                //Создаем объект класса статистики по умолчанию на 0-вом индексе
                Migration_Statistics statistic = new Migration_Statistics();
                statistics[0] = statistic;

                //Задаем начальные данные по умолчанию (проверяем не null ли они)
                //тыс.чел
                float average_imgrants = Immigrants != null && Immigrants.Any() ? Immigrants.Average() : 0f;
                float average_emigrants = Emigrants != null && Emigrants.Any() ? Emigrants.Average() : 0f;

                statistics[0].Emigrants_Change = average_emigrants;
                statistics[0].Immigrants_Change = average_imgrants;

                //Высчитываем разность
                statistics[0].Difference = statistics[0].Immigrants_Change - statistics[0].Emigrants_Change;

                statistics[0].dateTime = dateTimes[1].AddYears(-1);

                statistics[0].Migration_Procent_Change = 0;
            }
            else
            {
                return;
            }

            //Начинаем заполнение с индекса 1, чтобы учесть миграционный процент для 1 года (он рассчитывается значением по умолчанию)
            for (int i = 1; i < max_index + 1; i++)
            {
                //Создаем новый объект класса статистики
                Migration_Statistics statistic = new Migration_Statistics();
                statistics[i] = statistic;

                statistics[i].Emigrants_Change = Emigrants[i];
                statistics[i].Immigrants_Change = Immigrants[i];
                statistics[i].Difference = statistics[i].Immigrants_Change - statistics[i].Immigrants_Change;

                statistics[i].dateTime = dateTimes[i];

                //Высчитываем процент изменения миграции за год (на основании прошлого года)
                statistics[i].Migration_Procent_Change = 100 * (statistics[i].Difference - statistics[i - 1].Difference) / statistics[i - 1].Difference;
            }
        }

        //Задать число просчетов статистики скользящим методом (на последующие N (yearsAhead) лет)
        public void Year_MovingAverageForecast(int yearsAhead, int windowSize)
        {
            for (int i = 0; i < yearsAhead; i++)
                Year_MovingAverageMethod(windowSize);
        }

        //Метод скользящей средней (для статического прогнозирования)
        public void Year_MovingAverageMethod(int windowSize)
        {
            //Просчитываем на основе скольки предыдущих лет делать статистику
            var first_index = statistics.Count - windowSize;
            var last_index = statistics.Count;


            var avg_immigrants = 0f;
            var avg_emigrants = 0f;

            //Вычисляем сумму всех значений в заданные года
            for (int i = first_index; i < last_index; i++)
            {
                avg_immigrants += statistics[i].Immigrants_Change;
                avg_immigrants += statistics[i].Emigrants_Change;
            }

            //Считаем средние значения
            avg_immigrants /= (windowSize);
            avg_emigrants /= (windowSize);

            //Считаем следующую дату (год)
            DateTime NextDate = statistics[last_index - 1].dateTime.AddYears(1);

            //Считаем значения для нового экземпляра (записи) статистики
            var difference = avg_immigrants - avg_emigrants;
            var percent = 100 * (difference - statistics[last_index - 1].Difference) / statistics[last_index - 1].Difference;

            //Создаем еще 1 объект статистики (слепок)
            Migration_Statistics stat = new Migration_Statistics(avg_immigrants, avg_emigrants, NextDate, difference, percent);
            statistics.Add(stat);
        }

        public Migration()
        {
            //Создаем списки
            Emigrants = [];
            Immigrants = [];
            dateTimes = [];
            statistics = [];
        }
    }
}

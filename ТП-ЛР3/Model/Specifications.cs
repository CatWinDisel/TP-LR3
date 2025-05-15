using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_LR3.Model
{
    public abstract class Specifications
    { // Абстракный класс для всех типов получаемых данных для анализа
        public abstract Object Get();
        public abstract void Set(Object s);
    }
    public class Date : Specifications // Дата 
    {
        private List<DateTime> date;
        public Date() { this.date = new List<DateTime>(); }
        public override Object Get()
        {
            return date;
        }
        public override void Set(Object date)
        {
            this.date.Add(Convert.ToDateTime(date.ToString()));
        }
        public override string ToString()
        {
            string mode = "";
            switch ("y-m")
            {
                case "y":
                    mode = "yyyy";
                    break;
                case "y-m":
                    mode = "yyyy-MM";
                    break;
            }
            string text = "";
            foreach (DateTime dt in date)
            {
                text += dt.ToString(mode) + " ";
            }
            return text;
        }
    }
    public class Inflation : Specifications // Данные по инфляции
    {
        private List<float> inflation;
        public Inflation() { this.inflation = new List<float>(); }
        public override object Get()
        {
            return inflation;
        }
        public override void Set(object inflation)
        {
            this.inflation.Add(float.Parse(inflation.ToString()));
        }
        public override string ToString()
        {
            return string.Join(" ", inflation);
        }
    }
    public class Immigrants : Specifications // Данные по инфляции
    {
        private List<float> immigrant;
        public Immigrants() { this.immigrant = new List<float>(); }
        public override object Get()
        {
            return immigrant;
        }
        public override void Set(object immigrant)
        {
            this.immigrant.Add(float.Parse(immigrant.ToString()));
        }
        public override string ToString()
        {
            return string.Join (" ", immigrant);
        }
    }
    public class Emigrants : Specifications // Данные по инфляции
    {
        private List<float> emigrant;
        public Emigrants() { this.emigrant = new List<float>(); }
        public override object Get()
        {
            return emigrant;
        }
        public override void Set(object emigrant)
        {
            this.emigrant.Add(float.Parse(emigrant.ToString()));
        }
        public override string ToString()
        {
            return string.Join (" ", emigrant);
        }
    }
    public class Currency_USD : Specifications // Данные по инфляции
    {
        private List<float> сurrency_USD;
        public Currency_USD() { this.сurrency_USD = new List<float>(); }
        public override object Get()
        {
            return сurrency_USD;
        }
        public override void Set(object сurrency_USD)
        {
            this.сurrency_USD.Add(float.Parse(сurrency_USD.ToString()));
        }
        public override string ToString()
        {
            return string.Join(" ", сurrency_USD);
        }
    }
    public class Currency_EUR : Specifications // Данные по инфляции
    {
        private List<float> сurrency_EUR;
        public Currency_EUR() { this.сurrency_EUR = new List<float>(); }
        public override object Get()
        {
            return сurrency_EUR;
        }
        public override void Set(object сurrency_EUR)
        {
            this.сurrency_EUR.Add(float.Parse(сurrency_EUR.ToString()));
        }
        public override string ToString()
        {
            return string.Join(" ", сurrency_EUR);
        }
    }
}

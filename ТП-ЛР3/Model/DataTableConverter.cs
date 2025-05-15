using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TP_LR3.Model
{
    public class DataTableConverter
    {
        public static DataTable GetDataTable (StatisticalData statisticalData)
        {
            DataTable dt = new DataTable ();
            foreach (Specifications specification in statisticalData.StatisticData)
            {
                dt.Columns.Add(specification.GetType().Name);
                dt.Rows.Add(specification.Get());
            }
            return dt;
        }
    }
}

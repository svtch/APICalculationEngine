using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICalculationEngine.Models
{
    public class CalculatedMetric
    {
        public string Devicemacaddress;
        public string Calculated_Metric_Value;
        public DateTime DateStart;
        public DateTime DateEnd;
        public int Metric_Calculation_Type_ID;
    }
}
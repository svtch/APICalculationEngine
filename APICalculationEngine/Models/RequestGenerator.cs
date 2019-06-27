using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICalculationEngine.Models
{
    public class RequestGenerator
    {
        public string buildSelectRequest(string macaddress, DateTime dateDebut, DateTime dateFin, int calculationtype)
        {
            return "SELECT * FROM `calculated_metric` WHERE Metric_Calculation_Type_ID = '" + calculationtype +
                    "' AND Device_macaddress = '" + macaddress +
                    "' AND DateStart BETWEEN '" + dateDebut + "' AND '" + dateFin +
                    "' AND DateEnd BETWEEN '" + dateDebut + "' AND '" + dateFin;
        }
    }
}
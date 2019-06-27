using APICalculationEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICalculationEngine.Gestion
{
    public class DataGenerator
    {
        public List<CalculatedMetric> list;
        public DataGenerator(int number)
        {
            Random rnd = new Random();
            list = new List<CalculatedMetric>();
            
            for (int i = 0; i < number; i++)
            {
                int nb_deb;
                int nb_fin;
                nb_deb = rnd.Next(1, 50);
                nb_fin = rnd.Next(1, 50);
                while (nb_deb > nb_fin)
                {
                    nb_deb = rnd.Next(1, 200);
                    nb_fin = rnd.Next(1, 200);
                }
                
                CalculatedMetric cm = new CalculatedMetric();
                cm.DateEnd = MethodesGlobales.GoodDateAdd(DateTime.Now, nb_fin);
                cm.DateStart = MethodesGlobales.GoodDateAdd(DateTime.Now, nb_deb);
                cm.Devicemacaddress = GetRandomMacAddress();
                cm.Metric_Calculation_Type_ID = 1;
                cm.Calculated_Metric_Value = rnd.Next(1,20).ToString();
                list.Add(cm);
            }
        }
        public string GetRandomMacAddress()
        {
            var random = new Random();
            var buffer = new byte[6];
            random.NextBytes(buffer);
            var result = String.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
            return result.TrimEnd(':');
        }

    }
}
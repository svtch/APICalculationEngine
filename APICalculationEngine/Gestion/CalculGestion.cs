using APICalculationEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICalculationEngine.Gestion
{
    public class CalculGestion
    {
        string[] characters;
        DateTime startTime;
        DateTime endTime;
        List<CalculatedMetric> list_final;
        List<CalculatedMetric> list_base2;
        public List<CalculatedMetric> getRealCalculatedMetrics(List<CalculatedMetric> list_base, string step , DateTime dateDebut, DateTime dateFin, int calcType)
        {
            characters = step.Split(':');
            startTime = dateDebut;
            list_final = new List<CalculatedMetric>();
            list_base2 = (from m in list_base where (m.DateStart >= dateDebut && m.DateEnd <= dateFin) select m).ToList();
            endTime = startTime;
            RemoveValuesOutOfInterval(list_base2);
            switch (characters[1].ToString())
            {
                case "d": // jour
                    while (list_base2.Count != 0 && endTime < dateFin)
                    {
                        //Creation de l'intervalle
                        endTime = startTime.AddDays(int.Parse(characters[0].ToString()));
                        var metrics = (from m in list_base2 where (m.DateStart >= startTime && m.DateEnd <= endTime) select m).ToList();
                        //On calcule les metrics
                        if (metrics.Count > 0)
                        {
                            CalculAndRemove(calcType, metrics);
                        }
                        startTime = endTime;
                    }
                    break;
                case "m": // minute
                    while (list_base2.Count != 0 && endTime<dateFin)
                    {
                        //Creation de l'intervalle
                        endTime = startTime.AddMinutes(int.Parse(characters[0].ToString()));
                        var metrics = (from m in list_base2 where (m.DateStart >= startTime && m.DateEnd <= endTime) select m).ToList();
                        //On calcule les metrics
                        if (metrics.Count > 0)
                        {
                            CalculAndRemove(calcType, metrics);
                        }
                        startTime = endTime;
                    }
                    break;
                case "y": // année
                    while (list_base2.Count != 0 && endTime < dateFin)
                    {
                        //Creation de l'intervalle
                        endTime = startTime.AddYears(int.Parse(characters[0].ToString()));
                        var metrics = (from m in list_base2 where (m.DateStart >= startTime && m.DateEnd <= endTime) select m).ToList();
                        //On calcule les metrics
                        if (metrics.Count > 0)
                        {
                            CalculAndRemove(calcType, metrics);
                        }
                        startTime = endTime;
                    }
                    break;
                case "M": // mois
                    while (list_base2.Count != 0 && endTime < dateFin)
                    {
                        //Creation de l'intervalle
                        endTime = startTime.AddMonths(int.Parse(characters[0].ToString()));
                        var metrics = (from m in list_base2 where (m.DateStart >= startTime && m.DateEnd <= endTime) select m).ToList();
                        //On calcule les metrics
                        if (metrics.Count > 0)
                        {
                            CalculAndRemove(calcType, metrics);
                        }
                        startTime = endTime;
                    }
                    break;
                case "h": // heure
                    while (list_base2.Count != 0 && endTime < dateFin)
                    {
                        //Creation de l'intervalle
                        endTime = startTime.AddHours(int.Parse(characters[0].ToString()));
                        var metrics = (from m in list_base2 where (m.DateStart >= startTime && m.DateEnd <= endTime) select m).ToList();
                        //On calcule les metrics
                        if (metrics.Count > 0)
                        {
                            CalculAndRemove(calcType, metrics);
                        }
                        startTime = endTime;
                    }
                    break;
            }
            return list_final;
        }

        private void CalculAndRemove(int calcType, List<CalculatedMetric> metrics)
        {
            list_final.Add(ApplyCalculation(metrics, calcType));
            foreach (CalculatedMetric m in metrics)
            {
                list_base2.RemoveAll(x => x.Devicemacaddress == m.Devicemacaddress && x.DateStart == m.DateStart);
            }
        }

        public CalculatedMetric ApplyCalculation(List<CalculatedMetric> metrics, int calcType)
        {
            CalculatedMetric cmFinal = new CalculatedMetric();
            switch (calcType)
            {
                case 1:
                    int longueur = metrics.Count();
                    int total = 0;
                    foreach(CalculatedMetric m in metrics)
                    {
                        total = total + int.Parse(m.Calculated_Metric_Value);
                    }
                    
                    cmFinal.Calculated_Metric_Value = (total / longueur).ToString();
                    cmFinal.Devicemacaddress = longueur.ToString();
                    break;
            }
            return cmFinal;
        }

        public void RemoveValuesOutOfInterval(List<CalculatedMetric> list)
        {
            var metricsNoInterval = (from m in list_base2 where ((m.DateEnd - m.DateStart).TotalMinutes > int.Parse(characters[0].ToString())) select m).ToList();
            if (metricsNoInterval.Count > 0)
            {
                foreach (CalculatedMetric m in metricsNoInterval)
                {
                    list_base2.RemoveAll(x => x.Devicemacaddress == m.Devicemacaddress && x.DateStart == m.DateStart);
                }

            }
        }
    }
}
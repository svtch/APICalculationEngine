using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICalculationEngine.Gestion
{
    public static class MethodesGlobales
    {
        public static DateTime GoodDateAdd(DateTime d, int nbtoadd)
        {
            return d.AddDays(nbtoadd);
        }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using APICalculationEngine.Models;
using APICalculationEngine.Gestion;

namespace APICalculationEngine.Controllers
{
    public class CalculatedMetricController : ApiController
    {
        public Boolean IsTest = true;
        public MySqlConnection connection;
        public string BDDChain = Properties.Settings.Default.BDDChain;
        List<CalculatedMetric> list_calc;

        // GET: api/CalculatedMetric
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CalculatedMetric/5
        //string macaddress, DateTime dateDebut, DateTime dateFin, int calculationtype, string step
        public List<CalculatedMetric> Get(string macaddress, DateTime dateDebut, DateTime dateFin, int calculationtype, string step)
        {
            CalculGestion cg = new CalculGestion();
            if (IsTest)
            {
                DataGenerator dg = new DataGenerator(15000);
                list_calc = dg.list;
                return cg.getRealCalculatedMetrics(list_calc, step, DateTime.Now, MethodesGlobales.GoodDateAdd(DateTime.Now, 50), calculationtype);
            }
            else
            {
                GetDataFromDB(macaddress, dateDebut, dateFin, calculationtype);
                return cg.getRealCalculatedMetrics(list_calc, step, dateDebut, dateFin, calculationtype);
            }
   
            /*list_calc.Add(new CalculatedMetric
            {
                Calculated_Metric_Value = "25",
                DateStart = DateTime.Now.AddMinutes(2),
                DateEnd = DateTime.Now.AddMinutes(7)
            }
                );*/
        }

        // POST: api/CalculatedMetric
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CalculatedMetric/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CalculatedMetric/5
        public void Delete(int id)
        {
        }
        public Boolean DBConnexion()
        {
            try
            {
                connection = new MySqlConnection(BDDChain);
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetDataFromDB(string macaddress, DateTime dateDebut, DateTime dateFin, int calculationtype)
        {
            DBConnexion();
            try
            {
                RequestGenerator rg = new RequestGenerator();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = rg.buildSelectRequest(macaddress, dateDebut, dateFin, calculationtype);
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CalculatedMetric cm = new CalculatedMetric();
                    cm.DateEnd = DateTime.ParseExact(reader["DateEnd"].ToString(), "yyyy-MM-dd HH:mm tt", null);
                    cm.DateStart = DateTime.ParseExact(reader["DateStart"].ToString(), "yyyy-MM-dd HH:mm tt", null);
                    cm.Devicemacaddress = reader["Devicemacaddress"].ToString();
                    cm.Metric_Calculation_Type_ID = Convert.ToInt32(reader["Metric_Calculation_Type_ID"].ToString());
                    cm.Calculated_Metric_Value = reader["Calculated_Metric_Value"].ToString();
                    list_calc.Add(cm);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

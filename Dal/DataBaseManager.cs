using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Dal
{
    public class DataBaseManager
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        

        public DataBaseManager(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            connectionString = configuration.GetConnectionString("DBContext");
        }

        internal void Add(ArduinoData data)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
             
                string CallStoredProcedure = "CALL addtodb(@temperature,@humidity)";
                NpgsqlCommand cmd = new NpgsqlCommand(CallStoredProcedure, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@temperature", data.Temperature);
                cmd.Parameters.AddWithValue("@humidity", data.Humidity);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

           
        }

        internal List<ArduinoData> GetData()
        {
            List<ArduinoData> datalist = new List<ArduinoData>();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                string CallStoredProcedure = "SELECT * FROM datacollection";

                NpgsqlCommand cmd = new NpgsqlCommand(CallStoredProcedure, con);
                cmd.CommandType = CommandType.Text;
               
                con.Open();
                NpgsqlDataReader pgreader = cmd.ExecuteReader();

                while (pgreader.Read())
                {
                    //double dTemperature = (double)pgreader["temperature"];
                    //double dhumidity = (double)pgreader["humidity"];

                    //ArduinoData data = new ArduinoData
                    //{
                    //    Temperature = (float)dTemperature,
                    //    Humidity = (float)dhumidity,
                    //    Log = (DateTime)pgreader["log"]
                    //};


                    ArduinoData data = new ArduinoData
                    {
                        Temperature = (double)pgreader["temperature"],
                        Humidity = (double)pgreader["humidity"],
                        Log = (DateTime)pgreader["log"]
                    };


                    datalist.Add(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


            // List<ArduinoData> data = tem
            return datalist;
        }
    }
}

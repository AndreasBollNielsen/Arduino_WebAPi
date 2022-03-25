using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class ArduinoData
    {
        private double temperature;
        private double humidity;
        private DateTime log;

        public double Temperature { get => temperature; set => temperature = value; }
        public double Humidity { get => humidity; set => humidity = value; }
        public DateTime Log { get => log; set => log = value; }
    }
}

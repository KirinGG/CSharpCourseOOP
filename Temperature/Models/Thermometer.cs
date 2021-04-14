using System;

namespace Temperature.Models
{
    enum MeasurementSystems
    {
        Fahrenheit = 0,
        Celsius = 1,
        Kelvin = 2
    }

    class Thermometer
    {
        MeasurementSystems initialMeasurementSystem;
        MeasurementSystems calculatedMeasurementSystem;
        double temperatureValue;

        public Thermometer(double temperatureValue = 0, MeasurementSystems initialMeasurementSystem = MeasurementSystems.Celsius, MeasurementSystems calculatedMeasurementSystem = MeasurementSystems.Celsius)
        {
            this.temperatureValue = temperatureValue;
            this.initialMeasurementSystem = initialMeasurementSystem;
            //this.calculatedMeasurementSystem = (MeasurementSystems)Enum.Parse(typeof(MeasurementSystems), calculatedMeasurementSystem);
            this.calculatedMeasurementSystem = calculatedMeasurementSystem;
        }

        public string[] GetMeasurementSystems()
        {
            var count = Enum.GetValues(typeof(MeasurementSystems)).Length;
            var measurementSystems = new string[count];

            /*foreach(var item in MeasurementSystems)
            {
                measurementSystems
            }*/
            return null;
        }

        public double Calculate()
        {
            return 0;
        }
    }
}

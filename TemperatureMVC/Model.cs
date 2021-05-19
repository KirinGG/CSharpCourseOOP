using System;

namespace Temperature
{
    enum MeasurementUnits
    {
        Fahrenheit = 0,
        Celsius = 1,
        Kelvin = 2
    }

    class Model
    {
        public double Temperature { get; set; }

        public MeasurementUnits From { get; set; }

        public MeasurementUnits To { get; set; }

        public Model(MeasurementUnits from, MeasurementUnits to, double temperature)
        {
            Temperature = temperature;
            From = from;
            To = to;
        }

        public double GetTemperature()
        {
            if (From == To)
            {
                return Temperature;
            }

            var celsiusTemperature = ConverToCelsius(Temperature, From);

            return ConverFromCelsius(celsiusTemperature, To);
        }

        public string[] GetMeasurementUnits()
        {
            return Enum.GetNames(typeof(MeasurementUnits));
        }

        private static double ConverToCelsius(double temperature, MeasurementUnits measurementUnit)
        {
            if (measurementUnit == MeasurementUnits.Fahrenheit)
            {
                return 5.0 / 9.0 * (temperature - 32);
            }

            if (measurementUnit == MeasurementUnits.Kelvin)
            {
                return temperature - 273.15;
            }

            return temperature;
        }

        private static double ConverFromCelsius(double temperature, MeasurementUnits measurementUnit)
        {
            if (measurementUnit == MeasurementUnits.Fahrenheit)
            {
                return 9.0 / 5.0 * temperature + 32;
            }

            if (measurementUnit == MeasurementUnits.Kelvin)
            {
                return temperature + 273.15;
            }

            return temperature;
        }
    }
}

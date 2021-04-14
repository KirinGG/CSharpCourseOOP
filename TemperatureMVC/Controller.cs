using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temperature
{
    class Controller
    {
        Model model;
        View view;

        public Controller(View view)
        {
            model = new Model(MeasurementUnits.Celsius, MeasurementUnits.Fahrenheit, 1);
            this.view = view;
        }

        public void CalculatedTemperature()
        {
            view.RefreshView(model.GetTemperature());
        }

        public void UpdateModel(string from, string to, double temperature)
        {
            model.From = (MeasurementUnits)Enum.Parse(typeof(MeasurementUnits), from);
            model.To = (MeasurementUnits)Enum.Parse(typeof(MeasurementUnits), to);
            model.Temperature = temperature;
        }

        public void SetFrom(string measurementUnit)
        {
            model.From = (MeasurementUnits)Enum.Parse(typeof(MeasurementUnits), measurementUnit);
        }

        public void SetTo(string measurementUnit)
        {
            model.To = (MeasurementUnits)Enum.Parse(typeof(MeasurementUnits), measurementUnit);
        }

        public void SetValue(double temperature)
        {
            model.Temperature = temperature;
        }

        public void InitializeComponent()
        {
            view.InitializeComboBoxFrom(model.GetMeasurementUnits());
            view.InitializeComboBoxTo(model.GetMeasurementUnits());
        }
    }
}

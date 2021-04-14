using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temperature
{
    public partial class View : Form, IView
    {
        Controller controller;

        public View()
        {
            InitializeComponent();
            controller = new Controller(this);
            controller.InitializeComponent();
        }

        public void InitializeComboBoxFrom(string[] items)
        {
            foreach (var item in items)
            {
                comboBoxFrom.Items.Add(item);
            }
        }

        public void InitializeComboBoxTo(string[] items)
        {
            foreach (var item in items)
            {
                comboBoxTo.Items.Add(item);
            }
        }

        public void RefreshView(double temperature)
        {
            textBoxResult.Text = temperature.ToString();
        }

        public void SetFrom(string measurementUnit)
        {
            controller.SetFrom(measurementUnit);
        }

        public void SetTo(string measurementUnit)
        {
            controller.SetTo(measurementUnit);
        }

        public void SetValue(double temperature)
        {
            controller.SetValue(temperature);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (comboBoxFrom.SelectedItem == null)
            {
                textBoxResult.Text = "The unit of measurement from which the temperature is translated cannot be null!";
                return;
            }

            if (comboBoxTo.SelectedItem == null)
            {
                textBoxResult.Text = "The unit of measurement in which the temperature is translated cannot be null!";
                return;
            }

            bool isNumerical = double.TryParse(textBoxValue.Text, out double temperature);

            if (!isNumerical)
            {
                textBoxResult.Text = "The temperature must be a number!";
                return;
            }

            controller.CalculatedTemperature();
        }

        private void comboBoxFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFrom.SelectedItem == null)
            {
                //throw new ArgumentNullException(nameof(comboBoxFrom.SelectedItem), "The unit of measurement from which the temperature is translated cannot be null!");
                textBoxResult.Text = "The unit of measurement from which the temperature is translated cannot be null!";
                return;
            }

            SetFrom(comboBoxFrom.SelectedItem.ToString());
        }

        private void comboBoxTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTo.SelectedItem == null)
            {
                //throw new ArgumentNullException(nameof(comboBoxFrom.SelectedItem), "The unit of measurement in which the temperature is translated cannot be null!");
                textBoxResult.Text = "The unit of measurement in which the temperature is translated cannot be null!";
                return;
            }

            SetTo(comboBoxTo.SelectedItem.ToString());
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            double temperature;
            bool isNumerical = double.TryParse(textBoxValue.Text, out temperature);

            if (!isNumerical)
            {
                //throw new ArgumentException("The temperature must be a number!", nameof(textBoxValue));
                textBoxResult.Text = "The temperature must be a number!";
                return;
            }

            SetValue(temperature);
        }
    }
}

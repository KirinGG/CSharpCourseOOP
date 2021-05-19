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
        private Controller controller;

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
            textBoxResult.ForeColor = Color.Black;

            if (comboBoxFrom.SelectedItem == null)
            {
                textBoxResult.Text = "The unit of measurement from which the temperature is translated cannot be null!";
                textBoxResult.ForeColor = Color.Red;
                return;
            }

            if (comboBoxTo.SelectedItem == null)
            {
                textBoxResult.Text = "The unit of measurement in which the temperature is translated cannot be null!";
                textBoxResult.ForeColor = Color.Red;
                return;
            }

            bool isNumerical = double.TryParse(textBoxValue.Text, out var temperature);

            if (!isNumerical)
            {
                textBoxResult.Text = "The temperature must be a number!";
                textBoxResult.ForeColor = Color.Red;
                return;
            }

            controller.CalculatedTemperature();
        }

        private void comboBoxFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxResult.ForeColor = Color.Black;

            if (comboBoxFrom.SelectedItem == null)
            {
                textBoxResult.Text = "The unit of measurement from which the temperature is translated cannot be null!";
                textBoxResult.ForeColor = Color.Red;
                return;
            }

            SetFrom(comboBoxFrom.SelectedItem.ToString());
        }

        private void comboBoxTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxResult.ForeColor = Color.Black;

            if (comboBoxTo.SelectedItem == null)
            {
                textBoxResult.Text = "The unit of measurement in which the temperature is translated cannot be null!";
                textBoxResult.ForeColor = Color.Red;
                return;
            }

            SetTo(comboBoxTo.SelectedItem.ToString());
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            double temperature;
            bool isNumerical = double.TryParse(textBoxValue.Text, out temperature);
            textBoxResult.ForeColor = Color.Black;

            if (!isNumerical)
            {
                textBoxResult.Text = "The temperature must be a number!";
                textBoxResult.ForeColor = Color.Red;
                return;
            }

            SetValue(temperature);
        }
    }
}

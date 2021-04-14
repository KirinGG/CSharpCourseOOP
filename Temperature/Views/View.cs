using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Temperature.Controllers;
using Temperature.Views;

namespace Temperature.Views
{
    public partial class View : Form
    {
        Controller controller = new Controller();

        public View()
        {
            InitializeComponent();
            comboBoxFrom.Items.Add("1");
        }

        private void textBoxTempereture_TextChanged(object sender, EventArgs e)
        {
            controller.Temperature = Convert.ToDouble(textBoxTempereture.Text);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            controller.Calculate();
        }
    }
}

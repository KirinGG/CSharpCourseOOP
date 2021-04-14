using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Temperature.Models;
using Temperature.Views;

namespace Temperature.Controllers
{
    class Controller
    {
        private Thermometer thermometerModel;
        private View view;

        public double Temperature { get; set; }

        public Controller()
        {
            thermometerModel = new Thermometer();
        }

        public void Calculate()
        {
            //
        }
    }
}

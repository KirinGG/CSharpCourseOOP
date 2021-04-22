using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperUI
{
    public partial class ViewGame : Form
    {
        public ViewGame()
        {
            InitializeComponent();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewSetting viewSetting = new ViewSetting();
            viewSetting.Show();
        }
    }
}

using DemographicSimulator.Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemographicSimulator
{
    public partial class DataForm : Form
    {
        public DataForm(MapConfiguration mc)
        {
            InitializeComponent();
            birthBox.Text = mc.Birthrate.ToString();
            devLvlBox.Text = mc.DevelopmentLevel.ToString();
            tempBox.Text = mc.AvgTemperature.ToString();
            heiBox.Text = mc.AvgHeight.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}

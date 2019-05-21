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
        MapConfiguration mc;
        public DataForm(MapConfiguration mc)
        {
            InitializeComponent();
            birthBox.Text = mc.Birthrate.ToString();
            devLvlBox.Text = mc.DevelopmentLevel.ToString();
            tempBox.Text = mc.AvgTemperature.ToString();
            heiBox.Text = mc.AvgHeight.ToString();
            this.mc = mc;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(birthBox.Text, out double birth))
            {
                MessageBox.Show("Birthrate value is not floating point number", "Error", MessageBoxButtons.OK);
                return;
            }
            if (!int.TryParse(devLvlBox.Text, out int dev))
            {
                MessageBox.Show("Development value is not an integer", "Error", MessageBoxButtons.OK);
                return;
            }
            if (!double.TryParse(tempBox.Text, out double temp))
            {
                MessageBox.Show("Temperature value is not floating point number", "Error", MessageBoxButtons.OK);
                return;
            }
            if (!double.TryParse(heiBox.Text, out double hei))
            {
                MessageBox.Show("Height value is not floating point number", "Error", MessageBoxButtons.OK);
                return;
            }
            mc.Birthrate = birth;
            mc.DevelopmentLevel = dev;
            mc.AvgTemperature = temp;
            mc.AvgHeight = hei;
            Visible = false;
        }
    }
}

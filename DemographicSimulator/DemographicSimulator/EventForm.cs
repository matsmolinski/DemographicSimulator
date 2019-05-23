using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemographicSimulator.Events;
using DemographicSimulator.Simulator;

namespace DemographicSimulator
{
    public partial class EventForm : Form
    {
        public IGameEvent ev = null;
        public int power;
        public MapObjects.Point p;
        public MainControler mc;
        public MainWindow mw;

        public EventForm(MainControler mc, MainWindow mw)
        {
            InitializeComponent();
            eventList.Items.AddRange(new object[] { "Drought", "Earthquake", "Fire", "Flood", "Wind"});
            eventList.SelectedItem = "Drought";
            eventList.DropDownStyle = ComboBoxStyle.DropDownList;
            XTextBox.Text = "0";
            YTextBox.Text = "0";
            powerTextBox.Text = "100";
            p = new MapObjects.Point(0, 0);
            this.mc = mc;
            this.mw = mw;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(XTextBox.Text, out int x))
            {
                MessageBox.Show("X value is not proper integer", "Error", MessageBoxButtons.OK);
                return;
            }
            if (!int.TryParse(YTextBox.Text, out int y))
            {
                MessageBox.Show("Y value is not proper integer", "Error", MessageBoxButtons.OK);
                return;
            }
            if (x < 0 || y < 0 || x > 600 || y > 500)
            {
                MessageBox.Show("Point out of range", "Error", MessageBoxButtons.OK);
                return;
            }
            p.X = x;
            p.Y = y;
            if (!int.TryParse(powerTextBox.Text, out power))
            {
                MessageBox.Show("Power value is not proper integer", "Error", MessageBoxButtons.OK);
                return;
            }
            if (power < 0 || power > 100)
            {
                MessageBox.Show("Power out of range", "Error", MessageBoxButtons.OK);
                return;
            }
            switch (eventList.SelectedItem)
            {
                case "Drought":
                    ev = new Drought();
                    break;
                case "Earthquake":
                    ev = new Earthquake();
                    break;
                case "Fire":
                    ev = new Fire();
                    break;
                case "Flood":
                    ev = new Flood();
                    break;
                case "Wind":
                    ev = new Wind();
                    break;
                default:
                    MessageBox.Show("Event name not recognised", "Error", MessageBoxButtons.OK);
                    return;
            }
            mc.ForceEvent(ev, p, power);
            Visible = false;
            mw.RefreshCityDataBox();
        }
    }
}

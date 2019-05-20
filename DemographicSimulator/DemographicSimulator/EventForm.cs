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
    public partial class EventForm : Form
    {
        public EventForm()
        {
            InitializeComponent();
            eventList.Items.AddRange(new object[] { "Drought", "Earthquake", "Fire", "Flood", "Wind"});
            eventList.SelectedItem = "Drought";
            eventList.DropDownStyle = ComboBoxStyle.DropDownList;
            XTextBox.Text = "0";
            YTextBox.Text = "0";
            powerTextBox.Text = "100";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}

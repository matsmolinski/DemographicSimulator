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
    public partial class MainWindow : Form
    {
        private System.Windows.Forms.TrackBar trackBar1;

        public MainWindow()
        {
            InitializeComponent();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.Controls.AddRange(new System.Windows.Forms.Control[] {this.trackBar1 });

            this.trackBar1.Location = new System.Drawing.Point(760, 80);
            this.trackBar1.Size = new System.Drawing.Size(100, 45);
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);

            trackBar1.Maximum = 30;
            trackBar1.TickFrequency = 5;
            trackBar1.LargeChange = 3;
            trackBar1.SmallChange = 2;

            DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            System.Console.WriteLine(trackBar1.Value);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

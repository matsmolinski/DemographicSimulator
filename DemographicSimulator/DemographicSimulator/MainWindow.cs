using DemographicSimulator.DataParser;
using DemographicSimulator.Events;
using DemographicSimulator.MapObjects;
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
    public partial class MainWindow : Form
    {
        private readonly MainControler mc;
        private readonly TrackBar trackBar1;

        public MainWindow()
        {
            mc = new MainControler();
            InitializeComponent();
            gamePanel.Paint += new PaintEventHandler(GamePanel_Paint);

            mc.Map.ContourLines.Add(new Line(10, 10, 300, 10));
            mc.Map.ContourLines.Add(new Line(300, 10, 300, 300));
            mc.Map.ContourLines.Add(new Line(300, 300, 200, 400));
            mc.Map.ContourLines.Add(new Line(200, 400, 10, 350));
            mc.Map.ContourLines.Add(new Line(10, 10, 10, 350));

            mc.Map.Cities.Add(new City(new MapObjects.Point(50, 50), 10000));
            mc.Map.Cities.Add(new City(new MapObjects.Point(200, 200), 10000));

            Line[] segs = new Line[2];
            segs[0] = new Line(150, 10, 150, 100);
            segs[1] = new Line(150, 100, 200, 150);
            River river = new River(segs);
            mc.Map.Rivers.Add(river);
            trackBar1 = new TrackBar();
            Controls.AddRange(new Control[] {trackBar1});

            trackBar1.Location = new System.Drawing.Point(760, 80);
            trackBar1.Size = new Size(100, 45);
            trackBar1.Scroll += new EventHandler(TrackBar1_Scroll);

            trackBar1.Maximum = 30;
            trackBar1.TickFrequency = 5;
            trackBar1.LargeChange = 3;
            trackBar1.SmallChange = 2;
            Parser.readData("anyPath");
            DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void TrackBar1_Scroll(object sender, System.EventArgs e)
        {
            Console.WriteLine(trackBar1.Value);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            foreach(Line l in mc.Map.ContourLines)
            {
                PaintContour(g, l);
            }
            foreach (River r in mc.Map.Rivers)
            {
                PaintRiver(g, r);
            }
            foreach (City c in mc.Map.Cities)
            {
                PaintCity(g, c.point);
            }
            /*Random rnd = new Random();
            int i = rnd.Next(0, 11);
            if (i > 5)
            {
                PaintCity(g, new MapObjects.Point(500, 100));
                //PaintRiver(g, new Line(210, 210, 100, 50));
                PaintContour(g, new Line(10, 110, 110, 100));
            }
               
            else
            {
                PaintCity(g, new MapObjects.Point(300, 300));
               // PaintRiver(g, new Line(10, 10, 30, 50));
                PaintContour(g, new Line(111, 111, 222, 222));
            }*/
        }

        private void PaintCity(Graphics g, MapObjects.Point p)
        {
            g.FillEllipse(new SolidBrush(Color.Red), p.X, p.Y, 10, 10);
        }

        private void PaintRiver(Graphics g, River r)
        {
            for(int i = 0; i <r.riverSegments.Length; i++)
            {
                Line seg = r.riverSegments[i];
                g.DrawLine(new Pen(Color.Blue, 3), seg.Points[0].X, seg.Points[0].Y,
                    seg.Points[1].X, seg.Points[1].Y);
            }
            
        }

        private void PaintContour(Graphics g, Line l)
        {
            g.DrawLine(new Pen(Color.BlanchedAlmond, 3), l.Points[0].X, l.Points[0].Y, l.Points[1].X, l.Points[1].Y);
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (mc.IsSimulationOn)
            {
                button1.BackgroundImage = Properties.Resources.playbtn;
                mc.IsSimulationOn = false;
                gamePanel.Refresh();
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.pausebtn;
                mc.IsSimulationOn = true;
                gamePanel.Refresh();
            }
                
        }

        private void AddEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Drought dr = new Drought();
            mc.ForceEvent(dr);
        }
    }
}
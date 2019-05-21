using DemographicSimulator.DataParser;
using DemographicSimulator.Events;
using DemographicSimulator.MapObjects;
using DemographicSimulator.Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemographicSimulator
{
    public partial class MainWindow : Form
    {
        private readonly MainControler mc;
        private readonly TrackBar trackBar1;
        private Thread simulationThread;
        private City chosenCity = null;

        public MainWindow()
        {
            mc = new MainControler();
            InitializeComponent();
            gamePanel.Paint += new PaintEventHandler(GamePanel_Paint);
            trackBar1 = new TrackBar();
            Controls.AddRange(new Control[] {trackBar1});

            trackBar1.Location = new System.Drawing.Point(760, 80);
            trackBar1.Size = new Size(100, 45);
            trackBar1.Scroll += new EventHandler(TrackBar1_Scroll);
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 12;
            trackBar1.TickFrequency = 1;
            trackBar1.LargeChange = 2;
            trackBar1.SmallChange = 1;
            trackBar1.Enabled = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            label2.Text=DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;           
            foreach (River r in mc.Map.Rivers)
            {
                PaintRiver(g, r);
            }
            foreach (City c in mc.Map.Cities)
            {
                PaintCity(g, c);
            }
            foreach (Line l in mc.Map.ContourLines)
            {
                PaintContour(g, l);
            }
        }

        private void PaintCity(Graphics g, City c)
        {
            g.FillEllipse(new SolidBrush(Color.Red), c.point.X, c.point.Y, 10, 10);
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
            if(mc.IsSimulationOn)
            {
                mc.IsSimulationOn = false;
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataForm dataWindow = new DataForm(mc.Map.mc)
            {
                Visible = true
            };
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (mc.IsSimulationOn)
            {
                button1.BackgroundImage = Properties.Resources.playbtn;
                mc.IsSimulationOn = false;               
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.pausebtn;
                mc.IsSimulationOn = true;
                simulationThread = new Thread(new ThreadStart(Run))
                {
                    IsBackground = true
                };
                simulationThread.Start();
            }
                
        }
// TUTAJ
        private void AddEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventForm eventWindow = new EventForm(mc)
            {
                Visible = true
            };            
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To start the simulation, press start button. \nTo pause the simulation, press pause button.\n" +
                "To force an event, use the \"Add event\" option on the menu bar. \nYou can upload your own file at any moment, using File>Load option.",
                "Help");
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Project Demographic Simulator \n Author: Mateusz Smoliński", "About");
        }

        private void LoadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            if(mc.IsSimulationOn)
            {
                Button1_Click(null, null);
            }          
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
               // openFileDialog.InitialDirectory = "/~";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    try
                    {
                        mc.Map = Parser.ReadData(filePath, out List<string> fb);
                        if(fb.Count != 0)
                        {
                            string message = "";
                            foreach(string s in fb)
                            {
                                message = message + s + "\n";
                            }
                            MessageBox.Show(message, "Warning", MessageBoxButtons.OK);
                        }
                        label2.Text = DateTime.Now.ToString("dd.MM.yyyy");
                        trackBar1.Enabled = true;
                        cityDataBox.Enabled = true;
                        addEventToolStripMenuItem.Enabled = true;
                        globalToolStripMenuItem.Enabled = true;
                        button1.Enabled = true;
                        gamePanel.Refresh();
                        chosenCity = null;
                        RefreshCityDataBox();
                    }
                    catch(ParseFileException er)
                    {
                        MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK);
                    }
                    
                }
            }
        }

        public void Run()
        {

            while (true)
            {
                if(!mc.IsSimulationOn)
                {
                    break;
                }
                int sliderValue = 6;
                trackBar1.Invoke(new Action(delegate ()
                {
                    sliderValue = trackBar1.Value;
                }));               
                mc.MakeTimeJump(sliderValue);
                label2.Invoke(new Action(delegate ()
                {
                    label2.Text = AddToDate(sliderValue);
                }));
                cityDataBox.Invoke(new Action(delegate ()
                {
                    RefreshCityDataBox();
                }));
                Thread.Sleep(1000);
            }
        }

        private void RefreshCityDataBox()
        {
            if (chosenCity == null)
            {
                cityDataBox.Text = "";
            }
            else
            {
                cityDataBox.Text = chosenCity.name + "\nPopulation: " + chosenCity.Population +
                    "\nAvg. temperature: " + mc.Map.mc.AvgTemperature + "°C\n" +
                    "Distance to river: " + chosenCity.CityData.DistanceToRiver + " km\n" +
                    "Point: "+ chosenCity.point.ToString();
            }
        }

        private string AddToDate(int sliderValue)
        {
            string currentData = label2.Text;
            string[] dateElements = currentData.Split('.');
            int.TryParse(dateElements[0], out int day);
            int.TryParse(dateElements[1], out int month);
            int.TryParse(dateElements[2], out int year);
            month += sliderValue;
            if(month > 12)
            {
                month -= 12;
                year++;
            }
            if (day > 28)
            {
                day = 28;
            }
            return day + "." + (month < 10 ? "0" : "") + month + "." + year;

        }

        private void GamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            bool chosen = false;
            foreach (City c in mc.Map.Cities)
            {
                if (Math.Abs(e.X - c.point.X) < 10 && Math.Abs(e.Y - c.point.Y) < 10)
                {
                    chosenCity = c;
                    chosen = true;
                }
            }
            if (!chosen)
            {
                chosenCity = null;
            }
            RefreshCityDataBox();
        }
    }
}
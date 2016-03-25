﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //VARIABLES
        //different constants
        const int POINT_SIZE = 5;
        
        //global scope variables
        Point top_left_coord = new Point();
        Point top_right_coord = new Point();
        Point bottom_left_coord = new Point();
        Point bottom_right_coord = new Point();

        int window_width = Form1.ActiveForm.Width;
        int window_height = Form1.ActiveForm.Height;

        Dictionary<int, int> points_coordinates = new Dictionary<int, int>();


        //CUSTOM FUNCTIONS

        private void DrawPoint(int coordinate_x, int coordinate_y)
        {
            //define variables for point drawing on form
            System.Drawing.Graphics _graphics = this.CreateGraphics();
            System.Drawing.Rectangle _ellipse = new System.Drawing.Rectangle(coordinate_x, 
                coordinate_y, POINT_SIZE, POINT_SIZE);
            Brush _brush = System.Drawing.Brushes.Black;

            //drawing point
            _graphics.FillEllipse(_brush, _ellipse);

            //catch if there is an equals points, then rewrite it in the dictionary
            try
            {
                points_coordinates.Add(coordinate_x, coordinate_y);
            }
            catch (System.ArgumentException)
            {
                points_coordinates.Remove(coordinate_x);
                points_coordinates.Add(coordinate_x, coordinate_y);
            }
            
        }

        private void DrawCoordinateGrid() 
        {
            System.Drawing.Graphics _graphics = this.CreateGraphics();
            Pen line_pen = new Pen(Color.Black, 2);

            //vertical line
            Point vertical_top = new Point(window_width / 2, 0);
            Point vertical_bottom = new Point(window_width/2, window_height);
            _graphics.DrawLine(line_pen, vertical_top, vertical_bottom);

            //horizontal line (substract 20 pixels 'cause of some error in values)
            Point horizontal_left = new Point(0, window_height/2 - 20);
            Point horizontal_right = new Point(window_width, window_height/2 - 20);
            _graphics.DrawLine(line_pen, horizontal_left, horizontal_right);
        }


        //DEFAULT FUNCTIONS

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            DrawPoint(e.X, e.Y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            window_height = ActiveForm.Height;
            window_width = ActiveForm.Width;
            this.Refresh();
            DrawCoordinateGrid();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}

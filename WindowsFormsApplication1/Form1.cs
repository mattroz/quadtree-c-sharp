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

        int window_width = Form1.ActiveForm.Width;
        int window_height = Form1.ActiveForm.Height;

        Point left_top_coord;
        Point right_bottom_coord;

        Quadtree root;

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

        //draw grid to show entity of quadtree
        private void DrawGrid(Point left_top, Point right_bottom) 
        {
            System.Drawing.Graphics _graphics = this.CreateGraphics();
            Pen line_pen = new Pen(Color.Black, 2);

            //vertical line coordinates
            Point vertical_top = new Point(
                    left_top.X + (right_bottom.X - left_top.Y) / 2, 
                    0
                );

            Point vertical_bottom = new Point(
                    vertical_top.X, 
                    right_bottom.Y
                );
            _graphics.DrawLine(line_pen, vertical_top, vertical_bottom);

            //horizontal line coordinates (substract 20 pixels 'cause of some error in values)
            Point horizontal_left = new Point(
                    left_top.X,
                    left_top.Y + (right_bottom.Y - left_top.Y) / 2
                );
            Point horizontal_right = new Point(
                    right_bottom.X,
                    right_bottom.Y / 2
                );
            _graphics.DrawLine(line_pen, horizontal_left, horizontal_right);
        }


        //DEFAULT FUNCTIONS

        public Form1()
        {
            InitializeComponent(); 
            left_top_coord = new Point(0, 0);
            right_bottom_coord = new Point(window_width, window_height);
        }

        //redraw grid on resize
        private void Form1_Resize(object sender, EventArgs e)
        {
            window_height = ActiveForm.Height;
            window_width = ActiveForm.Width;
            left_top_coord = new Point(0, 0);
            right_bottom_coord = new Point(window_width, window_height);

            this.Refresh();
            DrawGrid(left_top_coord, right_bottom_coord);
        }



        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DrawPoint(e.X, e.Y);
                Point point = new Point(e.X, e.Y);
                root.Insert(point);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            root = new Quadtree(left_top_coord, right_bottom_coord);        //init quadtree
            MessageBox.Show("Root bounds are " + root.RightBottomBound.X + ";" + root.RightBottomBound.Y);
        }
    }
}

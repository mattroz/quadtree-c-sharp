﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



public class Quadtree
{
    //constructor
    public Quadtree(Point _left_bound, Point _right_bound, WindowsFormsApplication1.Form1 _form)
	{
        this.LeftTopBound = _left_bound;
        this.RightBottomBound = _right_bound;
        this.form = _form;
        this.points = new List<Point>();
	}


    ////////////////////////////    CLASS FIELDS   /////////////////////////////////

    #region
    private const int DEFAULT_MAX_OBJ = 2;

    WindowsFormsApplication1.Form1 form = null;

    private int maximum_objects = 2;
    private int number_of_children = 0;
    private List<Point> points;

    private Point left_top_bound;          //bounds are two points: top left corner of an rectangle and bottom right corner
    private Point right_bottom_bound;      //consequently, this is rectangle diagonale line in common
    
    Quadtree parent;
    Quadtree north_west;
    Quadtree north_east;
    Quadtree south_west;
    Quadtree south_east;
    #endregion

    /////////////////////////////   CLASS METHODS   /////////////////////////////

    #region
    //setters/getters

    public int NumberOfChildren
    {
        set { this.number_of_children = value ;}
        get { return this.number_of_children; }
    }


    public int MaximumObjects    
    {
        set { this.maximum_objects = value; }
        get { return this.maximum_objects; }
    }


    public Point LeftTopBound 
    {
        set { this.left_top_bound = value; }
        get { return this.left_top_bound; }
    }


    public Point RightBottomBound
    {
        set { this.right_bottom_bound = value; }
        get { return this.right_bottom_bound; }
    }


    public Quadtree Parent 
    {
        set { this.parent = value; }
        get { return this.parent; }
    }

    //calculate methods

    //check if this quadrant includes current point
    public bool Includes(Point _point) { 
        if( (_point.X > this.LeftTopBound.X && _point.X < this.RightBottomBound.X) &&
            (_point.Y > this.LeftTopBound.Y && _point.Y < this.RightBottomBound.Y) ){
                return true;
        }
        return false;
    }

    //basic Quadtree methods

    public void Subdivide() 
    {
        Point nw_left = new Point(), nw_right = new Point(),
              ne_left = new Point(), ne_right = new Point(),
              sw_left = new Point(), sw_right = new Point(),
              se_left = new Point(), se_right = new Point();

        //north-west bound
        nw_left = this.LeftTopBound;
        nw_right.X = this.RightBottomBound.X / 2;
        nw_right.Y = this.RightBottomBound.Y / 2;

        // calculate north-east bound
        ne_left.X = this.RightBottomBound.X / 2;
        ne_left.Y = this.LeftTopBound.Y;
        ne_right.X = this.RightBottomBound.X;
        ne_right.Y = this.RightBottomBound.Y / 2;

        //south-west bound
        sw_left.X = this.LeftTopBound.X;
        sw_left.Y = this.RightBottomBound.Y / 2;
        sw_right.X = this.RightBottomBound.X / 2;
        sw_right.Y = this.RightBottomBound.Y;

        //south-east bound
        se_left.X = this.RightBottomBound.X / 2;
        se_left.Y = this.RightBottomBound.Y / 2;
        se_right = this.RightBottomBound;

        //initialize new leafs
        this.north_west = new Quadtree(nw_left, nw_right, null);
        this.north_east = new Quadtree(ne_left, ne_right, null);
        this.south_west = new Quadtree(sw_left, sw_right, null);
        this.south_east = new Quadtree(se_left, se_right, null);
    }


    public bool Insert(Point _point)
    {
        //if we can add current point and it's included in this quadrant range - add it!
        if (this.Includes(_point))
        {
            if (this.points.Count <= this.MaximumObjects)
            {
                this.points.Add(_point);
                return true;
            }

            //else if there's no place for another one point or it's not included in current quadrant range, 
            //subdivide current leaf, detect quadrant to which we want to add current point, then just add it.
            else
            {
                WindowsFormsApplication1.Form1.DrawGrid(this.LeftTopBound, this.RightBottomBound);
                this.Subdivide();
                this.NumberOfChildren += 4;

                MessageBox.Show("Subdivide");

                //try to push current point to one of the created leafs
                if (this.north_west.Insert(_point)) { this.north_west.Parent = this; return true; }
                if (this.north_east.Insert(_point)) { this.north_east.Parent = this; return true; }
                if (this.south_west.Insert(_point)) { this.south_west.Parent = this; return true; }
                if (this.south_east.Insert(_point)) { this.south_east.Parent = this; return true; }                
            }
        }
        else 
        { 
            return false; 
        }

        //this branch never executed (potentially)
        return false;
    }

    #endregion
}

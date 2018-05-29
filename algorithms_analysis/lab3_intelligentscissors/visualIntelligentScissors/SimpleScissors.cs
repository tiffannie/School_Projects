using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;

namespace VisualIntelligentScissors
{
	public class SimpleScissors : Scissors
	{
        Pen yellowpen = new Pen(Color.Yellow);

		public SimpleScissors() { }

        /// <summary>
        /// constructor for SimpleScissors. 
        /// </summary>
        /// <param name="image">the image you are going to segment including methods for getting gradients.</param>
        /// <param name="overlay">a bitmap on which you can draw stuff.</param>
		public SimpleScissors(GrayBitmap image, Bitmap overlay) : base(image, overlay) { }

        // this is a class you need to implement in CS 312. 

        /// <summary>
        ///  this is the class to implement for CS 312. 
        /// </summary>
        /// <param name="points">the list of segmentation points parsed from the pgm file</param>
        /// <param name="pen">a pen for writing on the overlay if you want to use it.</param>
		public override void FindSegmentation(IList<Point> points, Pen pen)
		{
            // this is the entry point for this class when the button is clicked for 
            // segmenting the image using the simple greedy algorithm. 
            // the points
            
			if (Image == null) throw new InvalidOperationException("Set Image property first.");

            //circle points that are given
            ColorStartingPoints(points);
            
            //for each point, find the path to that point
            for(int i= 0; i<points.Count; i++)
            {   //going from i to i+1%mod count <- starts back at first point
                PathToPoints(points[i], points[(i + 1) % points.Count]); //the mod is to make sure we wrap around the first point
            }
        } 
        //draws circles around labeled pixel points
        private void ColorStartingPoints(IList<Point> points)
        {
            using (Graphics g = Graphics.FromImage(Overlay))
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Point start = points[i];                            //get the start
                    g.DrawEllipse(yellowpen, start.X, start.Y, 5, 5);
                }
                Program.MainForm.RefreshImage(); //repaints
            }
        }

        private Boolean WithinPicture(Point point)
        {   //makes sure point is within dimension of image
            return (point.X < (Overlay.Width - 2) && point.Y < (Overlay.Height - 2) && point.X > 1 && point.Y > 1);
        }

        private void PathToPoints(Point start, Point end)
        {
            //normal lists use index values, we would have to find index, then find object, then find value of object and compare the value
            //to another value of an object in an index of another list
            //hash set. faster to use hash number because like objects will have exact same hash number
            HashSet<Point> visited = new HashSet<Point>();//empty hash set
            Point currentPoint = start; 

            while(currentPoint != end)  //while the current point is not equal to the end point
            {
                //Program.MainForm.RefreshImage();
                //draw the current pixel and add it to the visited list. Also, set an arbitrarily large integer as a weight
                Overlay.SetPixel(currentPoint.X, currentPoint.Y, Color.Red);
                visited.Add(currentPoint);

                int leastPointWeight = int.MaxValue; //highest value you can get of an integer. 3 million or something like that.

                Point nPoint = new Point(currentPoint.X, currentPoint.Y - 1);
                Point ePoint = new Point(currentPoint.X + 1, currentPoint.Y);
                Point sPoint = new Point(currentPoint.X, currentPoint.Y+1);
                Point wPoint = new Point(currentPoint.X - 1, currentPoint.Y);
                /*Point nePoint = new Point(currentPoint.X+1, currentPoint.Y - 1);
                Point nwPoint = new Point(currentPoint.X - 1, currentPoint.Y -1);
                Point sePoint = new Point(currentPoint.X+1, currentPoint.Y + 1);
                Point swPoint = new Point(currentPoint.X - 1, currentPoint.Y+1);*/

                int nWeight = this.GetPixelWeight(nPoint);
                int eWeight = this.GetPixelWeight(ePoint);
                int sWeight = this.GetPixelWeight(sPoint);
                int wWeight = this.GetPixelWeight(wPoint);
               /* int neWeight = this.GetPixelWeight(nePoint);
                int nwWeight = this.GetPixelWeight(nwPoint);
                int seWeight = this.GetPixelWeight(sePoint);
                int swWeight = this.GetPixelWeight(swPoint);*/

                //get the next point if it is within the picture, the point has already been visited, and its weight is less than the previous
                //point because of the way this is set up, it will go in a clockwise fashion (nesw) and wil only accept smaller weights once a
                //weight is accepted
                if (WithinPicture(nPoint) && !visited.Contains(nPoint) && nWeight < leastPointWeight)
                {
                    currentPoint = nPoint;
                    leastPointWeight = nWeight;
                }

                if (WithinPicture(ePoint) && !visited.Contains(ePoint) && eWeight < leastPointWeight)
                {
                    currentPoint = ePoint;
                    leastPointWeight = eWeight;
                }

                if (WithinPicture(sPoint) && !visited.Contains(sPoint) && sWeight < leastPointWeight)
                {
                    currentPoint = sPoint;
                    leastPointWeight = sWeight;
                }

                if (WithinPicture(wPoint) && !visited.Contains(wPoint) && wWeight < leastPointWeight)
                {
                    currentPoint = wPoint;
                    leastPointWeight = wWeight;
                }
                /*if (WithinPicture(nePoint) && !visited.Contains(nePoint) && nWeight < leastPointWeight)
                {
                    currentPoint = nePoint;
                    leastPointWeight = neWeight;
                }

                if (WithinPicture(nwPoint) && !visited.Contains(nwPoint) && nwWeight < leastPointWeight)
                {
                    currentPoint = nwPoint;
                    leastPointWeight = nwWeight;
                }

                if (WithinPicture(sePoint) && !visited.Contains(sePoint) && seWeight < leastPointWeight)
                {
                    currentPoint = sePoint;
                    leastPointWeight = seWeight;
                }

                if (WithinPicture(swPoint) && !visited.Contains(swPoint) && swWeight < leastPointWeight)
                {
                    currentPoint = swPoint;
                    leastPointWeight = swWeight;
                }*/
                //we go through all the weights of all sides and find the smallest and add it to visited.

                //if our weight never changed, meaning we didn't go n e s w then we've reached a dead end. return
                if (leastPointWeight == int.MaxValue)
                {
                    break;
                }
            }
        }

    }
}


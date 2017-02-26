using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ex_Drawing
{
    class Square
    {
        private int length;
        private int centerX;    //x coordinate of the center point of the square
        private int centerY;    //y
        private int x;
        private int y;
        private string figure;  //figure drew on the square

        public Square(int len, int cx, int cy, int x, int y)
        {
            length = len;
            centerX = cx;
            centerY = cy;
            this.x = x;
            this.y = y;
            figure = "";
        }

        /// <summary>
        /// Check if a given point is in this square
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool IsInSquare(Point pt)
        {
            if(pt.X < centerX + length/2 && pt.X > centerX - length/2
                && pt.Y < centerY + length/2 && pt.Y > centerY - length / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get the coordinate of the center of the square
        /// </summary>
        /// <returns></returns>
        public Point getCenter()
        {
            return new Point(centerX, centerY);
        }

        /// <summary>
        /// Get the position in Squares board
        /// </summary>
        /// <returns></returns>
        public Point getPosition()
        {
            return new Point(this.x, this.y);
        }

        public void setFigure(string fg)
        {
            figure = fg;
        }

        public string getFigure()
        {
            return figure;
        }

        public bool hasFigure()
        {
            return this.figure != "";
        }
    }
}

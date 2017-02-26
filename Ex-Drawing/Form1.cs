using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Ex_Drawing
{

    public partial class Form1 : Form
    {
        
        private const int radius = 30;
        private const int boardLen = 100;
        private const int boardSize = 3;
        private Square[, ] squares;

        public Form1()
        {
            InitializeComponent();
            squares = new Square[boardSize, boardSize];
            //initialize the square object
            for(int x = 0; x < boardSize; x++)
            {
                for(int y = 0; y < boardSize; y++)
                {
                    squares[x, y] = new Square(boardLen, boardLen * x + boardLen/2, boardLen * y + boardLen/2, x, y);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pn = new Pen(Color.Black, 1);
            //draw lines on the board
            g.DrawLine(pn, new Point(0, 100), new Point(300, 100));
            g.DrawLine(pn, new Point(0, 200), new Point(300, 200));
            g.DrawLine(pn, new Point(100, 0), new Point(100, 300));
            g.DrawLine(pn, new Point(200, 0), new Point(200, 300));
            foreach(Square sq in squares)
            {
                if (sq.getFigure() == "Cross")
                {
                    DrawCross(sq, g);
                }
                else if(sq.getFigure() == "Circle")
                {
                    DrawCircle(sq, g);
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if(lblResult.Text != "")
            {
                return;
            }
            Graphics g = panel1.CreateGraphics(); 
            foreach(Square sq in squares)
            {
                //find which square the user click
                if(sq.IsInSquare(new Point(e.X, e.Y)))
                {
                    //if this square already has X or O, do not respond
                    if (sq.hasFigure())
                    {
                        return;
                    }
                    if (e.Button == MouseButtons.Left)
                    {
                        DrawCross(sq, g);
                    }
                    else if(e.Button == MouseButtons.Right)
                    {
                        DrawCircle(sq, g);
                    }
                    lblResult.Text = CheckResult(sq);
                    break;
                }
            } 
        }

        /// <summary>
        /// Draw a cross on the specific square
        /// </summary>
        /// <param name="sq"></param>
        private void DrawCross(Square sq, Graphics g)
        {
            Pen pn = new Pen(Color.Red, 3);
            // calculate left top point of drawing
            Point ptLT = new Point(sq.getCenter().X - radius, sq.getCenter().Y - radius);
            //left bottom
            Point ptLB = new Point(sq.getCenter().X - radius, sq.getCenter().Y + radius);
            //right top
            Point ptRT = new Point(sq.getCenter().X + radius, sq.getCenter().Y - radius);
            //right bottom
            Point ptRB = new Point(sq.getCenter().X + radius, sq.getCenter().Y + radius);

            g.DrawLine(pn, ptLT, ptRB);
            g.DrawLine(pn, ptLB, ptRT);
            sq.setFigure("Cross");
        }

        /// <summary>
        /// Draw a circle on specific square
        /// </summary>
        /// <param name="sq"></param>
        private void DrawCircle(Square sq, Graphics g)
        {
            Pen pn = new Pen(Color.Blue, 3);
            // calculate left top point of drawing
            Point ptLT = new Point(sq.getCenter().X - radius, sq.getCenter().Y - radius);
            Rectangle rect = new Rectangle(ptLT, new Size(radius * 2, radius * 2));
            
            g.DrawEllipse(pn, rect);
            sq.setFigure("Circle");
        }

        /// <summary>
        /// Check the result of the game
        /// </summary>
        /// <param name="sq">last square drawn</param>
        /// <returns>result of the game. empty string if game not finished</returns>
        private string CheckResult(Square sq)
        {
            string result = sq.getFigure() + " Wins!";
            int posX = sq.getPosition().X;
            int posY = sq.getPosition().Y;
            //check row
            for (int x = 0; x < boardSize; x++)
            {
                if(squares[x, posY].getFigure() != sq.getFigure())
                {
                    result = "";
                }     
            }
            if(result != "")
            {
                return result;
            }

            //check colomn
            result = sq.getFigure() + " Wins!";
            for (int y = 0; y < boardSize; y++)
            {
                if(squares[posX, y].getFigure() != sq.getFigure())
                {
                    result = "";
                }
            }
            if(result != "")
            {
                return result;
            }

            //check diagonal
            if (squares[1, 1].hasFigure())
            {
                if (
                    (squares[1, 1].getFigure() == squares[0, 0].getFigure()
                    && squares[1, 1].getFigure() == squares[2, 2].getFigure())
                    ||
                    (squares[1, 1].getFigure() == squares[0, 2].getFigure()
                    && squares[1, 1].getFigure() == squares[2, 0].getFigure())
                   )
                {
                    return squares[1, 1].getFigure() + " Wins!";
                }
            }
            return result; 
        }

        /// <summary>
        /// Reset the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            foreach (Square sq in squares)
            {
                sq.setFigure("");
            }
            panel1.Refresh();
            lblResult.Text = "";
            
        }
    }
}

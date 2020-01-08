using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Piece:Button
    {
        private int X;
        private int Y;
        private Size s=new Size(29,29);

        public enum PIECE_STATE
        {
            STATE_GRAY= 0,
            STATE_WHITE= 1,
            STATE_BLACK= 2
        };

        private PIECE_STATE pieceState;

        public PIECE_STATE PieceState
        {
            get
            {
                return pieceState;
            }

            set
            {
                pieceState = value;
                switch(pieceState)
                {
                    case PIECE_STATE.STATE_GRAY:
                        Console.WriteLine("is gray");
                        this.BackColor = Color.LightGray;
                        this.Enabled = true;
                        break;
                    case PIECE_STATE.STATE_WHITE:
                        Console.WriteLine("is white");
                        this.BackColor = Color.White;
                        this.Enabled = false;
                        break;
                    case PIECE_STATE.STATE_BLACK:
                        Console.WriteLine("is black");
                        this.BackColor = Color.Black;
                        this.Enabled = false;
                        break;
                }
                //this.BackColor = (pieceState==PIECE_STATE.STATE_BLACK) ? Color.Black : Color.White;
            }
        }

        public Piece() {} //empty constructor
        public Piece(PIECE_STATE color, int x, int y, Point p)
        {
            this.PieceState = color;
            this.X = x;
            this.Y = y;
            this.Location = p;
            this.Size=s;
            this.Visible=true;
            this.FlatAppearance.BorderColor = Color.DarkSlateGray;
        }

        public void ChangeColor()
        {
            this.PieceState = (this.pieceState == PIECE_STATE.STATE_BLACK) ? PIECE_STATE.STATE_WHITE : PIECE_STATE.STATE_BLACK;
        }

        public int GetX()
        {
            return this.X;
        }

        public int GetY()
        {
            return this.Y;
        }
    }
}


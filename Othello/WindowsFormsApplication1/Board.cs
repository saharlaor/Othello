using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Board
    {
        public static Piece[,] pieces = new Piece[8, 8];
        
        public Board()
        {
            for(int y=0; y<8; y++)
            {
                for(int x=0; x<8; x++)
                {
                    pieces[x, y] = new Piece(Piece.PIECE_STATE.STATE_GRAY, x, y, new Point { X = 27 + 35 * x, Y = 53 + 35 * y });
                }
            }

        }

        public bool InBoard(int x, int y)
        {
            if (x % 8 <= 0 || y % 8 <= 0)
                return false;
            return true;
        }

        public  Piece[,] GetPieces()
        {
            return pieces;
        }
    }
}

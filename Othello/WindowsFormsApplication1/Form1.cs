using System;
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
        public int blackPoints;
        public int whitePoints;
        bool blackTurn = true;
        protected Piece[,] pieces;
       public Board mainBoard;
        
        public Form1()
        {
            InitializeComponent();
        }

        public void AddPiece(Piece p)
        {
            this.Controls.Add(p);
        }

        public void ColorChange(Piece p)
        {
            p.ChangeColor();
        }

        private bool LegalMove(int x, int y)
        {
            bool legal = false;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        if(LegalMoveDir(x, y, false, j, i))
                        {
                            legal = true;
                        }
                    }
                }
            }
            return legal;
        }

        private bool LegalMoveDir(int x, int y, bool change, int dirX, int dirY)
        {
            int initialX = x;
            int initialY = y;
            Queue<Piece> Flipped = new Queue<Piece>();
            Piece.PIECE_STATE c = pieces[x, y].PieceState;
            int counter = 1;
            x += dirX;
            y += dirY;
            while (mainBoard.InBoard(x, y))
            {
                if (pieces[x, y].PieceState == Piece.PIECE_STATE.STATE_GRAY)
                {
                    return false;
                }
                if (pieces[x, y].PieceState != c)
                {
                    x += dirX;
                    y += dirY;
                    counter++;
                }
                else
                {
                    if (counter > 1)
                    {
                        FlipPiece(initialX, initialY, dirX, dirY, x, y);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        private void FlipPiece(int startX, int startY, int dirX, int dirY, int endX, int endY)
        {
            startX += dirX;
            startY += dirY;
            while ((startX != endX || startY != endY)&&(mainBoard.InBoard(startX,startY)))
            {
                ColorChange(pieces[startX, startY]);
                startX += dirX;
                startY += dirY;
                blackPoints = (blackTurn) ?  blackPoints + 1:blackPoints - 1;
                whitePoints = (blackTurn) ? whitePoints - 1 : whitePoints + 1;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            blackTurn = true;
            foreach (Piece piece in pieces)
            {
                Controls.Remove(piece);
            }
            blackPoints = 2;
            whitePoints = 2;
            mainBoard = new Board();
            pieces = mainBoard.GetPieces();
            turnLabel.Text = "BLACK's TURN";
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    pieces[x, y].Click += this.Piece_Click;
                    this.Controls.Add(pieces[x, y]);
                    if ((x == 3 || x == 4) && (y == 3 || y == 4))
                    {
                        if (x - y == 0)
                            pieces[x, y].PieceState = Piece.PIECE_STATE.STATE_BLACK;
                        else
                            pieces[x, y].PieceState = Piece.PIECE_STATE.STATE_WHITE;
                    }
                }
            }
        }

        public void Piece_Click(object sender, EventArgs e)
        {
            Piece p=(Piece)sender;
            p.PieceState = (blackTurn) ? Piece.PIECE_STATE.STATE_BLACK : Piece.PIECE_STATE.STATE_WHITE;
            if (LegalMove(p.GetX(), p.GetY()))
            {
                if (blackTurn)
                {
                    p.PieceState = Piece.PIECE_STATE.STATE_BLACK;
                    blackPoints++;
                    BlackPoints.Text = (int.Parse(BlackPoints.Text) + 1).ToString();
                }
                else
                {
                    p.PieceState = Piece.PIECE_STATE.STATE_WHITE;
                    whitePoints++;
                    WhitePoints.Text = (Int32.Parse(BlackPoints.Text) + 1).ToString();
                }
                blackTurn = !(blackTurn);
                turnLabel.Text =(blackTurn)? "BLACK's TURN":"WHITE's TURN";
            }
            else
                p.PieceState = Piece.PIECE_STATE.STATE_GRAY;
            BlackPoints.Text = blackPoints.ToString();
            WhitePoints.Text = whitePoints.ToString();
            if(blackPoints+whitePoints==64 || blackPoints==0 || whitePoints==0)
            {
                End_Game();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            blackPoints = 2;
            whitePoints = 2;
            mainBoard= new Board();
            pieces = mainBoard.GetPieces();
            turnLabel.Text = "BLACK's TURN";
            for (int y=0; y<8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    pieces[x, y].Click += this.Piece_Click;
                    this.Controls.Add(pieces[x, y]);
                    if ((x == 3 || x == 4) && (y == 3 || y == 4))
                    {
                        if (x - y == 0)
                            pieces[x, y].PieceState = Piece.PIECE_STATE.STATE_BLACK;
                        else
                            pieces[x, y].PieceState = Piece.PIECE_STATE.STATE_WHITE;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.blackTurn = !blackTurn;
            turnLabel.Text = (blackTurn) ? "BLACK's TURN" : "WHITE's TURN";
        }

        private void End_Game()
        {
            string winner;
            if (blackPoints == whitePoints)
            {
                MessageBox.Show("IT'S A DRAW!");
            }
            else
            {
                if (blackPoints > whitePoints)
                    winner = "black";
                else
                    winner = "white";
            MessageBox.Show(winner + " is the winner");
            }
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void fullInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.yourturnmyturn.com/rules/reversi.php");
        }

        private void shortInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.flyordie.com/games/help/Reversi/en/games_rules_reversi.html");
        }
    }
}

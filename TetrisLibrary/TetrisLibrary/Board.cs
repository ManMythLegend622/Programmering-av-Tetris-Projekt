using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Representerar tetris boarden
    public class Board : IBoard
    {
        private Color background = new Color(20, 20, 20);
        private Color[,] board;
        private IShape shape;
        private IShape nextShape;

        /// startar när spelet är över
        public event GameOverHandle GameOver;
        /// startat när board linjerna clearas när en rad är färdig
        public event LinesClearedHandle LinesCleared;

        /// Startar board objektet
        public Board()
        {
            board = new Color[10, 21];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 21; j++)
                    board[i, j] = background;
            }

            this.shape = new ShapeProxy(this);
            shape.JoinPile += addToPile;
            this.nextShape = new ShapeProxy(this);
        }

        /// nuvarande formen
        public IShape Shape
        {
            get { return shape; }
        }

        /// nästa formen
        public IShape NextShape
        {
            get { return nextShape; }
        }

        /// färgen på det specifika stället i boarden.
        public Color this[int i, int j]
        {
            get
            {
                checkCoordinate(i, j);
                return board[i, j];
            }
        }

        /// ger tillbaka elementen i en dimension
        public int GetLength(int rank)
        {
            if (rank < 0 || rank >= board.Rank)
                throw new IndexOutOfRangeException("Index: " + rank + ". Rank: " + board.Rank);
            return board.GetLength(rank);
        }

        /// kör igång game over elementet
        protected virtual void OnGameOver()
        {
            if (GameOver != null)
                GameOver();
        }

        /// kör igång lines cleared elementet 
        protected virtual void OnLinesCleared(int lines)
        {
            if (LinesCleared != null)
                LinesCleared(lines);
        }

        //hanterar gameover
        private void addToPile()
        {
            for (int i = 0; i < shape.Length; i++)
            {
                Block block = shape[i];
                board[block.Position.X, block.Position.Y] = block.Colour;
            }

            checkClearedLines();

            if (!checkGameOver())
            {
                ((ShapeProxy)shape).DeployNewShape((ShapeProxy)nextShape);
                ((IShapeFactory)nextShape).DeployNewShape();
            }
        }

        //kollar om en hel linje har formats
        private void checkClearedLines()
        {
            int lines = 0;
            bool noBlack = true;
            int boardWidth = this.GetLength(0);
            int boardHeight = this.GetLength(1);

            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth && noBlack; x++)
                {
                    if (board[x, y] == background)
                        noBlack = false;
                    else if (x == (boardWidth - 1))
                    {
                        clearLine(y, boardWidth, boardHeight);
                        lines++;
                    }
                }
                noBlack = true;
            }
            if (lines > 0)
            {
                OnLinesCleared(lines);
            }
        }

        //kollar om kordinaterna är inom boarden
        private void checkCoordinate(int x, int y)
        {
            if (x < 0 || y < 0)
                throw new IndexOutOfRangeException("Givna kordinaten är negativ.");
            if (x >= this.GetLength(0) || y >= this.GetLength(1))
                throw new IndexOutOfRangeException("Givna kordinaten är utanför.");
        }

        //kollar om det är en gameover
        private bool checkGameOver()
        {
            int boardWidth = this.GetLength(0);
            //x=0 to check all columns
            for (int i = 0; i < boardWidth; i++)
            {
                //y=0 row is not a part of the game board and serves as a buffer for newly-created shapes
                if (board[i, 1] != background)
                {
                    OnGameOver();
                    return true;
                }
            }
            return false;
        }

        //tar bort hela raden från boarden
        private void clearLine(int line, int boardWidth, int boardHeight)
        {
            for (int y = line; y > 0; y--)
            {
                for (int x = 0; x < boardWidth; x++)
                    board[x, y] = board[x, y - 1];
            }
            for (int x = 0; x < boardWidth; x++)
                board[x, 0] = background;
        }
    }
}
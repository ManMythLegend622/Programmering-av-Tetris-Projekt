using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Representerar blocken som dom olika formerna best�r av
    public class Block
    {
        private Color background = new Color(20, 20, 20);
        private IBoard board;
        private Color colour;
        private Point position;

        /// Initierar block v�rdet
        public Block(IBoard board, Color colour, Point position)
        {
            if (board == null || colour == null || position == null)
                throw new ArgumentNullException();

            this.board = board;
            this.colour = colour;
            checkCoordinate(position, board);
            this.position = position;
        }

        /// Ger block f�rgen
        public Color Colour
        { get { return colour; } }

        /// Ger block positionen
        public Point Position
        {
            get { return new Point(position.X, position.Y); }
            set
            {
                checkCoordinate(value, board);
                position.X = value.X;
                position.Y = value.Y;
            }
        }

        /// Flyttar blocket ned�t om det g�r
        public void MoveDown()
        {
            if (TryMoveDown())
                position.Y++;
        }

        /// Flyttar blocket v�nster om det g�r
        public void MoveLeft()
        {
            if (TryMoveLeft())
                position.X--;
        }

        /// Flyttar blocket h�ger om det g�r
        public void MoveRight()
        {
            if (TryMoveRight())
                position.X++;
        }

        /// Roterar blocket om det g�t
        public void Rotate(Point offset)
        {
            if (TryRotate(offset))
            {
                position.X += offset.X;
                position.Y += offset.Y;
            }
        }

        /// Kollar om det g�r att flytta blocket ner�t
        public bool TryMoveDown()
        {
            int y = position.Y + 1;
            if (y >= board.GetLength(1) || board[position.X, y] != background)
                return false;
            return true;
        }

        /// kollar om det g�r att flytta den v�nster
        public bool TryMoveLeft()
        {
            int x = position.X - 1;
            if (x < 0 || board[x, position.Y] != background)
                return false;
            return true;
        }

        /// kollar om det g�r att flytta den h�ger
        public bool TryMoveRight()
        {
            int x = position.X + 1;
            if (x >= board.GetLength(0) || board[x, position.Y] != background)
                return false;
            return true;
        }

        /// Kollar om det g�r att rotera blocket
        public bool TryRotate(Point offset)
        {
            int x = position.X + offset.X;
            int y = position.Y + offset.Y;
            if (x < 0 || y < 0 || x >= board.GetLength(0) ||
                y >= board.GetLength(1) || board[x, y] != background)
                return false;
            return true;
        }

        //Kollar om kordinaterna �r valida s� dom inte �r utanf�r boardet.
        private static void checkCoordinate(Point coord, IBoard board)
        {
            if (coord.X < 0 || coord.Y < 0)
                throw new ArgumentException("Givna kordinaten �r negativ");
            if (coord.X >= board.GetLength(0) || coord.Y >= board.GetLength(1))
                throw new ArgumentException("Givna kordinaten �r utanf�r.");
        }

    }
}
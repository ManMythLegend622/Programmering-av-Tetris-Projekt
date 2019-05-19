using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Representerar blocken som dom olika formerna består av
    public class Block
    {
        private Color background = new Color(20, 20, 20);
        private IBoard board;
        private Color colour;
        private Point position;

        /// Initierar block värdet
        public Block(IBoard board, Color colour, Point position)
        {
            if (board == null || colour == null || position == null)
                throw new ArgumentNullException();

            this.board = board;
            this.colour = colour;
            checkCoordinate(position, board);
            this.position = position;
        }

        /// Ger block färgen
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

        /// Flyttar blocket nedåt om det går
        public void MoveDown()
        {
            if (TryMoveDown())
                position.Y++;
        }

        /// Flyttar blocket vänster om det går
        public void MoveLeft()
        {
            if (TryMoveLeft())
                position.X--;
        }

        /// Flyttar blocket häger om det går
        public void MoveRight()
        {
            if (TryMoveRight())
                position.X++;
        }

        /// Roterar blocket om det gåt
        public void Rotate(Point offset)
        {
            if (TryRotate(offset))
            {
                position.X += offset.X;
                position.Y += offset.Y;
            }
        }

        /// Kollar om det går att flytta blocket neråt
        public bool TryMoveDown()
        {
            int y = position.Y + 1;
            if (y >= board.GetLength(1) || board[position.X, y] != background)
                return false;
            return true;
        }

        /// kollar om det går att flytta den vänster
        public bool TryMoveLeft()
        {
            int x = position.X - 1;
            if (x < 0 || board[x, position.Y] != background)
                return false;
            return true;
        }

        /// kollar om det går att flytta den höger
        public bool TryMoveRight()
        {
            int x = position.X + 1;
            if (x >= board.GetLength(0) || board[x, position.Y] != background)
                return false;
            return true;
        }

        /// Kollar om det går att rotera blocket
        public bool TryRotate(Point offset)
        {
            int x = position.X + offset.X;
            int y = position.Y + offset.Y;
            if (x < 0 || y < 0 || x >= board.GetLength(0) ||
                y >= board.GetLength(1) || board[x, y] != background)
                return false;
            return true;
        }

        //Kollar om kordinaterna är valida så dom inte är utanför boardet.
        private static void checkCoordinate(Point coord, IBoard board)
        {
            if (coord.X < 0 || coord.Y < 0)
                throw new ArgumentException("Givna kordinaten är negativ");
            if (coord.X >= board.GetLength(0) || coord.Y >= board.GetLength(1))
                throw new ArgumentException("Givna kordinaten är utanför.");
        }

    }
}
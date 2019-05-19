using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Represents L Tetris shape.
    public class ShapeL : Shape
    {
        /// Instantiates FormL.
        public ShapeL(IBoard board) : base(board, setBlocks(board), setOffsets())
        { }

        //Creates the blocks for FormL with orange colour block starting at (6,0)
        private static Block[] setBlocks(IBoard board)
        {
            Block[] blocks = new Block[4];
            blocks[0] = new Block(board, Color.DarkOrange, new Point(5, 1));
            blocks[1] = new Block(board, Color.DarkOrange, new Point(5, 0));
            blocks[2] = new Block(board, Color.DarkOrange, new Point(6, 0));
            blocks[3] = new Block(board, Color.DarkOrange, new Point(7, 0));
            return blocks;
        }

        //Creates offsets for each block used for rotation.
        //FormL has four possible rotations.
        private static Point[][] setOffsets()
        {
            Point[][] offsets = new Point[4][];
            offsets[0] = new Point[4] { new Point(2, 0), new Point(1, 1), new Point(0, 0), new Point(-1, -1) };
            offsets[1] = new Point[4] { new Point(0, -2), new Point(1, -1), new Point(0, 0), new Point(-1, 1) };
            offsets[2] = new Point[4] { new Point(-2, 0), new Point(-1, -1), new Point(0, 0), new Point(1, 1) };
            offsets[3] = new Point[4] { new Point(0, 2), new Point(-1, 1), new Point(0, 0), new Point(1, -1) };
            return offsets;
        }
    }
}
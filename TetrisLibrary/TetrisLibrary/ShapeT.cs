using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Represents T Tetris shape.
    public class ShapeT : Shape
    {
        /// Instantiates ShapeT.
        public ShapeT(IBoard board) : base(board, setBlocks(board), setOffsets())
        { }

        //Creates the blocks for ShapeT with purple colour block starting at (6,0)
        private static Block[] setBlocks(IBoard board)
        {
            Block[] blocks = new Block[4];
            blocks[0] = new Block(board, Color.Purple, new Point(5, 0));
            blocks[1] = new Block(board, Color.Purple, new Point(6, 0));
            blocks[2] = new Block(board, Color.Purple, new Point(7, 0));
            blocks[3] = new Block(board, Color.Purple, new Point(6, 1));
            return blocks;
        }

        //Creates offsets for each block used for rotation.
        //ShapeT has four possible rotations.
        private static Point[][] setOffsets()
        {
            Point[][] offsets = new Point[4][];
            offsets[0] = new Point[4] { new Point(1, 1), new Point(0, 0), new Point(-1, -1), new Point(1, -1) };
            offsets[1] = new Point[4] { new Point(1, -1), new Point(0, 0), new Point(-1, 1), new Point(-1, -1) };
            offsets[2] = new Point[4] { new Point(-1, -1), new Point(0, 0), new Point(1, 1), new Point(-1, 1) };
            offsets[3] = new Point[4] { new Point(-1, 1), new Point(0, 0), new Point(1, -1), new Point(1, 1) };
            return offsets;
        }
    }
}
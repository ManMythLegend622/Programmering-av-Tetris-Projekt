using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Represents O Tetris shape.
    public class ShapeO : Shape
    {
        /// Instantiates ShapeO.
        public ShapeO(IBoard board) : base(board, setBlocks(board), null)
        { }

        /// Does nothing since O has only 1 rotation configuration.
        public override void Rotate()
        { }

        //Creates the blocks for FormO with yellow colour block starting at (6,0)
        private static Block[] setBlocks(IBoard board)
        {
            Block[] blocks = new Block[4];
            blocks[0] = new Block(board, Color.Gold, new Point(5, 0));
            blocks[1] = new Block(board, Color.Gold, new Point(6, 0));
            blocks[2] = new Block(board, Color.Gold, new Point(5, 1));
            blocks[3] = new Block(board, Color.Gold, new Point(6, 1));
            return blocks;
        }
    }
}
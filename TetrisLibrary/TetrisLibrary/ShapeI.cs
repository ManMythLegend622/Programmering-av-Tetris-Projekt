﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Represents I Tetris shape.
    public class ShapeI : Shape
    {
        /// Instantiates ShapeI.
        public ShapeI(IBoard board) : base(board, setBlocks(board), setOffsets())
        { }

        //Creates the blocks for ShapeI with cyan colour starting at (6,0)
        private static Block[] setBlocks(IBoard board)
        {
            Block[] blocks = new Block[4];
            for (int i = 0; i < 4; i++)
                blocks[i] = new Block(board, Color.DodgerBlue, new Point(4 + i, 0));
            return blocks;
        }

        //Creates offsets for each block used for rotation.
        private static Point[][] setOffsets()
        {
            Point[][] offsets = new Point[2][];
            offsets[0] = new Point[4] { new Point(2, 2), new Point(1, 1), new Point(0, 0), new Point(-1, -1) };
            offsets[1] = new Point[4];
            for (int i = 0; i < 4; i++)
                offsets[1][i] = new Point(-offsets[0][i].X, -offsets[0][i].Y);
            return offsets;
        }
    }
}
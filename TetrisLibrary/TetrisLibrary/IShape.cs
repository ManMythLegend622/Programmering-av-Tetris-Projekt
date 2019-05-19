using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Delegate for JoinPile event.
    public delegate void JoinPileHandler();

    /// Represents the Tetris Shape.
    public interface IShape
    {
        event JoinPileHandler JoinPile;

        /// The length of the Shape 
        /// (i.e. the number of blocks the Shape consists of).
        int Length
        { get; }

        /// Returns one of the Blocks of the Shape depending on the index.
        Block this[int i]
        { get; }

        /// Drops the Shape to the bottom of the board until the free space is available.
        void Drop();

        /// Moves the current Shape down.
        void MoveDown();

        /// Moves the current Shape left.
        void MoveLeft();

        /// Moves the current Shape right.
        void MoveRight();

        /// Rotates the current Shape.
        void Rotate();

    }
}
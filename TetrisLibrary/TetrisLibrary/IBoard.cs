
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Delegate for GameOver event.
    public delegate void GameOverHandle();

    /// Delegate for LinesCleared event.
    public delegate void LinesClearedHandle(int num);

    /// Represents the Tetris board.
    public interface IBoard
    {
        /// Fires when the game is over.
        event GameOverHandle GameOver;
        /// Fires when some Board lines are cleared after the Shape joins the pile.
        event LinesClearedHandle LinesCleared;

        /// The colour of the specific place at the board.>
        Color this[int i, int j]
        { get; }

        /// The current Shape.
        IShape Shape
        { get; }

        /// Returns the number of elements in the specified dimension of board.
        int GetLength(int rank);
    }

}
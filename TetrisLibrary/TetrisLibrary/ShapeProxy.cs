using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLibrary
{
    /// Represents the current Shape on the board.
    public class ShapeProxy : IShapeFactory, IShape
    {
        private static Random random = new Random();
        private IBoard board;
        private IShape current;

        //Fires when the Shape is about to join the board pile.
        public event JoinPileHandler JoinPile;

        /// Instantiates ShapeProxy object and creates the new Shape.
        public ShapeProxy(IBoard board)
        {
            if (board == null)
                throw new ArgumentNullException();
            this.board = board;
            DeployNewShape();
        }

        /// The length of the current Shape 
        public int Length
        {
            get { return current.Length; }
        }

        /// Returns one of the Blocks of the current Shape depending on the index.
        public Block this[int i]
        {
            get { return current[i]; }
        }

        /// Randomly creates the new Shape and adds a listener for the JoinPile event.
        public void DeployNewShape()
        {
            int shape = random.Next(0, 7);
            switch (shape)
            {
                case 0:
                    current = new ShapeO(board);
                    break;
                case 1:
                    current = new ShapeI(board);
                    break;
                case 2:
                    current = new ShapeS(board);
                    break;
                case 3:
                    current = new ShapeT(board);
                    break;
                case 4:
                    current = new ShapeZ(board);
                    break;
                case 5:
                    current = new ShapeL(board);
                    break;
                default:
                    current = new ShapeJ(board);
                    break;
            }
            current.JoinPile += joinPileHandler;
        }

        /// Creates a new Shape according to the given nextShape and adds a listener for the JoinPile event
        public void DeployNewShape(ShapeProxy nextShape)
        {
            IShape shape = nextShape.current;
            if (shape is ShapeL)
                current = new ShapeL(board);
            else if (shape is ShapeI)
                current = new ShapeI(board);
            else if (shape is ShapeS)
                current = new ShapeS(board);
            else if (shape is ShapeZ)
                current = new ShapeZ(board);
            else if (shape is ShapeT)
                current = new ShapeT(board);
            else if (shape is ShapeO)
                current = new ShapeO(board);
            else
                current = new ShapeJ(board);

            current.JoinPile += joinPileHandler;
        }

        /// Drops the current Shape to the bottom of the board.
        public void Drop()
        {
            current.Drop();
        }

        /// Moves the current Shape down.
        public void MoveDown()
        {
            current.MoveDown();
        }

        /// Moves the current Shape left.
        public void MoveLeft()
        {
            current.MoveLeft();
        }

        /// Moves the current Shape right.
        public void MoveRight()
        {
            current.MoveRight();
        }

        /// Rotates the current Shape.
        public void Rotate()
        {
            current.Rotate();
        }

        /// Fires JoinPile event.
        protected virtual void OnJoinPile()
        {
            if (JoinPile != null)
                JoinPile();
        }

        //Handles the JoinPile event from current
        private void joinPileHandler()
        {
            OnJoinPile();
        }
    }
}
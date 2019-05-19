using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TetrisLibrary
{
    /// Representerar tetris formen
    public abstract class Shape : IShape
    {
        protected Block[] blocks;
        protected int currentRotation = 0;
        protected Point[][] rotationOffset;
        private IBoard board;

        //Händer när formen ska gå med board högen
        public event JoinPileHandler JoinPile;

        /// bas konstruktör för olika former
        public Shape(IBoard board, Block[] blocks, Point[][] offset)
        {
            if (board == null || blocks == null)
                throw new ArgumentNullException();

            for (int i = 0; i < blocks.Length; i++)
                if (blocks[i] == null)
                    throw new ArgumentNullException("en av blocken i clock arrayn är null.");

            if (offset != null)
            {
                for (int i = 0; i < offset.Length; i++)
                    if (offset[i].Length < blocks.Length)
                        throw new ArgumentException("Offset array längden: " + offset[i].Length +
                                                    ". Blockerar array längden: " + blocks.Length);
                for (int i = 0; i < offset.Length; i++)
                {
                    for (int j = 0; j < offset.Length; j++)
                        if (offset[i][j] == null)
                            throw new ArgumentNullException("En av offset värdena i offset arrayn är null.");
                }
            }

            this.board = board;
            this.blocks = blocks;
            this.rotationOffset = offset;
        }

        /// länden på formen
        public virtual int Length
        { get { return blocks.Length; } }

        public Block this[int i]
        {
            get
            {
                checkIndex(i);
                return new Block(board, blocks[i].Colour, blocks[i].Position);
            }
        }

        /// droppar formen till botten av boarden
        public virtual void Drop()
        {
            while (tryMoveDown())
                MoveDown();
        }

        /// flyttar den nuvarande formen neråt
        public virtual void MoveDown()
        {
            if (tryMoveDown())
            {
                for (int i = 0; i < this.Length; i++)
                    blocks[i].MoveDown();
            }
        }

        /// Flyttar den nuvarande formen vänster
        public virtual void MoveLeft()
        {
            bool moveAllow = true;

            //verifierar att varje block i formen kan röra sig
            for (int i = 0; i < this.Length && moveAllow; i++)
            {
                if (!blocks[i].TryMoveLeft())
                    moveAllow = false;
            }
            //flyttar formen
            if (moveAllow)
            {
                for (int i = 0; i < this.Length; i++)
                    blocks[i].MoveLeft();
            }
        }

        /// flyttar den nuvarande formen vänster
        public virtual void MoveRight()
        {
            bool moveAllow = true;

            //verifies that each Block of the Shape can move
            for (int i = 0; i < this.Length && moveAllow; i++)
            {
                if (!blocks[i].TryMoveRight())
                    moveAllow = false;
            }
            //moves the Shape
            if (moveAllow)
            {
                for (int i = 0; i < this.Length; i++)
                    blocks[i].MoveRight();
            }
        }

        /// Rotates the current Shape.
        public virtual void Rotate()
        {
            bool moveAllow = true;

            //verifies that each Block of the Shape can move
            for (int i = 0; i < this.Length && moveAllow; i++)
            {
                if (!blocks[i].TryRotate(rotationOffset[currentRotation][i]))
                    moveAllow = false;
            }
            //moves the Shape
            if (moveAllow)
            {
                for (int i = 0; i < this.Length; i++)
                    blocks[i].Rotate(rotationOffset[currentRotation][i]);

                currentRotation++;
                if (currentRotation >= rotationOffset.Length)
                    currentRotation = 0;
            }
        }

        /// Fires JoinPile event.
        protected virtual void OnJoinPile()
        {
            if (JoinPile != null)
                JoinPile();
        }

        private void checkIndex(int i)
        {
            if (i < 0 || i >= this.Length)
                throw new IndexOutOfRangeException("Index: " + i + ". Size: " + this.Length);
        }

        //Verifies whether it is possible to move the Shape down;
        private bool tryMoveDown()
        {
            for (int i = 0; i < this.Length; i++)
            {
                if (!blocks[i].TryMoveDown())
                {
                    OnJoinPile();
                    return false;
                }
            }
            return true;
        }
    }
}
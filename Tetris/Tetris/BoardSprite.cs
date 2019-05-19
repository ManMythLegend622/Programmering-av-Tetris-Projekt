
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TetrisLibrary;

namespace TetrisGame
{

    /// klassen som ritar och updaterrar brädet.
    public class BoardSprite : DrawableGameComponent
    {
        IBoard board;
        Spel1 game;
        SpriteBatch spriteBatch;
        Texture2D filledBlock;

        //visar spöke när G trycks
        private bool drawGhost = false;
        private bool keyGhost = false;

        /// game = spel1 objektet, board = logiska representationen av brädet.
        public BoardSprite(Spel1 game, IBoard board) : base(game)
        {
            this.game = game;
            this.board = board;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// laddar det som behövs
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            filledBlock = game.Content.Load<Texture2D>("FilledBlock");

            base.LoadContent();
        }

        /// uppdaterar brädet
        public override void Update(GameTime gameTime)
        {
            checkGhostKey(Keyboard.GetState());
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //Kopierar formen och ritar som ett spöke
            Block[] shapeCopy = new Block[board.Shape.Length];
            fill(ref shapeCopy);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 1; j < board.GetLength(1); j++)
                {
                    if (isGhostPosition(shapeCopy, i, j) && drawGhost)
                        spriteBatch.Draw(filledBlock, new Vector2(20 + i * 20, 35 + j * 20),
                            shapeCopy[0].Colour * 0.3f);
                    else
                        spriteBatch.Draw(filledBlock, new Vector2(20 + i * 20, 35 + j * 20), board[i, j]);
                }
            }

            //ritar nästa form
            IShape next = ((Board)board).NextShape;
            for (int i = 0; i < next.Length; i++)
            {
                spriteBatch.Draw(filledBlock, new Vector2(160 + next[i].Position.X * 20, 110 + next[i].Position.Y * 20),
                next[i].Colour);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //kollar spök läget
        private void checkGhostKey(KeyboardState keyboardState)
        {
            bool keyGhostNow = (keyboardState.IsKeyDown(Keys.G));

            if (!keyGhost && keyGhostNow)
                drawGhost = !drawGhost;

            keyGhost = keyGhostNow;
        }

        private bool isGhostPosition(Block[] shapeGhost, int x, int y)
        {
            for (int i = 0; i < shapeGhost.Length; i++)
                if (shapeGhost[i].Position.X == x && shapeGhost[i].Position.Y == y)
                    return true;
            return false;
        }


        private void fill(ref Block[] shapeGhost)
        {
            //kopierar formen
            for (int i = 0; i < board.Shape.Length; i++)
            {
                shapeGhost[i] = board.Shape[i];
            }

            //lägger den på sista position
            while (tryMoveDown(shapeGhost))
            {
                for (int i = 0; i < shapeGhost.Length; i++)
                    shapeGhost[i].MoveDown();
            }
            for (int i = 0; i < board.Shape.Length; i++)
            {
                if (shapeGhost[i].Position.Y < 2)
                    drawGhost = false;
            }
        }

        //Försöker gå neråt. Blir true om det går.
        private bool tryMoveDown(Block[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i].TryMoveDown())
                    return false;
            }
            return true;
        }
    }
}
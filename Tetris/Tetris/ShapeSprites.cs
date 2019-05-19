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
    /// Klassen som ritar och uppdaterar formens rörelse på boarden
    public class ShapeSprite : DrawableGameComponent
    {
        IShape shape;

        Score score;
        int counterMoveDown = 0;

        KeyboardState oldState;
        int counterInput = 0;
        int threshold;

        Spel1 game;
        SpriteBatch spriteBatch;

        Texture2D filledBlock;

        //För att pausa trycker spelaren p eller spacebar
        private bool paused = false;
        private bool keyPaused = false;

        /// initialiserar formsprite objektet
        public ShapeSprite(Spel1 game, IBoard board, Score score) : base(game)
        {
            this.game = game;
            this.score = score;
            this.shape = board.Shape;
        }

        /// tillåter initialisering innan spelets början
        public override void Initialize()
        {
            oldState = Keyboard.GetState();
            threshold = 25;

            base.Initialize();
        }

        /// laddar all nödvändig contetn för spelet
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            filledBlock = game.Content.Load<Texture2D>("FilledBlock");

            base.LoadContent();
        }

        /// Låter formen uppdateras. Låter användaren pausa
        public override void Update(GameTime gameTime)
        {
            checkPauseKey(Keyboard.GetState());
            if (!paused)
            {
                double delay = (11 - score.Level) * 0.05 * 60;
                if (counterMoveDown > delay)
                {
                    shape.MoveDown();
                    counterMoveDown = 0;
                }
                else
                {
                    counterMoveDown++;
                    checkInput();
                }
            }
            base.Update(gameTime);
        }

        /// kallas när formen ritas
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (int i = 0; i < shape.Length; i++)
            {
                int x = shape[i].Position.X;
                int y = shape[i].Position.Y;
                if (y > 0)
                    spriteBatch.Draw(filledBlock, new Vector2(20 + x * 20, 35 + y * 20), shape[i].Colour);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //flyttar formen beroende på vilken knapp som trycks
        private void checkCounter(Keys type, MoveFunction move)
        {
            if (!oldState.IsKeyDown(type))
            {
                move();
                counterInput = 0; 
            }
            else
            {
                counterInput++;
                if (counterInput > threshold)
                    move();
            }
        }

        //Ckollar vilken knapp som trycktes
        private void checkInput()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Right))
                checkCounter(Keys.Right, shape.MoveRight);
            else if (newState.IsKeyDown(Keys.Left))
                checkCounter(Keys.Left, shape.MoveLeft);
            else if (newState.IsKeyDown(Keys.Down))
                checkCounter(Keys.Down, shape.MoveDown);
            else if (newState.IsKeyDown(Keys.Up))
                checkCounter(Keys.Up, shape.Rotate);
            //för att släppa borde användaren trycka enter
            else if (newState.IsKeyDown(Keys.Enter))
                checkCounter(Keys.Enter, shape.Drop);
            oldState = newState;
        }

        //kollar om p eller space trycktes
        private void checkPauseKey(KeyboardState keyboardState)
        {
            bool keyPausedNow = (keyboardState.IsKeyDown(Keys.P) ||
                keyboardState.IsKeyDown(Keys.Space));

            if (!keyPaused && keyPausedNow)
                paused = !paused;

            keyPaused = keyPausedNow;
        }
    }

    public delegate void MoveFunction();

}
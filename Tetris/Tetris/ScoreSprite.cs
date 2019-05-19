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
using System.IO;

namespace TetrisGame
{
    /// Classen ritar och uppdaterar poängen
    public class ScoreSprite : DrawableGameComponent
    {
        Score score;
        private SpriteFont font;
        private SpriteFont fontTitle;
        private SpriteFont fontEm;
        private Game1 game;
        private SpriteBatch spriteBatch;
        private String gameover = "";
        private Color fontColour = Color.Silver;
        //the previous high score
        int highScore = 0;

        /// initialiserar poängsprite objektet
        public ScoreSprite(Game1 game, Score score) : base(game)
        {
            this.game = game;
            this.score = score;
            //save the high score to a file when the game is over.
            game.Exiting += saveResults;
        }

        /// låter initialisering hända innan spelet börjar
        public override void Initialize()
        {
            base.Initialize();
        }

        /// laddar all content som behövs för spelet
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = game.Content.Load<SpriteFont>("scoreFont");
            fontTitle = game.Content.Load<SpriteFont>("scoreFontTitle");
            fontEm = game.Content.Load<SpriteFont>("scoreFontEm");
            findHighScore();

            base.LoadContent();
        }

        /// ritar allt som behövs
        public override void Draw(GameTime gameTime)
        {
            Color baseTxt = Color.White;
            Color emTxt = Color.SeaGreen;

            spriteBatch.Begin();
            spriteBatch.DrawString(fontEm,
                "Poang: " + score.ScoreValue + "\nNiva: " + score.Level + "\nKlarade linjer: "
                + score.Lines + gameover, new Vector2(20, GraphicsDevice.Viewport.Height - 90), fontColour);
            spriteBatch.DrawString(fontTitle, "TETRIS", new Vector2(75, 15), Color.LimeGreen);

            spriteBatch.DrawString(font, "Nasta form:", new Vector2(230, 80), baseTxt);
            spriteBatch.DrawString(font, "Hogsta poang:", new Vector2(230, 180), baseTxt);
            spriteBatch.DrawString(fontTitle, "" + highScore, new Vector2(230, 200), emTxt);

            spriteBatch.DrawString(font, "For att pausa/fortsatta\nspelet tryck ", new Vector2(230, 250), baseTxt);
            spriteBatch.DrawString(fontEm, "P", new Vector2(365, 270), emTxt);
            spriteBatch.DrawString(font, "key", new Vector2(380, 270), baseTxt);
            spriteBatch.DrawString(font, "eller A", new Vector2(230, 290), baseTxt);
            spriteBatch.DrawString(fontEm, "SPACEBAR.", new Vector2(270, 290), emTxt);

            spriteBatch.DrawString(font, "For att ga in i spoklage\ntryck", new Vector2(230, 330), baseTxt);
            spriteBatch.DrawString(fontEm, "G", new Vector2(285, 350), emTxt);
            spriteBatch.DrawString(font, "key.", new Vector2(305, 350), baseTxt);


            spriteBatch.DrawString(font, "For att slappa formen\ntryck", new Vector2(230, 390), baseTxt);
            spriteBatch.DrawString(fontEm, "ENTER.", new Vector2(285, 410), emTxt);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Displays the game over message.
        public void HandleGameOver()
        {
            gameover = "\nGAME OVER";
            fontColour = Color.Red;
        }

        //Determines the previous high score from the file.
        private void findHighScore()
        {
            try
            {
                if (!File.Exists("score.txt"))
                {
                    File.WriteAllLines("score.txt", new string[1] { "0" });
                }
                else
                {
                    string[] array = File.ReadAllLines("score.txt");
                    highScore = Int32.Parse(array[0]);
                }
            }
            catch (IOException io)
            { }
        }

        //If the current score is higher than the previous high score, saves it to the file.
        private void saveResults(object sender, EventArgs e)
        {
            if (score.ScoreValue > highScore)
            {
                try
                {
                    File.WriteAllLines("score.txt", new string[1] { score.ScoreValue + "" });
                }
                catch (IOException io)
                { }
            }
        }
    }
}

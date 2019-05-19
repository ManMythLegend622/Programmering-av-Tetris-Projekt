using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TetrisLibrary;

namespace TetrisGame
{
    public class Spel1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BoardSprite boardSprite;
        ShapeSprite shapeSprite;
        ScoreSprite scoreSprite;

        public Spel1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// Gör att spelet kan ladda allt det behöver innan det startas.
        protected override void Initialize()
        {
            Board board = new Board();
            Score score = new Score(board);

            boardSprite = new BoardSprite(this, board);
            Components.Add(boardSprite);
            shapeSprite = new ShapeSprite(this, board, score);
            Components.Add(shapeSprite);
            scoreSprite = new ScoreSprite(this, score);
            Components.Add(scoreSprite);

            graphics.PreferredBackBufferHeight = 555;
            graphics.PreferredBackBufferWidth = 450;
            graphics.ApplyChanges();

            base.Initialize();
            board.GameOver += gameOver;
        }

        /// Laddar allt content
        protected override void LoadContent()
        {
            // Gör att man kan rita texturer
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// avladdar allt spel specifikt content
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// låter spelet updatera logiken som kollar kollisioner med mera.
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// kallas när spelet ska rita sig själv
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(10, 10, 10));

            base.Draw(gameTime);
        }

        //När spelet är över försvinner formen
        private void gameOver()
        {
            Components.Remove(shapeSprite);
            scoreSprite.HandleGameOver();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AllInOne
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ActionScene : GameScene
    {
        Game game;
        private CollisionManager cm;
        private bool isDead = false;
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }
        private SpriteBatch spriteBatch;
        private SpaceShip firstPlayerShip;
        private Texture2D spaceBackground;
        private RegularAsteroid regAsteriod;
        private Rectangle mainFrame;
        private float asteroidSpeed = 0.2f;
        private Vector2 position;
        private float speed = 0.1f;
        List<RegularAsteroid> allAsteroids = new List<RegularAsteroid>();
        List<RegularAsteroid> resetAsteroids = new List<RegularAsteroid>();
        private Texture2D shipTex;
        private RegularAsteroid thisAsteroid;
        private int delay = 0;
        private SpriteFont scoreFont;
        private SimpleString simple;
        private Vector2 scorePos;
        private int score = 0;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        private string message;
        private SimpleString deadMessage;

        public SimpleString DeadMessage
        {
            get { return deadMessage; }
            set { deadMessage = value; }
        }
        private Vector2 deadMessagePos;
        public List<RegularAsteroid> AllAsteroids
        {
            get { return allAsteroids; }
            set { allAsteroids = value; }
        }
        private LoseScreen ls;
        private Game1 gm;

        private float xMove = 0;
        private float yMove = 0;

        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.game = game;
            
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            ls = new LoseScreen(game, spriteBatch);
            gm = new Game1();
            scoreFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            scorePos = new Vector2(Shared.stage.X - 1150, 550);
            simple = new SimpleString(game, spriteBatch, scoreFont, scorePos, message, Color.White);
            this.Components.Add(simple);
            simple.Message = "Score: ";
            shipTex = game.Content.Load<Texture2D>("Sprites/spaceShip");

            deadMessage = new SimpleString(game, spriteBatch, scoreFont, deadMessagePos, message, Color.White);
            deadMessagePos = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            deadMessage.Message = "";
            this.Components.Add(deadMessage);

            firstPlayerShip = new SpaceShip(game, spriteBatch,
            game.Content.Load<Texture2D>("Sprites/spaceShip"), this);
            this.Components.Add(firstPlayerShip);

            spaceBackground = game.Content.Load<Texture2D>("Images/background");
            mainFrame = new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y);

            //SmallFasterAsteroids - (124,101)
            Texture2D regularAsteroid = game.Content.Load<Texture2D>("Sprites/RegularAsteroidSprites");
            Vector2 zeroPos = Vector2.Zero;
            int delay = 25;
            regAsteriod = new RegularAsteroid(game, spriteBatch, regularAsteroid, asteroidSpeed,
                zeroPos, delay, 15);

            cm = new CollisionManager(game, regAsteriod,
                firstPlayerShip, this, spriteBatch);
            this.Components.Add(cm);
            CreateAsteroids();

            xMove = thisAsteroid.Position.X;
            yMove = thisAsteroid.Position.Y;

        }
        private void CreateAsteroids()
        {
            Random Random = new Random();
            int screenWidth = (int)Shared.stage.X;
            int screenHeight = (int)Shared.stage.Y;
            if (isDead == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    float xPosition = Random.Next(screenWidth - 150, screenWidth);
                    float yPosition = Random.Next(1, 400);
                    regAsteriod.Position = new Vector2(xPosition, yPosition);
                    thisAsteroid = new RegularAsteroid(game, spriteBatch, game.Content.Load<Texture2D>("Sprites/RegularAsteroidSprites"), speed, regAsteriod.Position, 10, 3f);
                    allAsteroids.Add(thisAsteroid);
                    this.Components.Add(allAsteroids.Last<RegularAsteroid>());
                }

                foreach (var item in allAsteroids)
                {
                    if (firstPlayerShip.IsResetting == true)
                    {
                        item.Visible = false;
                        item.Enabled = false;
                        item.Position = new Vector2(0, 3000);
                        firstPlayerShip.Enabled = true;
                        firstPlayerShip.Visible = true;
                        cm.Played = false;
                    }
                }
                firstPlayerShip.IsResetting = false;
            }
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape))
            {
                firstPlayerShip.resetAll();
                this.Game.Exit();
            }
            
            MediaPlayer.Resume();
            if (isDead == true)
            {
                deadMessage.Message = "You have died. Press Escape to try again.";
                deadMessage.Position = new Vector2(Shared.stage.X / 2 - 250, 0);
            }

            if (delay % 60 == 0)
            {
                CreateAsteroids();
                if (isDead == false)
                {
                    score += 10;
                }
            }
            delay++;
            simple.Message = "Score: " + score.ToString();
            base.Update(gameTime);
        }
        public void AllReset()
        {
            firstPlayerShip.resetAll();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spaceBackground, mainFrame, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X + 15, (int)position.Y + 32, 25, 40);
        }
    }
}

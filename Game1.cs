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

//http://stackoverflow.com/questions/11382101/how-to-show-lots-of-sprites-from-one-texture-and-have-them-move-at-intervals-xna
namespace AllInOne
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Declare all scenes here....
        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene actionScene;
        private HowToPlay howToPlayScene;
        private AboutScene aboutScene;

        private Song backgroundMusic;
        private bool playingGame = false;

        public bool PlayingGame
        {
            get { return playingGame; }
            set { playingGame = value; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //this.spaceShip = spaceShip;
            // TODO: use this.Content to load your game content here

            // create all scenes here and add to Components list.
            // make only startScene active
            Vector2 stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            howToPlayScene = new HowToPlay(this, spriteBatch);
            this.Components.Add(howToPlayScene);

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);

            backgroundMusic = Content.Load<Song>("Sounds/BackGroundMusic");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;

            startScene.show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        public void hideAll()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    bool whistleSound = false;
                    hideAll();
                    actionScene.show();
                    whistleSound = true;
                    SoundEffect.MasterVolume = 0.1f;
                    MediaPlayer.Play(backgroundMusic);
                    if (playingGame == true && whistleSound == true)
                    {
                        MediaPlayer.Resume();
                    }
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAll();
                    helpScene.show();
                }

                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAll();
                    howToPlayScene.show();
                }

                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAll();
                    aboutScene.show();
                }

                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (helpScene.Enabled || actionScene.Enabled || howToPlayScene.Enabled || aboutScene.Enabled) // || add others
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAll();
                    startScene.show();
                    actionScene.AllReset();
                    MediaPlayer.Stop();
                }

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
       
    }
}

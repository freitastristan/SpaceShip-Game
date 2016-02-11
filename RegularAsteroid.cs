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
    public class RegularAsteroid : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private float asteroidSpeed;

        public float AsteroidSpeed
        {
            get { return asteroidSpeed; }
            set { asteroidSpeed = value; }
        }

        private List<Rectangle> frames;
        private int frameIndex = 0;
        private int delay;
        private int delayCounter;
        //private int asteroidSpeed = 50;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public RegularAsteroid(Game game, SpriteBatch spriteBatch, Texture2D tex, float speed,
            Vector2 position, int delay, float asteroidSpeed)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.delay = delay;
            this.position = position;
            dimension = new Vector2(125, 102);
            this.asteroidSpeed = asteroidSpeed;
            createFrames();
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);

                    frames.Add(r);
                }
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
            position += new Vector2(-asteroidSpeed, 0);

            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > 4)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
               spriteBatch.Draw(tex, position, frames.ElementAt<Rectangle>(frameIndex), Color.White); 
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X + 47, (int)position.Y + 55, 12, 7);
        }
    }
}

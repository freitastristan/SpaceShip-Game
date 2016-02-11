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
    public class SpaceShip : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private bool isResetting = false;

        public bool IsResetting
        {
            get { return isResetting; }
            set { isResetting = value; }
        }
        private ActionScene ac;
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        float speed = 5f;
        public SpaceShip(Game game, SpriteBatch spriteBatch, Texture2D tex, ActionScene ac)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            position = new Vector2(Shared.stage.X / 2 - tex.Width,
                Shared.stage.Y /2 - tex.Height);
            this.ac = ac;
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
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += speed;
                if (position.X > Shared.stage.X - tex.Width)
                {
                    position.X = Shared.stage.X - tex.Width;
                }
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= speed;
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up))
            {
                position.Y -= speed;
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                position.Y += speed;
                if (position.Y > 400)
                {
                    position.Y = 400;
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }

        public void resetAll()
        {
            Position = new Vector2(Shared.stage.X / 2 - tex.Width,
            Shared.stage.Y / 2 - tex.Height);
            this.Visible = false;
            this.Enabled = false;
            ac.IsDead = false;
            ac.Score = 0;
            ac.DeadMessage.Message = "";
            isResetting = true;
        }
    }
}

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
    public class Shooting : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public bool isVisible = true;
        const int MAX_DISTANCE = 500;

        private Vector2 mStartPosition;
        private Vector2 mSpeed;
        private Vector2 mDirection;
        SpaceShip spaceShip;
        public Shooting(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
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
            if (Vector2.Distance(mStartPosition, spaceShip.Position) > MAX_DISTANCE)
            {
                isVisible = false;
            }
            if (Visible == true)
            {
                base.Update(gameTime);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            if (isVisible == true)
            {
               base.Draw(gameTime);
            }
            
        }
        
        public void Fire(Vector2 theStartPosition, Vector2 theSpeed, Vector2 theDirection)
        {
            spaceShip.Position = theStartPosition;
            mStartPosition = theStartPosition;
            mSpeed = theSpeed;
            mDirection = theDirection;
            isVisible = true;
        }
    }
}

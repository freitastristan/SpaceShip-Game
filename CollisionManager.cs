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
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private SpriteBatch spriteBatch;
        private RegularAsteroid regAsteroid;
        private SpaceShip ship;
        private Explosion exp;
        private Texture2D explosionSprite;
        private ActionScene ac;
        private LoseScreen ls;
        private SoundEffect boom;
        private bool played = false;
        private int delay = 0;

        public bool Played
        {
            get { return played; }
            set { played = value; }
        }

        public CollisionManager(Game game, RegularAsteroid regAsteroid,
            SpaceShip ship, ActionScene ac, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.regAsteroid = regAsteroid;
            this.ship = ship;
            this.ac = ac;
            this.spriteBatch = spriteBatch;
            boom = game.Content.Load<SoundEffect>("Sounds/finalexplosion");
            explosionSprite = game.Content.Load<Texture2D>("Sprites/explosion");
            exp = new Explosion(game, spriteBatch, explosionSprite, regAsteroid.Position, 5);
            ac.Components.Add(exp);
            ls = new LoseScreen(game, spriteBatch);


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
            Rectangle regAsteroidRect = regAsteroid.getBounds();
            //Rectangle fstAsteroidRect = fstAsteroid.getBounds();
            Rectangle shipRect = ship.getBounds();
            List<Rectangle> listRect = new List<Rectangle>();

            foreach (RegularAsteroid item in ac.AllAsteroids)
            {
                if (shipRect.Intersects(item.getBounds()))
                {
                    exp.Enabled = true;
                    exp.Visible = true;
                    exp.Position = ship.Position;
                    exp.Dead = true;
                    ship.Enabled = false;
                    ship.Visible = false;
                    ac.IsDead = true;
                    ship.Position = new Vector2(0, 10000);

                    if (played == false)
                    {
                        boom.Play();
                        played = true;
                    }
                }
            }
            
            base.Update(gameTime);
        }
    }
}

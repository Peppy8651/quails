using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;

namespace quails
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        readonly bool test = true;
        Texture2D quail;
        Vector2 quailpos;
        float quailspeed;
        float scalex;
        float scaley;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            quailpos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            quailspeed = 200f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            if (test) {
                quail = Content.Load<Texture2D>("pollo");
                scalex = (_graphics.PreferredBackBufferWidth / 5) / quail.Width; // Make the quail texture scaled up to a 5th of the screen
                scaley = (_graphics.PreferredBackBufferHeight / 5) / quail.Height;
                scalex = (scalex + scaley) / 2;
                scaley = scalex; // To prevent stretching, find a number in the middle and use it to scale
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (test == true)
            {
                TestMovement(gameTime);
            }
            base.Update(gameTime);
        }
        protected void TestMovement(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                quailpos.Y -= quailspeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                quailpos.Y += quailspeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                quailpos.X -= quailspeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                quailpos.X += quailspeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (quailpos.X > _graphics.PreferredBackBufferWidth - (quail.Width * scalex) / 2)
            {
                quailpos.X = _graphics.PreferredBackBufferWidth - (quail.Width * scalex) / 2;
            }
            else if (quailpos.X < (quail.Width * scalex) / 2)
            {
                quailpos.X = (quail.Width * scalex) / 2;
            }


            if (quailpos.Y > _graphics.PreferredBackBufferHeight - (quail.Height * scaley) / 2)
            {
                quailpos.Y = _graphics.PreferredBackBufferHeight - (quail.Height * scaley) / 2;
            }
            else if (quailpos.Y < (quail.Height * scaley) / 2)
            {
                quailpos.Y = (quail.Height * scaley) / 2;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // turn off anti-aliasing
            _spriteBatch.Draw(quail, quailpos, null, Color.White, 0f, new Vector2(quail.Width / 2, quail.Height / 2), new Vector2(scalex, scaley), SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace quails
{
    public class Quail : Unit
    {
        public Quail(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            speed = 0.2f;
            jumpTimer = new MyTimer(1500);
            jumpMomentum = 1f;
        }
        public override void Update(Vector2 OFFSET)
        {
            bool checkScroll = false;
            if (Globals.keyboard.GetPress("A"))
            {
                pos = new Vector2(pos.X - speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds, pos.Y);
                checkScroll = true;
            }
            if (Globals.keyboard.GetPress("D"))
            {
                pos = new Vector2(pos.X + speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds, pos.Y);
                checkScroll = true;
            }
            //if (Globals.keyboard.GetPress("W"))
            //{
            //    pos = new Vector2(pos.X, pos.Y - speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds);
            //    checkScroll = true;
            //}
            //if (Globals.keyboard.GetPress("S"))
            //{
            //    pos = new Vector2(pos.X, pos.Y + speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds);
            //    checkScroll = true;
            //}
            if (Globals.keyboard.GetPress("Space") && jump == false)
            {
                jump = true;
                jumpMomentum = 40f;
            }
            if (jump)
            {
                pos = new Vector2(pos.X, pos.Y - (speed * jumpMomentum));
                jumpMomentum -= (float) Globals.gameTime.ElapsedGameTime.TotalMilliseconds / 10;
                checkScroll = true;
            }

            jumpTimer.UpdateTimer();
            if (jumpTimer.Test())
            {
                jump = false;
                jumpMomentum = 2f;
                checkScroll = false;
                jumpTimer.ResetToZero();
            }
            if (checkScroll)
            {
                GameGlobals.CheckScroll(pos);
            }
            // rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET); the top of the sprite faces the mouse
            if (Globals.mouse.LeftClick())
            {
                GameGlobals.PassProjectile(new Fireball(new Vector2(pos.X, pos.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
            }



            base.Update(OFFSET);
        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}

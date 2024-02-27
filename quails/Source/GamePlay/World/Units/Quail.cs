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

        public bool hitsGround = false;
        bool checkScroll = false;
        public MyTimer holdTimer = new MyTimer(1500);

        public Quail(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            speedX = 0.2f;
            speedY = 0.55f;
        }
        public override void Update(Vector2 OFFSET)
        {
            if (Globals.keyboard.GetPress("A") && jump != JumpType.HOLDING)
            {
                pos = new Vector2(pos.X - speedX * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds, pos.Y);
                checkScroll = true;
                flip = true;
            }
            if (Globals.keyboard.GetPress("D") && jump != JumpType.HOLDING)
            {
                pos = new Vector2(pos.X + speedX * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds, pos.Y);
                checkScroll = true;
                flip = false;
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
            checkJump();

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
        public virtual void checkJump()
        {
            if (Globals.keyboard.GetPress("Space") && Globals.keyboard.GetPress("S") && jump != JumpType.NORMAL && jump != JumpType.HOLD)
            {
                if (!Globals.keyboard.GetPress("A") && !Globals.keyboard.GetPress("D")) // hold jump
                {
                    if (holdTimer.Test()) // after being held for 1.5 seconds
                    {
                        holdTimer.ResetToZero();
                        jump = JumpType.HOLD;
                        speedY = 0.75f;
                        speedX = 0.1f;
                        hitsGround = false;
                    }
                    else // in process, update timer
                    {
                        jump = JumpType.HOLDING;
                        holdTimer.UpdateTimer();
                    }
                }
            }
            else if (jump == JumpType.HOLDING) // stop holding space
            {
                jump = JumpType.NONE;
                holdTimer.ResetToZero();

            }
            if (Globals.keyboard.GetPress("Space") && jump == JumpType.NONE)
            {
                    jump = JumpType.NORMAL;
                    hitsGround = false;
            }
            if (jump == JumpType.HOLD)
            {
                pos = new Vector2(pos.X, pos.Y - (speedY * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds));
                checkScroll = true;
                speedY -= 0.00095f * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (jump == JumpType.NORMAL)
            {
                pos = new Vector2(pos.X, pos.Y - (speedY * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds));
                checkScroll = true;
                speedY -= 0.0009f * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (hitsGround)
            {
                jump = JumpType.NONE;
                speedY = 0.55f;
                speedX = 0.2f;
                checkScroll = false;
            }
        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}

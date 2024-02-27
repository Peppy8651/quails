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
    public enum JumpType
    {
        NORMAL,
        HOLD,
        HOLDING,
        NONE
    }
    public class Unit : Basic2D
    {
        public bool dead;
        public JumpType jump;
        public MyTimer jumpTimer;
        public float speedX, hitDist, speedY;
        public Unit(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            dead = false;
            jump = JumpType.NONE;
            speedX = 2.0f;
            hitDist = 35.0f;
        }
        public override void Update(Vector2 OFFSET)
        {
            
            base.Update(OFFSET);
        }
        public virtual void GetHit()
        {
            dead = true;
        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}

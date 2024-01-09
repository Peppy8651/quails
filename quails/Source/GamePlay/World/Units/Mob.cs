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
    public class Mob : Unit
    {
        public Mob(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            speed = 2.0f;
        }
        public virtual void Update(Vector2 OFFSET, Quail QUAIL)
        {
            AI(QUAIL);

            base.Update(OFFSET);
        }
        public virtual void AI(Quail QUAIL)
        {
            pos += Globals.RadialMovement(QUAIL.pos, pos, speed);
            rot = Globals.RotateTowards(pos, QUAIL.pos);
        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}

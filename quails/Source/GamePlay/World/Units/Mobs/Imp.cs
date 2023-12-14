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
    public class Imp : Mob
    {
        public Imp(Vector2 POS) : base("2d/units/mobs/mob", POS, new Vector2(128, 128))
        {
            speed = 2.0f;
        }
        public override void Update(Vector2 OFFSET, Quail QUAIL)
        {
            base.Update(OFFSET, QUAIL);
        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}

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
    public class LevelElement : Basic2D
    {
        public LevelElement(string NAME, Vector2 POS, Vector2 DIMS) : base("2d/level/" + NAME, POS, DIMS)
        {

        }
        public override void Update(Vector2 OFFSET)
        {

            base.Update(OFFSET);

        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }

    }
}

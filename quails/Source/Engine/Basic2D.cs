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
    public class Basic2D
    {
        public float rot;
        public Vector2 pos, dims;
        public Texture2D tex;

        public Basic2D(string PATH, Vector2 POS, Vector2 DIMS)
        {
            pos = POS;
            dims = DIMS;
            tex = Globals.content.Load<Texture2D>(PATH);
        }
        public virtual void Update()
        {

        }
        public virtual void Draw(Vector2 OFFSET)
        {
            if (tex != null) {
                Globals.spriteBatch.Draw(tex, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White, rot, new Vector2(tex.Bounds.Width / 2, tex.Bounds.Height / 2), new SpriteEffects(), 0);
            }
        }
        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN)
        {
            if (tex != null)
            {
                Globals.spriteBatch.Draw(tex, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White, rot, new Vector2(ORIGIN.X, ORIGIN.Y), new SpriteEffects(), 0);
            }
        }
    }
}

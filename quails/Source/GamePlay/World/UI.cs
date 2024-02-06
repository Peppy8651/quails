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
    public class UI 
    {
        public SpriteFont font;
        public UI()
        {
            font = Globals.content.Load<SpriteFont>("fonts/font");

        }
        public void Update(World WORLD)
        {

        }
        public void Draw(World WORLD)
        {
            string tempStr = "Killed = " + WORLD.numKilled;
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight - 40), Color.Black);
        }
    }
}

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
    public class BasicLevel
    {
        public string Name;
        public string Scale;
        public string QuailColor;
        public float LevelStart;
        public float LevelEnd;
        public List<Floor> Floors;

        public BasicLevel()
        {
        }
        public virtual void Update(Vector2 OFFSET)
        {
        }
        public virtual void Draw(Vector2 OFFSET)
        {
            for (int i = 0; i < Floors.Count; i++)
            {
                Floor floor = Floors[i];
                floor.Draw(OFFSET);
            }
        }
    }
}

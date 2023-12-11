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
    public class World
    {
        public Vector2 offset;
        public Quail quail;
        public List<Projectile2D> projectiles = new List<Projectile2D>();
        public World()
        {
            quail = new Quail("2d/pollo", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(120, 104));

            GameGlobals.PassProjectile = AddProjectile;
            offset = new Vector2(0, 0);
        }
        public virtual void Update()
        {
            quail.Update();

            for(int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, null);
                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }


        }
        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2D)INFO);
        }
        public virtual void Draw(Vector2 OFFSET)
        {
            quail.Draw(OFFSET);
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }
        }
    }
}

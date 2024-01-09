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
        public int numKilled;
        public Vector2 offset;
        public Quail quail;
        public UI ui;
        public List<Projectile2D> projectiles = new List<Projectile2D>();
        public List<Mob> mobs = new List<Mob>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public World()
        {
            numKilled = 0;
            quail = new Quail("2d/pollo", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(120, 104));

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            GameGlobals.CheckScroll = CheckScroll;

            offset = new Vector2(0, 0);
            spawnPoints.Add(new SpawnPoint("2d/misc/idk", new Vector2(50, 50), new Vector2(128, 128)));
            spawnPoints.Add(new SpawnPoint("2d/misc/idk", new Vector2(Globals.screenWidth / 2, 50), new Vector2(128, 128)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
            spawnPoints.Add(new SpawnPoint("2d/misc/idk", new Vector2(Globals.screenWidth - 50, 50), new Vector2(128, 128)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(2000);

            ui = new UI();
        }
        public virtual void Update()
        {
            quail.Update(offset);
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(offset);
            }
            for(int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, mobs.ToList<Unit>());
                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
            for(int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Update(offset, quail);
                if (mobs[i].dead)
                {
                    numKilled++;
                    mobs.RemoveAt(i);
                    i--;
                }
            }

            ui.Update(this);
        }
        public virtual void AddMob(object INFO)
        {
            mobs.Add((Mob) INFO);
        }
        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2D)INFO);
        }
        public virtual void CheckScroll(object INFO) 
        {
            Vector2 tempPos = (Vector2) INFO;
            if (tempPos.X < -offset.X + (Globals.screenWidth * .4f))
            {
                offset = new Vector2(offset.X + quail.speed * 2, offset.Y);
            }
            if (tempPos.X > -offset.X + (Globals.screenWidth * .6f))
            {
                offset = new Vector2(offset.X - quail.speed * 2, offset.Y);
            }
            if (tempPos.Y < -offset.Y + (Globals.screenHeight * .4f))
            {
                offset = new Vector2(offset.X, offset.Y + quail.speed * 2);
            }
            if (tempPos.Y > -offset.Y + (Globals.screenHeight * .6f))
            {
                offset = new Vector2(offset.X, offset.Y - quail.speed * 2);
            }
        }
        public virtual void Draw(Vector2 OFFSET)
        {
            quail.Draw(offset);
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(offset);
            }
            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Draw(offset);
            }

            ui.Draw(this);
        }
    }
}

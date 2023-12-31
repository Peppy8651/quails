﻿using System;
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
        public List<Mob> mobs = new List<Mob>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public World()
        {
            quail = new Quail("2d/pollo", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(120, 104));

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            offset = new Vector2(0, 0);
            spawnPoints.Add(new SpawnPoint("2d/misc/idk", new Vector2(50, 50), new Vector2(128, 128)));
            spawnPoints.Add(new SpawnPoint("2d/misc/idk", new Vector2(Globals.screenWidth / 2, 50), new Vector2(128, 128)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
            spawnPoints.Add(new SpawnPoint("2d/misc/idk", new Vector2(Globals.screenWidth - 50, 50), new Vector2(128, 128)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(2000);

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
                    mobs.RemoveAt(i);
                    i--;
                }
            }


        }
        public virtual void AddMob(object INFO)
        {
            mobs.Add((Mob) INFO);
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
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(offset);
            }
            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Draw(offset);
            }
        }
    }
}

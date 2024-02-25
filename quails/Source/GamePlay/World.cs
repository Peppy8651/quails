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
using System.IO;

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
        public BasicLevel currentLevel;
        public World()
        {
            numKilled = 0;
            quail = new Quail("2d/pollo", new Vector2(0, 300), new Vector2(120, 104));

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
            LoadLevel();
        }
        public virtual void Update()
        {
            currentLevel.Update(offset);
            quail.Update(offset);
            for(int i = 0; i < spawnPoints.Count; i++)
             {
                spawnPoints[i].Update(offset);
            }
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, mobs.ToList<Unit>());
                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Update(offset, quail);
                if (mobs[i].dead)
                {
                    numKilled++;
                    mobs.RemoveAt(i);
                    i--;
                }
            }
            if (quail.pos.Y > 236)
            {
                quail.pos = new Vector2(quail.pos.X, 236);
            }
            if (quail.pos.X + (quail.dims.X / 2) > currentLevel.LevelEnd)
            {
                quail.pos.X -= quail.speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (quail.pos.X + (quail.dims.X / 2 )< currentLevel.LevelStart)
            {
                quail.pos.X += quail.speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
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
                offset = new Vector2(offset.X + quail.speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds, offset.Y);
            }
            if (tempPos.X > -offset.X + (Globals.screenWidth * .6f))
            {
                offset = new Vector2(offset.X - quail.speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds, offset.Y);
            }
            if (tempPos.Y < -offset.Y + (Globals.screenHeight * .4f))
            {
                offset = new Vector2(offset.X, offset.Y + quail.speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds);
            }
            if (tempPos.Y > -offset.Y + (Globals.screenHeight * .6f))
            {
                offset = new Vector2(offset.X, offset.Y - quail.speed * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds);
            }
        }
        public virtual void Draw(Vector2 OFFSET)
        {
            currentLevel.Draw(offset);
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
        public virtual void LoadLevel()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            XElement LevelXML = XElement.Load(currentDirectory + "/Content/2d/level/level.xml");
            var name = LevelXML.Elements().ElementAt(0);
            var scale = LevelXML.Elements().ElementAt(1);
            var color = LevelXML.Elements().ElementAt(2);
            var start = LevelXML.Elements().ElementAt(3);
            var end = LevelXML.Elements().ElementAt(4);
            var floorsList = LevelXML.Elements().ElementAt(5).Elements();

            currentLevel = new BasicLevel
            {
                Name = name.Value,
                Scale = scale.Value,
                QuailColor = color.Value,
                LevelStart = float.Parse(start.Value),
                LevelEnd = float.Parse(end.Value),
                Floors = new List<Floor>()
            };
            for (int i = 0; i < floorsList.Count(); i++)
            {
                var thisFloor = floorsList.ElementAt(i).Elements();
                string thisName = thisFloor.ElementAt(0).Value;
                Vector2 thisPos = new Vector2(float.Parse(thisFloor.ElementAt(1).FirstAttribute.Value), float.Parse(thisFloor.ElementAt(1).LastAttribute.Value));
                Vector2 thisDims = new Vector2(float.Parse(thisFloor.ElementAt(2).FirstAttribute.Value), float.Parse(thisFloor.ElementAt(2).LastAttribute.Value));
                Floor floor = new Floor(thisName, thisPos, thisDims);
                currentLevel.Floors.Add(floor);
            }
            System.Diagnostics.Debug.WriteLine(LevelXML);
        }
    }
}

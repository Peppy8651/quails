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
using System.Collections.Generic;

namespace quails
{
    public class Projectile2D : Basic2D
    {
        public bool done;
        public float speed;
        public Vector2 direction;
        public Unit owner;
        public MyTimer timer;
        public Projectile2D(string PATH, Vector2 POS, Vector2 DIMS, Unit OWNER, Vector2 TARGET) : base(PATH, POS, DIMS)
        {
            done = false;

            speed = 5.0f;

            owner = OWNER;

            rot = Globals.RotateTowards(pos, new Vector2(TARGET.X, TARGET.Y));

            direction = TARGET - owner.pos;
            direction.Normalize();

            timer = new MyTimer(1200);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            pos += direction * speed;


            timer.UpdateTimer();
            if (timer.Test())
            {
                done = true;
            }
            if (HitSomething(UNITS))
            {
                done = true;
            }
        }
        public virtual bool HitSomething(List<Unit> UNITS)
        {
            return false;
        }
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}

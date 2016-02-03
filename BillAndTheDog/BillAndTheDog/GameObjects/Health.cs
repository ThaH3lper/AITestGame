using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class Health : GameObject
    {
        public Health(SimulationWorld world, Vector2 location) : base(world, new Rectangle(0, 0, 10, 10), new Rectangle(0, 0, 1, 1), Globals.pixel)
        {
            recHit.X = (int)(location.X - recHit.Width / 2);
            recHit.Y = (int)(location.Y - recHit.Width / 2);
            color = Color.Red;
        }

        public override void Update(float delta)
        {
            Entity hited = world.GetEntityInRectangle(recHit);
            if(hited != null)
            {
                hited.AddHealth(20);
                world.Remove(this);
            }
        }

        public Vector2 GetPosition(){
            return new Vector2(recHit.X, recHit.Y);
        }
    }
}

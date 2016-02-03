using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class Bullet : GameObject
    {
        Vector2 location, direction;
        float speed;
        Entity owner;

        public Bullet(SimulationWorld world, Vector2 location, Vector2 direction, Entity owner) : base(world, new Rectangle(0, 0, 5, 5), Globals.pixel)
        {
            this.location = location;
            this.direction = direction;
            this.owner = owner;
            speed = 400;
        }

        public void Update(float delta)
        {
            location += delta * direction * speed;
            recDraw.X = (int)(location.X - recDraw.Width / 2);
            recDraw.Y = (int)(location.Y - recDraw.Height / 2);
        }
    }
}

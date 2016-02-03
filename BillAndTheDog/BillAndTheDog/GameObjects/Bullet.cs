﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class Bullet : GameObject
    {
        Entity entity;
        Vector2 location, direction;
        float speed;

        public Bullet(SimulationWorld world, Entity entity, Vector2 location, Vector2 direction) : base(world, new Rectangle(0, 0, 5, 5), new Rectangle(0, 0, 1, 1), Globals.pixel)
        {
            this.entity = entity;
            this.location = location;
            this.direction = direction;
            speed = 400;
        }

        public override void Update(float delta)
        {
            location += direction * delta * speed;
            recHit.X = (int)(location.X - recHit.Width / 2);
            recHit.Y = (int)(location.Y - recHit.Height / 2);
            Entity hited = world.GetEntityInRectangle(recHit);
            if (hited != null)
            {
                if (entity is Ai && hited is Ai)
                    return;
                if (entity is Player && hited is Player)
                    return;
                world.Remove(this);
                hited.Damage(10);
            }
        }
    }
}
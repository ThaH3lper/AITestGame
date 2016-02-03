using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class Ai : Entity
    {
        private float closeValue = 5;
        public Ai(SimulationWorld world, Vector2 location) : base(world, location)
        {
            color = Color.FromNonPremultiplied(Globals.random.Next(255), Globals.random.Next(255), Globals.random.Next(255), 255);
        }

        public bool MoveTo(Vector2 position)
        {
            if (location.X + closeValue > position.X && location.X - closeValue < position.X
                && location.Y + closeValue > position.Y && location.Y - closeValue < position.Y)
                return true;

            Vector2 direction = position - location;
            direction.Normalize();
            Move(direction);
            return false;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
        }
    }
}

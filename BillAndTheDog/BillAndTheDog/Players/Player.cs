using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class Player : Entity
    {
        public Player(SimulationWorld world, Vector2 location) : base(world, location)
        {

        }

        public override void Update(float delta)
        {
            base.Update(delta);
            Vector2 direction = new Vector2(0, 0);
            if (KeyMouseReader.KeyPress(Keys.A))
                direction.X -= 1;
            if (KeyMouseReader.KeyPress(Keys.D))
                direction.X += 1;
            if (KeyMouseReader.KeyPress(Keys.W))
                direction.Y -= 1;
            if (KeyMouseReader.KeyPress(Keys.S))
                direction.Y += 1;
            if (KeyMouseReader.LeftClick())
                FireAt(KeyMouseReader.GetMousePos());
            Move(direction);
        }
    }
}

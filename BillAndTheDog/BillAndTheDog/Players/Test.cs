using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class Test : Ai
    {
        Random r;
        Vector2 targetPos;
        public Test(SimulationWorld world, Vector2 location, Random r) : base(world, location)
        {
            this.r = r;
            targetPos = new Vector2(r.Next(1280), r.Next(720));
        }

        public override void Update(float delta)
        {
            if(MoveTo(targetPos))
            {
                targetPos = new Vector2(r.Next(1280), r.Next(720));
                FireAt(world.player.GetLocation());
                Talk(Globals.GetRandomWarning(), Color.Red);
            }
            base.Update(delta);
        }
    }
}

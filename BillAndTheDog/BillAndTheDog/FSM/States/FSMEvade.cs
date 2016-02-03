using BillAndTheDog.FuSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BillAndTheDog.FSM.States
{
    class FSMEvade : FSMState
    {

        public FSMEvade(AIController controller) : base(FSMEnum.Evade, controller)
        {

        }

        public override void Update(float delta)
        {
            Vector2 temp = controller.GetLocation() - controller.playerPosition;
            temp.Normalize();
            temp *= 500;
            Vector2 newPos = temp + controller.GetLocation();
            controller.MoveTo(newPos);
        }

        public override FSMEnum CheckTransitions()
        {
            if (controller.distanceToPlayer > 500)
            {
                nextState = FSMEnum.Random;
            }

            return base.CheckTransitions();
        }
    }
}

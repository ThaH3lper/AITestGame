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
        private float evadeTime = 0;    //Delay for this state.

        public FSMEvade(FAIController controller) : base(FSMEnum.Evade, controller){ }

        public override void Update(float delta)
        {
            //Calculate an evade point to move to. Is always away from player.
            Vector2 temp = controller.GetLocation() - controller.playerPosition;
            temp.Normalize();
            temp *= 500;
            Vector2 newPos = temp + controller.GetLocation();

            //Move to the new evade point.
            controller.MoveTo(newPos);

            //Update evadeTime so we can evade for x amount of time.
            evadeTime += delta;
        }

        public override FSMEnum CheckTransitions()
        {
            if (evadeTime > 2f)
            {
                evadeTime = 0; //Reset evadeTime.
                nextState = FSMEnum.Random;
            }

            return base.CheckTransitions();
        }
    }
}

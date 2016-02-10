using BillAndTheDog.FuSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BillAndTheDog.FSM.States
{
    class FSMRandom : FSMState
    {
        Vector2 targetPos;
        public FSMRandom(FAIController controller) : base(FSMEnum.Random, controller)
        {
            targetPos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
        }

        public override void Update(float delta)
        {
            if(controller.distanceToHealth > 0 && controller.distanceToHealth < 250)
                targetPos = controller.nearestHealth;

            if (controller.MoveTo(targetPos))
                targetPos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
        }

        public override FSMEnum CheckTransitions()
        {
            if (controller.healthPercentage > controller.playerHealthPercentage && controller.distanceToPlayer < 400)
            {
                nextState = FSMEnum.Approach;
            }
            else if (controller.healthPercentage < 0.6f && controller.distanceToPlayer < 400)
            {
                nextState = FSMEnum.Evade;
            }

            return base.CheckTransitions();
        }
    }
}
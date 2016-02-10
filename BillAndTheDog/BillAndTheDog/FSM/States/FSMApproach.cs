using BillAndTheDog.FuSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BillAndTheDog.FSM.States
{
    class FSMApproach : FSMState
    {

        public FSMApproach(FAIController controller) : base(FSMEnum.Approach, controller)
        {

        }

        public override void Update(float delta)
        {
            controller.MoveTo(controller.playerPosition);
        }

        public override FSMEnum CheckTransitions()
        {
            if (controller.healthPercentage < controller.playerHealthPercentage - 0.1f)
            {
                nextState = FSMEnum.Evade;
            }

            return base.CheckTransitions();
        }
    }
}

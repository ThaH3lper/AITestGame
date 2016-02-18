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
        private Vector2 targetPos;  //The point the entity moves to.

        public FSMRandom(FAIController controller) : base(FSMEnum.Random, controller)
        {
            targetPos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
        }

        public override void Update(float delta)
        {
            //If health is close enouth go for it!
            if(controller.distanceToHealth > 0 && controller.distanceToHealth < 400)
                targetPos = controller.nearestHealth;

            //Else move randomly.
            if (controller.MoveTo(targetPos))
                targetPos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
        }

        public override FSMEnum CheckTransitions()
        {
            if (controller.healthPercentage > controller.playerHealthPercentage - 0.3f && controller.distanceToPlayer < 500)
                nextState = FSMEnum.Approach;
            else if (controller.healthPercentage < 0.3f && controller.distanceToPlayer < 200)
                nextState = FSMEnum.Evade;

            return base.CheckTransitions();
        }
    }
}
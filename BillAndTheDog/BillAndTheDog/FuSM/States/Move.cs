using BillAndTheDog.FSM;
using BillAndTheDog.FSM.States;
using Microsoft.Xna.Framework;
using System;

namespace BillAndTheDog.FuSM
{
    class Move : FuSMState
    {
        private FSMMachine machine;     //This state uses a FSM to divide where is should move.

        public Move(FAIController controller) : base(controller)
        {
            //Create FSM.
            machine = new FSMMachine();

            //Set Random to default state.
            FSMRandom random = new FSMRandom(controller);
            machine.AddState(random);
            machine.SetDefaultState(random);

            //Add more states.
            machine.AddState(new FSMApproach(controller));
            machine.AddState(new FSMEvade(controller));
        }

        public override void Update(float delta)
        {
            //Update the FSM.
            machine.UpdateMachine(delta);

            //Update the speed of player depending on the afctivation level.
            controller.SetSpeed(activationLevel);
        }

        public override float CalculateActivationLevel()
        {
            //If entity is closer to player it will move slower.
            if(controller.distanceToPlayer < 300f)
                activationLevel = controller.distanceToPlayer / 300f;
            else
                activationLevel = 1;

            //Debug message.
            controller.SetDebugText(1, "Move: " + Math.Round(activationLevel, 2));
            return activationLevel;
        }
    }
}

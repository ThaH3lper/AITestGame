using BillAndTheDog.FSM;
using BillAndTheDog.FSM.States;
using Microsoft.Xna.Framework;

namespace BillAndTheDog.FuSM
{
    class Move : FuSMState
    {
        FSMMachine machine;

        public Move(AIController controller) : base(controller)
        {
            machine = new FSMMachine();
            FSMRandom random = new FSMRandom(controller);
            machine.AddState(random);
            machine.SetDefaultState(random);
            machine.AddState(new FSMApproach(controller));
            machine.AddState(new FSMEvade(controller));
        }
        public override void Update(float delta)
        {
            machine.UpdateMachine(delta);
            controller.SetSpeed(activationLevel);
        }

        public override float CalculateActivationLevel()
        {
            //Fix This!
            if(controller.distanceToPlayer < 300f)
            {
                activationLevel = controller.distanceToPlayer / 300f;
            }
            else
                activationLevel = 1;
            return activationLevel;
        }
    }
}

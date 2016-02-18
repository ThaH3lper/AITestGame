using BillAndTheDog.FuSM;
using Microsoft.Xna.Framework;

namespace BillAndTheDog.FSM
{
    class FSMState
    {
        protected FAIController controller;     //The AI controller
        private FSMEnum stateType;              //The enum state of this state.
        protected FSMEnum nextState;            //Set next state to change state.

        public FSMState(FSMEnum stateType, FAIController controller)
        {
            this.controller = controller;
            this.stateType = stateType;
            nextState = stateType;
        }

        public FSMEnum GetStateType()
        {
            return stateType;
        }

        public void Enter()
        {
            nextState = stateType;
        }

        public virtual void Update(float delta) { }

        public virtual FSMEnum CheckTransitions() {
            controller.SetDebugText(3, "FSM State: " + nextState);
            return nextState;
        }
    }
}

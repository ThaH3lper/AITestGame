using BillAndTheDog.FuSM;
using Microsoft.Xna.Framework;

namespace BillAndTheDog.FSM
{
    class FSMState
    {
        protected FAIController controller;
        private FSMEnum stateType;
        protected FSMEnum nextState;

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
            return nextState;
        }
    }
}

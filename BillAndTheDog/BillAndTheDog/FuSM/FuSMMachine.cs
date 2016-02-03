using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class FuSMMachine
    {
        List<FuSMState> states;
        List<FuSMState> activatedStates;

        public FuSMMachine()
        {
            states = new List<FuSMState>();
            activatedStates = new List<FuSMState>();
        }

        public void AddState(FuSMState state)
        {
            states.Add(state);
        }

        public void Update(float delta)
        {
            if (states.Count == 0)
                return;

            foreach (FuSMState state in states)
            {
                if(state.CalculateActivationLevel() > 0)
                    activatedStates.Add(state);
            }

            if (activatedStates.Count != 0)
            {
                foreach (FuSMState state in activatedStates)
                    state.Update(delta);
            }
            activatedStates.Clear();
        }
    }
}

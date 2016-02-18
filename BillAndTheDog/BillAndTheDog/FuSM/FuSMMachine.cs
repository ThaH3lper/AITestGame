using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class FuSMMachine
    {
        private List<FuSMState> states;             //All the states in the machine.
        private List<FuSMState> activatedStates;    //All the activated states.

        public FuSMMachine()
        {
            states = new List<FuSMState>();
            activatedStates = new List<FuSMState>();
        }

        /// <summary>
        /// Add state to the machine.
        /// </summary>
        /// <param name="state">The state to add.</param>
        public void AddState(FuSMState state){ states.Add(state); }

        public void Update(float delta)
        {
            //if there is no states in machine then return.
            if (states.Count == 0)
                return;

            ///For each state calculate the activation level and add activated states in the list.
            foreach (FuSMState state in states)
            {
                if(state.CalculateActivationLevel() > 0)
                    activatedStates.Add(state);
            }

            //Update the activated states.
            if (activatedStates.Count != 0)
            {
                foreach (FuSMState state in activatedStates)
                    state.Update(delta);
            }
            activatedStates.Clear();
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FSM
{
    class FSMMachine
    {
        private FSMState currentState;
        private FSMState defaultState;
        private Hashtable states;

        public FSMMachine()
        {
            states = new Hashtable();
        }
        public void UpdateMachine(float delta)
        {
            if (states.Count == 0)
                return;

            if (currentState == null)
                currentState = defaultState;
            if (currentState == null)
                return;

            FSMEnum oldType = currentState.GetStateType();
            FSMEnum newType = currentState.CheckTransitions();

            if(oldType != newType)
            {
                if (TransitionState(newType))
                {
                    currentState = (FSMState)states[newType];
                    currentState.Enter();
                }
            }
            currentState.Update(delta);
        }

        public void SetDefaultState(FSMState state){
            defaultState = state;
        }

        public void AddState(FSMState state){
            states[state.GetStateType()] = state;
        }

        public bool TransitionState(FSMEnum stateType){
            return (states[stateType] != null);
        }
    }
}

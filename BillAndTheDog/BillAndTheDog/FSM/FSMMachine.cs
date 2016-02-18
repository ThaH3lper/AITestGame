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
        private FSMState currentState;      //Current FSM state.
        private FSMState defaultState;      //The Default/Start state.
        private Hashtable states;           //Using hashtabel to easy get the state from enum name.

        public FSMMachine()
        {
            states = new Hashtable();
        }
        public void UpdateMachine(float delta)
        {
            //If the machine has no states then return;
            if (states.Count == 0)
                return;

            //If state is null set the default state.
            if (currentState == null)
                currentState = defaultState;
            if (currentState == null)
                return;

            FSMEnum oldType = currentState.GetStateType();
            FSMEnum newType = currentState.CheckTransitions();

            //Check if current state wants to change state.
            if(oldType != newType)
            {
                //Checks if the state exists in this machine.
                if (TransitionState(newType))
                {
                    //Enter the new state
                    currentState = (FSMState)states[newType];
                    currentState.Enter();
                }
            }
            //Update the current state.
            currentState.Update(delta);
        }

        public void SetDefaultState(FSMState state){
            defaultState = state;
        }

        public void AddState(FSMState state){
            states[state.GetStateType()] = state;
        }

        //Checks if the state exists in this machine.
        public bool TransitionState(FSMEnum stateType){
            return (states[stateType] != null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class FuSMState
    {
        protected FAIController controller;     //The AI controller with all the values.
        protected float activationLevel;        //The activation level of this state.

        public FuSMState(FAIController controller)
        {
            this.controller = controller;
            activationLevel = 0;
        }

        public virtual float CalculateActivationLevel(){ return 0; }

        public virtual void Update(float delta) { }
    }
}

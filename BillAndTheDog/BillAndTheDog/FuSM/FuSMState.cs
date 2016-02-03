﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class FuSMState
    {
        protected AIController controller;
        protected float activationLevel;

        public FuSMState(AIController controller)
        {
            this.controller = controller;
            activationLevel = 0;
        }

        public virtual float CalculateActivationLevel()
        {
            return 0;
        }

        public virtual void Update(float delta)
        {

        }
    }
}
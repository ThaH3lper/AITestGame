using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class Fire : FuSMState
    {
        private float maxRadius = 300;              //The maxradius for fire.
        private float time, timeInterval = 0.7f;    //Time is the current delay, timeInterval is the fastest fire rate.

        public Fire(FAIController controller) : base(controller)
        {
            time = 0f;
        }
        public override void Update(float delta)
        {
            time += delta;
            if(time >= timeInterval * (1 - activationLevel)) //Fire faster if activation is highet.
            {
                time = -0.1f;
                controller.FireAt(controller.playerPosition);
            }
        }

        public override float CalculateActivationLevel()
        {
            //Activate if player is inside maxRadius.
            if(controller.distanceToPlayer <= maxRadius)
            {
                float temp = (controller.healthPercentage) * (controller.distanceToPlayer / maxRadius);
                activationLevel = 1 - temp;
            }
            else
                activationLevel = 0;

            //Debug message.
            controller.SetDebugText(0, "Fire: " + Math.Round(activationLevel, 2));
            return activationLevel;
        }
    }
}

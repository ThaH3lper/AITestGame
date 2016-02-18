using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class Warn : FuSMState
    {
        private float peakRadius = 320;       //The peak radius is the radius where entity warns most.
        private float smoothValue = 100;      //peak value +- this smoothValue gives activation 0.
        float time, timeInterval = 1f;

        public Warn(FAIController controller) : base(controller)
        {
            time = 0f;
        }

        public override void Update(float delta)
        {
            time += delta;
            if(time >= timeInterval * (1 - activationLevel)) //Warn more if activation level is higher.
            {
                time = -0.1f;
                controller.Talk(Globals.GetRandomWarning(), Color.Red);
            }
        }

        public override float CalculateActivationLevel()
        {
            //Set peak value as 1, and smooth out over smoothValue.
            if(controller.distanceToPlayer <= peakRadius + smoothValue && controller.distanceToPlayer >= peakRadius - smoothValue)
            {
                float temp = (Math.Abs(controller.distanceToPlayer - peakRadius) / smoothValue);
                activationLevel = 1 - temp;
            }
            else
                activationLevel = 0;

            //Debug message
            controller.SetDebugText(2, "Warn: " + Math.Round(activationLevel, 2));
            return activationLevel;
        }
    }
}

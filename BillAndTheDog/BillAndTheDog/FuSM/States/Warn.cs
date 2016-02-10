using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class Warn : FuSMState
    {
        private float peakRadius = 320, smoothValue = 70;
        float time, timeInterval = 1f;

        public Warn(FAIController controller) : base(controller)
        {
            time = -0.1f;
        }

        public override void Update(float delta)
        {
            time += delta;
            if(time >= timeInterval * (1 - activationLevel))
            {
                time = -0.1f;
                controller.Talk(Globals.GetRandomWarning(), Color.Red);
            }
        }

        public override float CalculateActivationLevel()
        {
            if(controller.distanceToPlayer <= peakRadius + smoothValue && controller.distanceToPlayer >= peakRadius - smoothValue)
            {
                float temp = (Math.Abs(controller.distanceToPlayer - peakRadius) / 70);
                activationLevel = 1 - temp;
            }
            else
                activationLevel = 0;
            return activationLevel;
        }
    }
}

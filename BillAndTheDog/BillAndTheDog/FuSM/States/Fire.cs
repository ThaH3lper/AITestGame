using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class Fire : FuSMState
    {
        private float maxRadius = 300;
        float time, timeInterval = 1f;

        public Fire(AIController controller) : base(controller)
        {
            time = -0.1f;
        }
        public override void Update(float delta)
        {
            time += delta;
            if(time >= timeInterval * (1 - activationLevel))
            {
                time = -0.1f;
                controller.FireAt(controller.playerPosition);
            }
        }

        public override float CalculateActivationLevel()
        {
            if(controller.distanceToPlayer <= maxRadius)
            {
                float temp = (controller.healthPercentage) * (controller.distanceToPlayer / maxRadius);
                activationLevel = 1 - temp;
            }
            else
                activationLevel = 0;
            return activationLevel;
        }
    }
}

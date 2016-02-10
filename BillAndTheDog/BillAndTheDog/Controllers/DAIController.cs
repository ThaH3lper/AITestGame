using Microsoft.Xna.Framework;

namespace BillAndTheDog
{
    class DAIController : Ai
    {
        public float healthPercentage;
        public float distanceToHealth;
        public Vector2 nearestHealth;

        public float playerHealthPercentage;
        public float distanceToPlayer;
        public Vector2 playerPosition, evadePos;

        public Vector2 targetpos;
        public float time;

        public DAIController(SimulationWorld world, Vector2 location) : base(world, location)
        {
            targetpos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
        }

        public override void Update(float delta)
        {
            time += delta;
            if (time > 10)
                time = 10;
            UpdatePerceptions();
            DecisionTree();
            base.Update(delta);
        }

        public void DecisionTree()
        {
            if (distanceToPlayer < 500)
            {
                if (GetHealthPercentage() + 0.5f < playerHealthPercentage)
                {
                    MoveTo(evadePos);
                    targetpos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
                }
                else
                {
                    if (distanceToPlayer > 250)
                        MoveTo(playerPosition);
                    else
                    {
                        MoveTo(playerPosition);
                        if (time > 0.5f)
                        {
                            time = 0;
                            FireAt(playerPosition);
                        }
                    }
                }
            }
            else if(distanceToPlayer > 500 && distanceToPlayer < 600)
            {
                if (time > 1.0f)
                {
                    time = 0;
                    Talk(Globals.GetRandomWarning(), Color.Red);
                }
            }
            else 
            {
                if((distanceToHealth >= 0 && distanceToHealth < 400) || (healthPercentage < 0.5f && distanceToHealth > 0))
                    MoveTo(nearestHealth);
                else 
                {
                    if(MoveTo(targetpos))
                        targetpos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
                }
            }
        }

        public void UpdatePerceptions()
        {
            healthPercentage = GetHealthPercentage();
            Health health = world.GetShortestHealth(out distanceToHealth, location);
            if (health != null)
                nearestHealth = health.GetPosition();
            playerHealthPercentage = world.player.GetHealthPercentage();
            distanceToPlayer = (world.player.GetLocation() - location).Length();
            playerPosition = world.player.GetLocation();

            Vector2 temp = location - playerPosition;
            temp.Normalize();
            temp *= 500;
            evadePos = temp + location;
        }
    }
}

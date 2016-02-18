using Microsoft.Xna.Framework;

namespace BillAndTheDog
{
    class DAIController : Ai
    {
        public float healthPercentage;              //Health of entity in precentage.
        public float distanceToHealth;              //Distance to health, -1 if there is no health.
        public Vector2 nearestHealth;               //Position where the nearest health is.

        public float playerHealthPercentage;        //Player's health in percentage.
        public float distanceToPlayer;              //The distance to the player.
        public Vector2 playerPosition, evadePos;    //The player's position and the opposit position.

        public Vector2 targetpos;                   //Where the entity wants to go.
        public float time;                          //Time delay for fire.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="world">The world the entity lives in.</param>
        /// <param name="location">The location we want to spawn the entity at.</param>
        public DAIController(SimulationWorld world, Vector2 location) : base(world, location)
        {
            targetpos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));   //Walk to random position.
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

        /// <summary>
        /// The decision tree that this type of AI is using to decide what to do.
        /// </summary>
        public void DecisionTree()
        {
            ClearDebugText();
            if (distanceToPlayer < 500)
            { //NODE 1
                SetDebugText(0, "NODE 1");
                if (GetHealthPercentage() + 0.4f < playerHealthPercentage || distanceToPlayer > 300)
                { //NODE 3
                    SetDebugText(1, "NODE 3");
                    if ((distanceToHealth >= 0 && distanceToHealth < 600) || (healthPercentage < 0.5f && distanceToHealth > 0))
                    { //NODE 7
                        SetDebugText(2, "NODE 7");
                        MoveTo(nearestHealth);
                    }
                    else
                    { //NODE 8
                        SetDebugText(1, "NODE 8");
                        MoveTo(evadePos);
                        targetpos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
                    }
                }
                else
                { //NODE 4
                    SetDebugText(1, "NODE 4");
                    if (distanceToPlayer >= 100)
                    { //NODE 9
                        SetDebugText(2, "NODE 9");
                        if (time > 0.5f)
                        {
                            time = 0;
                            FireAt(playerPosition);
                        }
                        MoveTo(playerPosition);
                    }
                    else
                    { //NODE 10
                        SetDebugText(2, "NODE 10");
                        if (time > 0.5f)
                        {
                            time = 0;
                            FireAt(playerPosition);
                        }
                    }
                }
            }
            else
            { //NODE 2
                SetDebugText(0, "NODE 2");
                if (distanceToPlayer < 600)
                { //NODE 5
                    SetDebugText(1, "NODE 5");
                    if (time > 1.0f)
                    {
                        time = 0;
                        Talk(Globals.GetRandomWarning(), Color.Red);
                    }
                }
                else
                { //NODE 6
                    SetDebugText(1, "NODE 6");
                    if ((distanceToHealth >= 0 && distanceToHealth < 400) || (healthPercentage < 0.5f && distanceToHealth > 0))
                    { //NODE 11
                        SetDebugText(2, "NODE 11");
                        MoveTo(nearestHealth);
                    }
                    else
                    { //NODE 12
                        SetDebugText(2, "NODE 12");
                        if (MoveTo(targetpos))
                            targetpos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
                    }
                }
            }
        }

        /// <summary>
        /// Updates all the parameters that we can use in the decision tree.
        /// </summary>
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

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class FAIController : Ai
    {
        public float healthPercentage;              //Health of entity in precentage.
        public float distanceToHealth;              //Distance to health, -1 if there is no health.
        public Vector2 nearestHealth;               //Position where the nearest health is.

        public float playerHealthPercentage;        //Player's health in percentage.
        public float distanceToPlayer;              //The distance to the player.
        public Vector2 playerPosition;              //Position where the nearest health is.

        private FuSMMachine machine;                //The FuSM Machine.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="world">The world the entity lives in.</param>
        /// <param name="location">The location we want to spawn the entity at.</param>
        public FAIController(SimulationWorld world, Vector2 location) : base(world, location)
        {
            machine = new FuSMMachine();

            //Add states to the FuSM Machine
            machine.AddState(new Fire(this));
            machine.AddState(new Warn(this));
            machine.AddState(new Move(this));
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            UpdatePerceptions();
            machine.Update(delta);

        }

        /// <summary>
        /// Updates all the values to calculate in the FuSM and FSM
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
        }
    }
}

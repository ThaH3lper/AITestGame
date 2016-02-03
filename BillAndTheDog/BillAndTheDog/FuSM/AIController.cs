﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog.FuSM
{
    class AIController : Ai
    {
        public float healthPercentage;
        public float distanceToHealth;
        public Vector2 nearestHealth;

        public float playerHealthPercentage;
        public float distanceToPlayer;
        public Vector2 playerPosition;

        FuSMMachine machine;

        //debug
        Vector2 targetPos;

        public AIController(SimulationWorld world, Vector2 location) : base(world, location)
        {
            machine = new FuSMMachine();
            machine.AddState(new Fire(this));
            machine.AddState(new Warn(this));
            machine.AddState(new Move(this));

            //Debug
            targetPos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            UpdatePerceptions();
            machine.Update(delta);

            //debug
            //if (MoveTo(targetPos))
            //{
            //    targetPos = new Vector2(Globals.random.Next(1280), Globals.random.Next(720));
            //    Talk(Globals.GetRandomWarning(), Color.Red);
            //}
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
        }
    }
}
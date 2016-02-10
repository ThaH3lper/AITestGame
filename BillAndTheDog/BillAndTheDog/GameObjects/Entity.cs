﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BillAndTheDog
{
    class Entity : GameObject
    {
        protected Vector2 location;
        protected Vector2 velocity;
        protected float speed, defaultSpeed;
        protected HealthBar healthBar;

        public Entity(SimulationWorld world, Vector2 location) : base(world, new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), Globals.sheet)
        {
            speed = 200;
            defaultSpeed = speed;
            healthBar = new HealthBar(100, this);
            recDraw.X = Globals.random.Next(4) * 32;
            this.location = location;
        }

        public override void Update(float delta)
        {
            location += velocity * delta * speed;
            recHit.X = (int)(location.X - recDraw.Width / 2);
            recHit.Y = (int)(location.Y - recDraw.Height / 2);
            healthBar.Update();
            if (healthBar.IsDead())
            {
                world.Remove(this);
                Talk("Dead", Color.Violet);
            }
        }

        public void SetSpeed(float percentage)
        {
            speed = defaultSpeed * percentage;
        }

        public void Move(Vector2 direction)
        {
            if(direction.X != 0 && direction.Y != 0)
                direction.Normalize();
            velocity = direction;
        }

        public void Stop()
        {
            velocity = new Vector2(0, 0);
        }

        public void Talk(string msg, Color color)
        {
            world.SayMsg(msg, this, color);
        }

        public void FireAt(Vector2 position)
        {
            Vector2 direction = position - location;
            direction.Normalize();
            world.AddBullet(this, direction, location);
        }
        public void Damage(float damage) { healthBar.Damage(damage); }

        public float GetHealthPercentage() { return healthBar.GetPercentage(); }

        public void AddHealth(float amount) { healthBar.AddHealth(amount); }

        public Vector2 GetLocation() { return location; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
        }
    }
}

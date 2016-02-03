using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class HealthBar
    {
        float health;
        float maxHealth;
        float percentage;
        Entity entity;

        Rectangle recMax, recCurrent;

        public HealthBar(float maxHealth, Entity entity)
        {
            this.entity = entity;
            this.maxHealth = maxHealth;
            health = maxHealth;
            percentage = 1f;
            recMax = new Rectangle(0, 0, entity.GetRecHit().Width + 2, 7);
            recCurrent = new Rectangle(1, 1, entity.GetRecHit().Width, 5);
        }

        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0)
                health = 0;
            if (health >= maxHealth)
                health = maxHealth;
            percentage = health / maxHealth;
            recCurrent.Width = (int)((recMax.Width - 2) * percentage);
        }

        public void AddHealth(float amount) {
            Damage(-amount);
            if (percentage == 1f)
                entity.Talk("Full Health!", Color.Green);
            else
                entity.Talk("Healed!", Color.Green);
        }

        public float GetPercentage() { return percentage; }

        public bool IsDead() { return (health == 0); }

        public void Update()
        {
            Vector2 pos = entity.GetLocation();
            recMax.X = (int)pos.X - recMax.Width/2;
            recMax.Y = (int)pos.Y + entity.GetRecHit().Height/2;
            recCurrent.X = recMax.X + 1;
            recCurrent.Y = recMax.Y + 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.pixel, recMax, Color.Black);
            spriteBatch.Draw(Globals.pixel, recCurrent, Color.Red);
        }
    }
}

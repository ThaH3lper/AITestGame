using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BillAndTheDog
{
    class Entity : GameObject
    {
        protected Vector2 location;
        protected Vector2 velocity;
        protected float speed, defaultSpeed;
        protected HealthBar healthBar;

        protected string[] debugArray;

        public Entity(SimulationWorld world, Vector2 location) : base(world, new Rectangle(0, 0, 32, 32), new Rectangle(0, 0, 32, 32), Globals.sheet)
        {
            speed = 200;
            defaultSpeed = speed;
            healthBar = new HealthBar(100, this);
            recDraw.X = Globals.random.Next(4) * 32;
            this.location = location;

            debugArray = new string[5];
        }

        public override void Update(float delta)
        {
            location += velocity * delta * speed;
            UpdateRec();
            CheackVertical();
            CheackHorizontal();

            healthBar.Update();
            if (healthBar.IsDead())
            {
                world.Remove(this);
                Talk("Dead", Color.Violet);
            }
        }
        private void UpdateRec()
        {
            recHit.X = (int)(location.X - recHit.Width / 2);
            recHit.Y = (int)(location.Y - recHit.Height / 2);
        }
        public void CheackVertical()
        {
            if(recHit.Y + recHit.Height > Globals.ScreenHeight)
            {
                location.Y = Globals.ScreenHeight - recHit.Height/2;
                UpdateRec();
            }
            else if(recHit.Y < 0)
            {
                location.Y = recHit.Height / 2;
                UpdateRec();
            }
        }
        public void CheackHorizontal()
        {
            if (recHit.X + recHit.Width > Globals.ScreenWidth)
            {
                location.X = Globals.ScreenWidth - recHit.Width / 2;
                UpdateRec();
            }
            else if (recHit.X < 0)
            {
                location.X = recHit.Width / 2;
                UpdateRec();
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

        public void SetDebugText(int index, string text)
        {
            debugArray[index] = text;
        }
        
        public void ClearDebugText()
        {
            for (int i = 0; i < debugArray.Length; i++)
                debugArray[i] = "";
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

            //draw debug Text
            if (Globals.debug)
            {
                for (int i = 0; i < debugArray.Length; i++)
                {
                    if (debugArray[i] != null)
                    {
                        Vector2 temp = Globals.GetTextSize(debugArray[i]);
                        spriteBatch.DrawString(Globals.font, debugArray[i], location + new Vector2(-temp.X / 2, i * 15 + temp.Y + 7), Color.Black);
                    }
                }
            }
        }
    }
}

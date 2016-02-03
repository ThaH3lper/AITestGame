using BillAndTheDog.FuSM;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BillAndTheDog
{
    class SimulationWorld
    {
        private List<GameObject> gameObjects;
        private List<GameObject> addObjects;
        private List<GameObject> removeObjects;

        private List<Entity> entitys;
        private List<Health> healths;

        private List<Message> messages;
        private List<Message> removeMessages;

        public Player player;

        public SimulationWorld()
        {
            Random r = new Random();
            addObjects = new List<GameObject>();
            gameObjects = new List<GameObject>();
            removeObjects = new List<GameObject>();

            entitys = new List<Entity>();
            healths = new List<Health>();

            addObjects = new List<GameObject>();
            messages = new List<Message>();
            removeMessages = new List<Message>();

            player = new Player(this, new Vector2(640, 360));
            AddGameObject(player);
            for (int i = 0; i < 1; i++)
                AddGameObject(new AIController(this, new Vector2(50, 50)));
        }

        public void AddGameObject(GameObject o)
        {
            gameObjects.Add(o);
            if (o is Entity)
                entitys.Add((Entity)o);
            if (o is Health)
                healths.Add((Health)o);
        }

        public void Remove(GameObject o)
        {
            removeObjects.Add(o);
            if (o is Entity)
                entitys.Remove((Entity)o);
            if (o is Health)
                healths.Remove((Health)o);
        }

        public void Update(float delta)
        {
            //Add Health
            if (Globals.random.NextDouble() <= 0.01 && healths.Count < 3)
                AddGameObject(new Health(this, new Vector2(Globals.random.Next(1280), Globals.random.Next(720))));
            //-------

            foreach (GameObject o in addObjects)
                gameObjects.Add(o);
            addObjects.Clear();

            foreach (GameObject o in gameObjects)
                o.Update(delta);

            foreach (GameObject o in removeObjects)
            {
                gameObjects.Remove(o);
                if (o is Entity)
                    entitys.Remove((Entity)o);
            }
            removeObjects.Clear();

            foreach (Message msg in messages)
            {
                msg.Update(delta);
                if (msg.IsDead())
                    removeMessages.Add(msg);
            }
            foreach (Message msg in removeMessages)
                messages.Remove(msg);
            removeMessages.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject o in gameObjects)
                o.Draw(spriteBatch);
            foreach (Message msg in messages)
            {
                msg.Draw(spriteBatch);
            }
        }

        public void AddBullet(Entity entity, Vector2 direction, Vector2 location)
        {
            addObjects.Add(new Bullet(this, entity, location, direction));
        }

        public void SayMsg(string msg, Entity entity, Color color)
        {
            messages.Add(new Message(msg, entity, color));
        }
        public Entity GetEntityInRectangle(Rectangle rec)
        {
            foreach (Entity entity in entitys)
            {
                if (entity.GetRecHit().Intersects(rec))
                    return entity;
            }
            return null;
        }

        public Health GetShortestHealth(out float distance, Vector2 position)
        {
            float shortest = -1f;
            Health closest = null;

            foreach (Health h in healths)
            {
                if (shortest == -1f || (position - h.GetPosition()).Length() < shortest){
                    closest = h;
                    shortest = (position - h.GetPosition()).Length();
                }
            }
            distance = shortest;
            return closest;
        }
    }
}

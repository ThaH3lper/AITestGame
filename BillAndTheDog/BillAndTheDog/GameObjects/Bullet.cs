using Microsoft.Xna.Framework;

namespace BillAndTheDog
{
    class Bullet : GameObject
    {
        private Entity entity;
        private Vector2 location, direction;
        private float speed, distance;

        public Bullet(SimulationWorld world, Entity entity, Vector2 location, Vector2 direction) : base(world, new Rectangle(0, 0, 5, 5), new Rectangle(0, 0, 1, 1), Globals.pixel)
        {
            this.entity = entity;
            this.location = location;
            this.direction = direction;
            distance = 0;
            speed = 400;
        }

        public override void Update(float delta)
        {
            distance += (direction * delta * speed).Length();
            location += direction * delta * speed;
            recHit.X = (int)(location.X - recHit.Width / 2);
            recHit.Y = (int)(location.Y - recHit.Height / 2);
            Entity hited = world.GetEntityInRectangle(recHit);
            if (hited != null)
            {
                if (entity is Ai && hited is Ai)
                    return;
                if (entity is Player && hited is Player)
                    return;
                world.Remove(this);
                hited.Damage(10);
            }
            if(distance > 300.0f)
                world.Remove(this);
        }
    }
}

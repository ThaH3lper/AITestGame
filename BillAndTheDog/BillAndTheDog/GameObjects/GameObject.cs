using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BillAndTheDog
{
    class GameObject
    {
        protected Rectangle recDraw, recHit;
        protected Texture2D texture;
        protected Color color;
        protected SimulationWorld world;

        public GameObject(SimulationWorld world, Rectangle recHit, Rectangle recDraw, Texture2D texture)
        {
            this.world = world;
            this.recHit = recHit;
            this.recDraw = recDraw;
            this.texture = texture;
            this.color = Color.Black;
        }
        public virtual void Update(float delta)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, recHit, recDraw, color);
        }

        public Rectangle GetRecHit()
        {
            return recHit;
        }
    }
}

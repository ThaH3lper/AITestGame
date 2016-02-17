using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BillAndTheDog
{
    class Message
    {
        string msg;
        Entity entity;
        Vector2 Offset;
        Color color;
        int opacity;
        
        public Message(string msg, Entity entity, Color color)
        {
            this.msg = msg;
            this.color = color;
            this.entity = entity;

            opacity = 255;
            Offset = new Vector2(-Globals.GetTextSize(msg).X / 2, -entity.GetRecHit().Height/2 - Globals.GetTextSize(msg).Y);
        }

        public void Update(float delta)
        {
            Offset.Y -= delta * 30;
            opacity -= (int)(delta * 180);
            if (opacity <= 0)
                opacity = 0;
        }

        public bool IsDead() { return (opacity == 0); }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.font, msg, entity.GetLocation() + Offset, Color.FromNonPremultiplied(color.R, color.G, color.B, opacity));
        }
    }
}
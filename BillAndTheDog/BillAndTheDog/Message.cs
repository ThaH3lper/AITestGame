using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillAndTheDog
{
    class Message
    {
        Vector2 location;
        string msg;
        int opacity;
        public Message(string msg, Vector2 location)
        {
            this.location = location;
            this.msg = msg;
            opacity = 255;
        }

        public void Update(float delta)
        {
            location.Y -= delta * 70;
            opacity -= (int)(delta * 255);
            if (opacity <= 0)
                opacity = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.font, msg, location, Color.FromNonPremultiplied(0, 0, 0, opacity));
        }
    }
}

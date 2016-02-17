using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BillAndTheDog
{
    static class Globals
    {
        public static Texture2D pixel, sheet;
        public static SpriteFont font;
        public static Random random;
        private static string[] warnings;

        public static void Load(Game1 game)
        {
            pixel = game.Content.Load<Texture2D>("Pixel");
            sheet = game.Content.Load<Texture2D>("Sheet");
            font = game.Content.Load<SpriteFont>("Font");

            random = new Random();

            warnings = new string[] { "Go away!", "I'll shoot you!", "Not one more step", "Go back!", "Halt!", "Stop right there!"};
        }

        public static Vector2 GetTextSize(string s){ return font.MeasureString(s); }

        public static string GetRandomWarning(){ return warnings[random.Next(warnings.Length)]; }
    }
}

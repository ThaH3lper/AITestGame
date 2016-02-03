using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

static class KeyMouseReader
{
	public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
	public static MouseState mouseState, oldMouseState = Mouse.GetState();

	public static bool KeyClick(Keys key) {
		return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
	}
    public static bool KeyPress(Keys key){
        return keyState.IsKeyDown(key);
    }
    public static List<Keys> GetKeyPressed(){
        List<Keys> keys = new List<Keys>();
        foreach(Keys key in keyState.GetPressedKeys())
        {
            if(oldKeyState.IsKeyUp(key))
                keys.Add(key);
        }
        return keys;
    }
	public static bool LeftClick() {
		return mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released;
	}
    public static bool LeftPressed(){
        return mouseState.LeftButton == ButtonState.Pressed;
    }
	public static bool RightClick() {
		return mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released;
	}
    public static bool RightPressed(){
        return mouseState.RightButton == ButtonState.Pressed;
    }
    public static Vector2 GetMousePos(){
        return new Vector2((int)mouseState.X, (int)mouseState.Y);
    }

	public static void Update() {
		oldKeyState = keyState;
		keyState = Keyboard.GetState();
		oldMouseState = mouseState;
		mouseState = Mouse.GetState();
	}
}
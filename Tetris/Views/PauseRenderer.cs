using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Views
{
    internal class PauseRenderer
         (GraphicsDeviceManager gdm, ContentManager cm) : IRenderer(gdm, cm)
    {
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawModalWithTitle(spriteBatch, "Game Pause", Color.White);
        }

        public override void LoadContent()
        {
            LoadBasicContent();
        }

        public override void UnLoad()
        {
            throw new System.NotImplementedException();
        }
    }
}

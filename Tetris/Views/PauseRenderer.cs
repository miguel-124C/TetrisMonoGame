using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tetris.Views
{
    internal class PauseRenderer(GraphicsDeviceManager gdm, ContentManager cm) : IRenderer(gdm, cm)
    {

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawModalWithTitle(spriteBatch, "Paused", Color.White);

            // instructions
            float alpha = (float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2));
            var instruction = "Press Enter to Resume - Esc to Exit";
            var sizeInst = _spriteFont18.MeasureString(instruction);
            var posInst = new Vector2((_width - sizeInst.X) / 2, (_height - sizeInst.Y));
            spriteBatch.DrawString(_spriteFont18, instruction, posInst, Color.White * alpha);
        }

        public override void LoadContent()
        {
            LoadBasicContent();
        }

        public override void UnLoad()
        {
            // nothing to unload specifically for this renderer
        }
    }
}

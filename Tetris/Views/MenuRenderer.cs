using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tetris.Views
{
    public class MenuRenderer(GraphicsDeviceManager gdm, ContentManager cm)
        : IRenderer(gdm, cm)
    {

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float alpha = (float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2.5));

            var titleGame = "TETRIS";
            var instruction = "Press Enter to Start";

            // background
            spriteBatch.Draw(_blankTexture, new Rectangle(0, 0, _width, _height), new Color(12, 12, 20));

            // title shadow
            var sizeTitle = _spriteFont48.MeasureString(titleGame);
            var titleY = (_height - sizeTitle.Y) / 2 - 30;
            var titlePos = new Vector2((_width - sizeTitle.X) / 2, titleY);
            spriteBatch.DrawString(_spriteFont48, titleGame, titlePos + new Vector2(4, 4), Color.Black * 0.6f);
            spriteBatch.DrawString(_spriteFont48, titleGame, titlePos, Color.White);

            // instruction pulsing
            var sizeInstruction = _spriteFont18.MeasureString(instruction);
            var instructionPos = new Vector2((_width - sizeInstruction.X) / 2, titleY + sizeTitle.Y + 40);
            spriteBatch.DrawString(_spriteFont18, instruction, instructionPos, Color.White * (0.5f + 0.5f * alpha));

            // small footer
            var footer = "Arrow keys to move | Arrow Up to rotate | Space to hard drop";
            var footerScale = 0.66f;
            var sizeFooter = _spriteFont18.MeasureString(footer) * footerScale;
            var footerPos = new Vector2((_width - sizeFooter.X) / 2, _height - sizeFooter.Y - 12);
            spriteBatch.DrawString(_spriteFont18, footer, footerPos, Color.White * 0.6f, 0f, Vector2.Zero, footerScale, SpriteEffects.None, 0f);
        }

        public override void UnLoad()
        {
            // nothing specific to unload
        }

        public override void LoadContent()
        {
            LoadBasicContent();
        }
    }
}
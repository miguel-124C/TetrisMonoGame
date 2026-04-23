using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tetris.Views
{
    public class MenuRenderer
        (GraphicsDeviceManager gdm, ContentManager cm) : IRenderer(gdm, cm)
    {
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float alpha = (float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3));

            var titleGame = "Tetris";
            var instruction = "Press Enter to Start";

            var sizeTitle = _spriteFont48.MeasureString(titleGame);
            var titleGameY = (_height - sizeTitle.Y) / 2;
            var positionTitle = new Vector2((_width - sizeTitle.X) / 2, titleGameY);

            var sizeInstruction = _spriteFont18.MeasureString(instruction);
            var positionInstruction = new Vector2((_width - sizeInstruction.X) / 2,
                (titleGameY + sizeTitle.Y) + 50);

            spriteBatch.Draw(_blankTexture, new Rectangle(0, 0, _width, _height), new Color(15, 15, 15));

            spriteBatch.DrawString(_spriteFont48, titleGame, positionTitle, Color.White);
            spriteBatch.DrawString(_spriteFont18, instruction, positionInstruction, Color.White * alpha);
        }

        public override void UnLoad()
        {
            throw new System.NotImplementedException();
        }

        public override void LoadContent()
        {
            base.LoadBasicContent();
        }
    }
}
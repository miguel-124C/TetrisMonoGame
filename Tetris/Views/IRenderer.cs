using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Views
{
    public abstract class IRenderer
    {
        protected Texture2D _blankTexture;
        protected SpriteFont _spriteFont48;
        protected SpriteFont _spriteFont18;

        protected readonly int _width;
        protected readonly int _height;

        protected readonly GraphicsDeviceManager _gdm;
        protected readonly ContentManager contentManager;

        protected IRenderer(GraphicsDeviceManager gdm, ContentManager cm)
        {
            _gdm = gdm;
            _width = gdm.PreferredBackBufferWidth;
            _height = gdm.PreferredBackBufferHeight;

            _blankTexture = new Texture2D(gdm.GraphicsDevice, 1, 1);
            _blankTexture.SetData([Color.White]);

            contentManager = cm;
        }

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void LoadContent();
        protected void LoadBasicContent()
        {
            _spriteFont48 = contentManager.Load<SpriteFont>("Fonts/Arial48");
            _spriteFont18 = contentManager.Load<SpriteFont>("Fonts/Arial18");
        }
        public abstract void UnLoad();

        protected void DrawModalWithTitle(SpriteBatch sb, string titleGame, Color colorText)
        {
            var sizeTitle = _spriteFont48.MeasureString(titleGame);
            var positionTitle = new Vector2((_width - sizeTitle.X) / 2, 10);

            sb.Draw(_blankTexture, new Rectangle(0, 0, _width, _height), new Color(0, 0, 0, 0.9f));
            sb.DrawString(_spriteFont48, titleGame, positionTitle, colorText);
        }
    }
}

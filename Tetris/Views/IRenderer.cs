using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Views
{
    public interface IRenderer
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void LoadContent(ContentManager content);
        void UnLoad();
    }
}

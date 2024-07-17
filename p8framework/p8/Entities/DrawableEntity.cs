using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yukon.p8framework
{
    public abstract class DrawableEntity : Entity
    {
        #region constructor
        protected DrawableEntity(string _name, Scene scene = null) : base(_name, scene)
        {

        }
        #endregion

        #region game functions
        public abstract void Draw(GameTime _gameTime, SpriteBatch _spriteBatch);
        #endregion
    }
}
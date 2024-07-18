using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace yukon.p8f
{
    public class Scene : Entity
    {
        public List<IDrawableEntity> DrawableEntities = new();
        
        public Scene(string _name) : base(_name, null)
        {
            Scene = this;

            p8.Scenes.Add(this);
        }

        public override void OnAwake()
        {
            
        }

        public override void OnDispose()
        {
            DrawableEntities.Clear();

            if (p8.Scenes.Contains(this))
            {
                p8.Scenes.Remove(this);

                if (p8.CurrentScene == this)
                {
                    if (p8.Scenes.Count > 0)
                    {
                        p8.CurrentScene = p8.Scenes[0];
                    }
                    else
                    {
                        p8.CurrentScene = null;
                    }
                }
            }
        }

        public override void OnUpdate(GameTime _gameTime)
        {
            
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            DrawableEntities.OrderBy(e => e.Depth);

            for (int i = 0; i < DrawableEntities.Count; i++)
            {
                Entity entity = DrawableEntities[i] as Entity;
                if(entity != null && !entity.Disposed)
                {
                    DrawableEntities[i].OnDraw(_gameTime, _spriteBatch);
                }
            }
        }

        public void AddDrawableEntity(IDrawableEntity _drawableEntity)
        {
            if(!DrawableEntities.Contains(_drawableEntity))
            {
                DrawableEntities.Add(_drawableEntity);
            }
        }

        public void RemoveDrawableEntity(IDrawableEntity _drawableEntity)
        {
            if (DrawableEntities.Contains(_drawableEntity))
            {
                DrawableEntities.Remove(_drawableEntity);
            }
        }
    }
}
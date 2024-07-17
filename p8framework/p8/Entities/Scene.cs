using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace yukon.p8framework
{
    public class Scene : IDisposable
    {
        #region properties
        public string Name = "Untitled Scene";
        public bool Enabled = true;
        public bool Visible = true;
        public int Depth = 0;

        public bool Disposed = false;
        #endregion

        #region references
        // entities
        public List<Entity> Entities // all entites (including drawables)
        {
            get { return entities; }
        }
        private List<Entity> entities = new();

        public List<DrawableEntity> DrawableEntities // just drawable entities
        {
            get { return drawableEntities; }
        }
        private List<DrawableEntity> drawableEntities = new();

        // monogame content
        public ContentManager Content
        {
            get { return content; }
        }
        private ContentManager content;
        #endregion

        #region constructor
        public Scene(string _name)
        {
            Name = _name;

            p8.Scenes.Add(this);

            content = new ContentManager(p8.Game.Services, "Content");
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool _disposing)
        {
            if (!Disposed)
            {
                Disposed = true;

                content = null;

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

                for (int i = 0; i < entities.Count; i++)
                {
                    Console.WriteLine("Disposing " + entities[i].Name);
                    entities[i].Scene = null;
                    entities[i].Dispose();
                }

                entities.Clear();
                drawableEntities.Clear();
            }
        }
        #endregion

        #region game functions
        public void Update(GameTime _gameTime)
        {
            if(Enabled)
            {
                for(int i = 0; i < entities.Count; i++)
                {
                    if (!entities[i].Disposed && entities[i].Enabled)
                    {
                        entities[i].Update(_gameTime);
                    }
                }
            }
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            if(Visible)
            {
                drawableEntities.OrderBy(e => e.Depth);

                for (int i = 0; i < drawableEntities.Count; i++)
                {
                    if (!drawableEntities[i].Disposed && drawableEntities[i].Visible)
                    {
                        drawableEntities[i].Draw(_gameTime, _spriteBatch);
                    }
                }
            }
        }
        #endregion

        #region management functions
        public void AddEntity(Entity _entity)
        {
            entities.Add(_entity);
            
            if(_entity is DrawableEntity)
            {
                drawableEntities.Add(_entity as DrawableEntity);
            }
        }

        public void RemoveEntity(Entity _entity)
        {
            entities.Remove(_entity);

            if (_entity is DrawableEntity)
            {
                drawableEntities.Remove(_entity as DrawableEntity);
            }
        }
        #endregion
    }
}
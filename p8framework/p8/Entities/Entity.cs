using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace yukon.p8f
{
    public abstract class Entity : IDisposable
    {
        #region properties
        public string Name = "Untitled Entity";
        public bool Enabled = true;

        public Transform Transform = null;

        public bool Disposed = false;
        #endregion

        #region parent & child relationships
        public Scene Scene
        {
            get
            {
                return scene;
            }
            set
            {
                if(this is IDrawableEntity)
                {
                    if (scene != null)
                    {
                        scene.RemoveDrawableEntity(this as IDrawableEntity);
                    }
                }
                
                scene = value;

                if (scene != null)
                {
                    scene.AddDrawableEntity(this as IDrawableEntity);
                }
            }
        }
        private Scene scene = null;
        public Entity Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (parent != null)
                {
                    parent.RemoveChild(this);
                }

                parent = value;

                if(parent != null)
                {
                    parent.AddChild(this);
                }
            }
        }
        private Entity parent = null;
        public List<Entity> Children = new();
        #endregion

        #region constructor
        public Entity(string _name, Entity _parent)
        {
            // set the name
            Name = _name;

            Transform = new Transform(this);

            // add to the proper parent
            Parent = _parent;
            if(Parent != null)
            {
                Scene = Parent.Scene;
            }

            // awake!
            OnAwake();
        }
        #endregion

        #region game functions
        public abstract void OnAwake();

        public void Update(GameTime _gameTime)
        {
            OnUpdate(_gameTime);

            for(int i = 0; i < Children.Count; i++)
            {
                Children[i].Update(_gameTime);
            }
        }

        public abstract void OnUpdate(GameTime _gameTime);
        #endregion

        #region child functions
        public void AddChild(Entity _entity)
        {
            if (!Children.Contains(_entity))
            {
                Children.Add(_entity);
            }
        }

        public void RemoveChild(Entity _entity)
        {
            if (Children.Contains(_entity))
            {
                Children.Remove(_entity);
            }
        }

        public void DisposeChildren()
        {
            for(int i = 0; i < Children.Count; i++)
            {
                Children[i].Dispose();
            }

            Children.Clear();
        }
        #endregion

        #region dispose functions
        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;

                Parent = null;
                DisposeChildren();
                Scene = null;

                Transform.Dispose();

                OnDispose();
                GC.SuppressFinalize(this);
            }
        }

        public abstract void OnDispose();
        #endregion 
    }

    #region transform class
    public class Transform : IDisposable
    {
        #region global properties
        public Vector2 GlobalPosition
        {
            get
            {
                if (entity.Parent != null)
                {
                    return entity.Parent.Transform.GlobalPosition + LocalPosition;
                }
                else
                {
                    return LocalPosition;
                }
            }

            set
            {
                if (entity.Parent != null)
                {
                    LocalPosition = value - entity.Parent.Transform.GlobalPosition;
                }
                else
                {
                    LocalPosition = value;
                }
            }
        }

        public float GlobalRotation
        {
            get
            {
                if (entity.Parent != null)
                {
                    return entity.Parent.Transform.GlobalRotation + LocalRotation;
                }
                else
                {
                    return LocalRotation;
                }
            }

            set
            {
                if (entity.Parent != null)
                {
                    LocalRotation = value - entity.Parent.Transform.GlobalRotation;
                }
                else
                {
                    LocalRotation = value;
                }
            }
        }

        public Vector2 GlobalScale
        {
            get
            {
                if (entity.Parent != null)
                {
                    return entity.Parent.Transform.GlobalScale + LocalScale;
                }
                else
                {
                    return LocalScale;
                }
            }

            set
            {
                if (entity.Parent != null)
                {
                    LocalScale = value - entity.Parent.Transform.GlobalScale;
                }
                else
                {
                    LocalScale = value;
                }
            }
        }
        #endregion

        #region local properties
        public Vector2 LocalPosition = new Vector2(0f, 0f);
        public float LocalRotation = 0;
        public Vector2 LocalScale = new Vector2(1f, 1f);
        #endregion

        private Entity entity = null;

        public Transform(Entity _entity)
        {
            entity = _entity;
        }

        public void Dispose()
        {
            entity = null;
            GC.SuppressFinalize(this);
        }
    }
    #endregion

    #region drawable interface
    public interface IDrawableEntity
    {
        bool Visible { get; set; }
        int Depth { get; set; }

        void OnDraw(GameTime _gameTime, SpriteBatch _spriteBatch);
    }
    #endregion
}
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace yukon.p8framework
{
    public abstract class Entity : IDisposable
    {
        #region properties
        public string Name = "Untitled Entity";
        public bool Enabled = true;
        public bool Visible = true;
        public int Depth = 0;

        public bool Disposed = false;

        // transform
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        #endregion

        #region references
        public Scene Scene;
        #endregion

        #region constructor
        public Entity(string _name, Scene scene = null)
        {
            // set the name
            Name = _name;

            // add to the proper scene
            if(scene != null)
            {
                Scene = scene;
            }
            else
            {
                Scene = p8.CurrentScene;
            }
            
            Scene.AddEntity(this);

            // awake!
            Awake();
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;

                if (Scene != null)
                {
                    Scene.RemoveEntity(this);
                    Scene = null;
                }

                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected abstract void Dispose(bool _disposing);
        #endregion

        #region game functions
        public abstract void Awake();

        public abstract void Update(GameTime _gameTime);
        #endregion

        #region transform class
        public class Transform
        {
            public Vector2 Position = new Vector2(0f, 0f);
            public float Rotation = 0;
            public Vector2 Scale = new Vector2(1f, 1f);

            public Entity Parent = null;
            public List<Entity> Children = new();
        }
        #endregion
    }
}
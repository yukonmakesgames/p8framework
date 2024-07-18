using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace yukon.p8f
{
    static class p8
    {
        #region references
        // scenes
        public static List<Scene> Scenes // all scenes
        {
            get { return scenes; }
        }
        private static List<Scene> scenes = new();

        public static Scene CurrentScene = null;

        // references
        public static Game Game;
        #endregion

        #region setup function
        public static void Setup(Game _game)
        {
            Game = _game;
        }
        #endregion

        #region game functions
        public static void Awake()
        {
            Debug.WriteLine("Hello World! -p8");
        }

        public static void Update(GameTime _gameTime)
        {
            foreach (var scene in scenes)
            {
                scene.Update(_gameTime);
            }
        }

        public static void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();

            foreach (var scene in scenes)
            {
                scene.Draw(_gameTime, _spriteBatch);
            }

            _spriteBatch.End();
        }
        #endregion
    }
}
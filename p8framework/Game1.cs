using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using yukon.p8framework;

namespace videogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Entity testEntity = null;
        WeakReference wr = null;
        WeakReference wrScene = null;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            p8.Setup(this);
            p8.Awake();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            p8.CurrentScene = new Scene("testing");

            testEntity = new TestEntity("test", p8.CurrentScene);
            wr = new WeakReference(testEntity);
            wrScene = new WeakReference(p8.CurrentScene);
        }

        protected override void Update(GameTime _gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            p8.Update(_gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                if (testEntity != null)
                {
                    Debug.WriteLine("nooo!!! you killed me!!! :'''''c");
                    testEntity.Dispose();
                    testEntity = null;

                    GC.Collect();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                if (p8.CurrentScene != null)
                {
                    Debug.WriteLine("woah wait you killed the entire scene!");
                    testEntity = null;
                    p8.CurrentScene.Dispose();

                    GC.Collect();
                }
            }

            Debug.WriteLine("object: " + wr.IsAlive.ToString() + ", scene: " + wrScene.IsAlive.ToString());

            base.Update(_gameTime);
        }

        protected override void Draw(GameTime _gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            p8.Draw(_gameTime, spriteBatch);

            base.Draw(_gameTime);
        }
    }
}

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

        WeakReference wrScene = null;
        WeakReference wrEntity = null;
        WeakReference wrChildEntity = null;

        private Entity testEntity = null;
        private Entity testChildEntity = null;

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
            wrScene = new WeakReference(p8.CurrentScene);

            testEntity = new TestEntity("test", p8.CurrentScene);
            wrEntity = new WeakReference(testEntity);

            testChildEntity = new TestEntity("test child", testEntity);
            wrChildEntity = new WeakReference(testChildEntity);

        }

        protected override void Update(GameTime _gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            p8.Update(_gameTime);

            

            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                if (p8.CurrentScene != null)
                {
                    Debug.WriteLine("killing scene");
                    testEntity = null;
                    testChildEntity = null;

                    p8.CurrentScene.Dispose();

                    GC.Collect();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                if (testEntity != null)
                {
                    Debug.WriteLine("killing entity");
                    testChildEntity = null;

                    testEntity.Dispose();
                    testEntity = null;

                    GC.Collect();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                if (testChildEntity != null)
                {
                    Debug.WriteLine("killing child entity");


                    testChildEntity.Dispose();
                    testChildEntity = null;

                    GC.Collect();
                }
            }

            Debug.WriteLine("scene: " + wrScene.IsAlive.ToString() + ", entity: " + wrEntity.IsAlive.ToString() + ", child entity: " + wrChildEntity.IsAlive.ToString());

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

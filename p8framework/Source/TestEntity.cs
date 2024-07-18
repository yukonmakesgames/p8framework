using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yukon.p8framework;

public class TestEntity : Entity, IDrawableEntity
{
    public TestEntity(string _name, Entity _parent) : base(_name, _parent)
    {
    }

    public bool Visible { get => visible; set => visible = value; }
    private bool visible = true;
    public int Depth { get => depth; set => depth = value; }
    private int depth = 0;

    public override void OnAwake()
    {
        
    }

    public override void OnDispose()
    {
        Debug.WriteLine("AAAAAAAAAAAAAA bye");
    }

    public void OnDraw(GameTime _gameTime, SpriteBatch _spriteBatch)
    {
        
    }

    public override void OnUpdate(GameTime _gameTime)
    {
        
    }
}
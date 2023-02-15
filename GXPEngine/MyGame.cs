using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.COBC;
using GXPEngine.COBC.Classes;

public class MyGame : Game {
	public MyGame() : base(1366, 786, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		// Draw some things on a canvas:
		EasyDraw canvas = new EasyDraw(1366,786);
		canvas.Clear(Color.Black);
		Player player1 = new Player(100, 100,true);
        Player player2 = new Player(100, 200);
		Platform test = new Platform(100, 300);

        // Add the canvas to the engine to display it:
        AddChild(canvas);
		AddChild(player1);
		AddChild(player2);
		AddChild(test);

		Console.WriteLine("MyGame initialized");
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}
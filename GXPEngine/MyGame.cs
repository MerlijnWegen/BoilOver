using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.COBC;
using GXPEngine.COBC.Classes;
using GXPEngine.COBC.Managers;

public class MyGame : Game {
	private PlayerManager playerManager;
	private PlatformManager platformManager;
	public MyGame() : base(1366, 786, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		playerManager = new PlayerManager();
		platformManager = new PlatformManager();
		// Draw some things on a canvas:
		EasyDraw canvas = new EasyDraw(1366,786, false);
		canvas.Clear(Color.Black);
		
		KillFloor killFloor = new KillFloor();

        // Add the canvas to the engine to display it:
        AddChild(canvas);
		playerManager.AddPlayers();
		playerManager.LoadPlayers();
		platformManager.AddPlatforms();
		platformManager.LoadPlatforms();
		
		AddChild(killFloor);
		platformManager.TogglePlatform();
        Console.WriteLine("MyGame initialized");
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		platformManager.Update();
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}
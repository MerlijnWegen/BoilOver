using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.COBC;
using GXPEngine.COBC.Classes;
using GXPEngine.COBC.Managers;

public class MyGame : Game {
	PlayerManager playerManager;
	PlatformManager platformManager;
	GameManager gameManager;
	AudioManager audioManager = new AudioManager();
	
	public MyGame() : base(1366, 786, false)     // Create a window that's 1366x786 and NOT fullscreen
	{
		gameManager = new GameManager();
        playerManager = new PlayerManager();
		platformManager = new PlatformManager(gameManager);
        
        // Draw some things on a canvas:
        EasyDraw canvas = new EasyDraw(1366,786, false);
		canvas.Clear(Color.Black);
		
		KillFloor killFloor = new KillFloor(playerManager);

        // Add the canvas to the engine to display it:
        AddChild(canvas);
		playerManager.LoadPlayers();
		platformManager.AddPlatforms();
		platformManager.LoadPlatforms();
		
		AddChild(killFloor);
		platformManager.TogglePlatformMovement();
		Console.WriteLine("MyGame initialized");

	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		platformManager.Update();
		playerManager.Update(); 
	}
    
	public GameManager GetGameManager()
	{
		return gameManager;
	}

    static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it

	}
}
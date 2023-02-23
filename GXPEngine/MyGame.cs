using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;  // System.Drawing contains drawing tools such as Color definitions
using System.IO;
using GXPEngine.COBC;
using GXPEngine.COBC.Classes;
using GXPEngine.COBC.Managers;
using System.IO.Ports;


public class MyGame : Game {
	PlayerManager playerManager;
	PlatformManager platformManager;
	GameManager gameManager;
	AudioManager audioManager = new AudioManager();


    public MyGame() : base(1366, 786, false)     // Create a window that's 1366x786 and NOT fullscreen
	{
		gameManager = new GameManager();
        playerManager = new PlayerManager();
		platformManager = new PlatformManager(gameManager,playerManager);
        
        // Draw some things on a canvas:
        //EasyDraw canvas = new EasyDraw(1366,786, false);
		Sprite background = new Sprite("background3.png", false, false);
		//canvas.Clear(Color.Black);
		//canvas.

        // Add the canvas to the engine to display it:
        AddChild(background);

		
		platformManager.AddStartingPlatforms();
		platformManager.LoadPlatforms();
        platformManager.StartPlatformMovement();
        playerManager.LoadPlayers();
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
	public PlayerManager GetPlayerManager()
	{
		return playerManager;
	}

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();
        SerialPort port = new SerialPort();
        port.PortName = "COM6";
        port.BaudRate = 9600;
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();
        //port.Write("7");
            // Create a "MyGame" and start it

        while (true)
        {

            string line = port.ReadLine(); // read separated values
                                           //string line = port.ReadExisting(); // when using characters
            if (line != "")
            {
                Console.WriteLine("Read from port: " + line);
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                port.Write(key.KeyChar.ToString());  // writing a string to Arduino
            }
        }
    }
}
using GXPEngine.COBC.Managers;
using GXPEngine;
using System;

public class LevelManager
{
    Game game = MyGame.main;
    PlatformManager platformManager;
    PlayerManager playerManager;
    HudManager hudManager;
    String activeScene = "menu";

    public LevelManager(PlatformManager platformManager,PlayerManager playerManager, HudManager hudManager)
    {
        this.platformManager = platformManager;
        this.playerManager = playerManager;
        this.hudManager = hudManager;
    }

    //clears the scene, adds the player to the game and shows the HUD. (health, score, highscore)
    public void StartGame()
    {
        hudManager.ClearScreen(activeScene);
        activeScene = "Game";
        platformManager.AddStartingPlatforms();
        platformManager.LoadPlatforms();
        platformManager.StartPlatformMovement();
        playerManager.LoadPlayers();
        hudManager.DisplayGameplayHUD();
        platformManager.AddKillFloor();
    }
    // clears the scene, shows Highscore with some text elements. (highscore)
    public void LoadMenu()
    {
        activeScene = "menu";
        hudManager.DisplayMenu();
    }
    public void ResetGame()
    {
        platformManager.Clear_Platforms();
    }
    public string GetCurrentScene()
    {
        return activeScene;
    }


}

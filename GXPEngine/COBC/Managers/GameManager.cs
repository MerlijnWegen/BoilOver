using GXPEngine.COBC.Classes;
using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Managers
{
    static class GameManager
    {
        static PlatformManager platformManager;
        static LevelManager levelManager;
        static PlayerManager playerManager;
        static HudManager hudManager;
        static float basePlatformVelocity = 0.2f;
        static int maxLives = 3;
        static string activeScene;
        static bool gameActive = false;
        static bool p1r = false;
        static bool p2r = false;



        static public void InitGame(LevelManager levelManager)
        {
            AudioManager.Init();
            levelManager.LoadMenu();
        }
        static public void SetManagers(PlatformManager platformManager1,LevelManager levelManager1,PlayerManager playerManager1,HudManager hudManager1)
        {
            platformManager = platformManager1;
            levelManager = levelManager1;
            playerManager = playerManager1;
            hudManager = hudManager1;
        }
        static public float GetBasePlatformVelocity()
        {
            return basePlatformVelocity;
        }
        static public int GetMaxLives()
        {
            return maxLives;
        }
        static public void GameOver(bool isPlayerOne)
        {
            platformManager.StopPlatformMovement();
            playerManager.SetPlayersInactive();
            hudManager.DisplayWinnerScreen(isPlayerOne);
            gameActive = false;
            
            
        }
        static public void Update()
        {
            platformManager.Update();
            playerManager.Update();
            hudManager.Update();
            CheckContinuebuttons();
        }

        static public void CheckContinuebuttons()
        {
            
            activeScene = levelManager.GetCurrentScene();
            if (Input.GetKeyDown(Key.R))
            {
                p1r= true;
            }
            if (Input.GetKeyDown(Key.P))
            {
                p2r = true;
            }
            if(activeScene == "menu" && p1r && p2r && !gameActive)
            {
                p1r = false;
                p2r = false;
                gameActive = true;
                activeScene= "game";
                Console.WriteLine("load game");
                levelManager.ResetGame();
                levelManager.StartGame();
                playerManager.ResetPlayers();
                playerManager.LoadPlayers();
            }
            else if (activeScene == "game" && gameActive)
            {
                hudManager.Update();
            }
            else
            {
                if (p1r && p2r && !gameActive)
                {
                    p1r = false;
                    p2r= false;
                    Console.WriteLine("Load menu");
                    hudManager.ClearScreen(activeScene);
                    platformManager.RemoveKillFloor();
                    levelManager.LoadMenu();
                }
            }

        }



    }
} 

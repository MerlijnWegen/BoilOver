using GXPEngine.COBC.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Managers
{
    public class HudManager

    {
        public ArrayList gamePlayHud = new ArrayList();
        public ArrayList mainMenuHud = new ArrayList();
        ArrayList activeHud= new ArrayList();

        Game game = MyGame.main;
        EasyDraw hudHealthP1;
        EasyDraw hudHealthP2;
        EasyDraw hudHealthPD1;
        EasyDraw hudHealthPD2;
        EasyDraw winTitle;
        EasyDraw winDesc;
        PlayerManager playerManager;

        EasyDraw gameTitle;
        EasyDraw gameDesc;
        Sprite background;
        public HudManager(PlayerManager playerManager)
        {
            SetupHUDElements();
            this.playerManager = playerManager;

        }
        public void SetupHUDElements()
        {
            background = new Sprite("background3.png", false, false);
            gamePlayHud.Add(hudHealthPD1 = new EasyDraw(game.width, game.height));
            gamePlayHud.Add(hudHealthPD2 = new EasyDraw(game.width, game.height));
            gamePlayHud.Add(hudHealthP1 = new EasyDraw(game.width, game.height));
            gamePlayHud.Add(hudHealthP2 = new EasyDraw(game.width, game.height));
            gamePlayHud.Add(winTitle= new EasyDraw(game.width, game.height));
            gamePlayHud.Add(winDesc = new EasyDraw(game.width, game.height));

            mainMenuHud.Add(gameTitle = new EasyDraw(game.width, game.height));
            mainMenuHud.Add(gameDesc = new EasyDraw(game.width,game.height));
            
        }
        public void DisplayGameplayHUD()
        {
            
            hudHealthPD1.TextAlign(CenterMode.Min, CenterMode.Min);
            hudHealthPD2.TextAlign(CenterMode.Max, CenterMode.Min);
            hudHealthP1.TextAlign(CenterMode.Min, CenterMode.Min);
            hudHealthP2.TextAlign(CenterMode.Max, CenterMode.Min);
            hudHealthPD1.Fill(255, 0, 0);
            hudHealthPD2.Fill(255, 0, 0);
            hudHealthP1.SetXY(0, 25);
            hudHealthP2.SetXY(0, 25);
            hudHealthPD1.Text("Lives player 1:");
            hudHealthPD2.Text("Lives player 2:");
            hudHealthP1.Text("TEST1");
            hudHealthP2.Text("TEST2");

            winTitle.TextAlign(CenterMode.Center, CenterMode.Center);
            winDesc.TextAlign(CenterMode.Center, CenterMode.Center);
            winTitle.TextSize(60);
            winTitle.Fill(0, 0, 0);
            winDesc.Fill(0, 0, 0);
            winTitle.SetXY(0, -200);
            winDesc.SetXY(0, -100);


            //gameplay hud elements
            foreach (EasyDraw canvas in gamePlayHud)
            {
                game.AddChild(canvas);
            }
        }
        public void DisplayWinnerScreen(bool playerOneDied)
        {
            if (playerOneDied)
            {
                winTitle.Text("PLAYER 2 WINS");
            }
            else
            {
                winTitle.Text("PLAYER 1 WINS");
            }
            winDesc.Text("                 PLAYER 1 & 2,\nPRESS FIRE TO RETURN TO MENU");
        }
        public void DisplayMenu()
        {
            gameTitle.TextAlign(CenterMode.Center, CenterMode.Center);
            gameDesc.TextAlign(CenterMode.Center, CenterMode.Center);
            gameTitle.TextSize(60);
            gameTitle.Fill(0, 0, 0);
            gameDesc.Fill(0, 0, 0);
            gameTitle.Text("BOIL OVER");
            gameDesc.Text("       PLAYER 1 & 2,\nPRESS FIRE TO PLAY");
            gameTitle.SetXY(0, -200);
            gameDesc.SetXY(0, -100);
            foreach (EasyDraw canvas in mainMenuHud)
            {
                game.AddChild(canvas);
            }
            
        }
        public void ClearScreen(string hudactive)
        {
            game.AddChild(background);
            switch (hudactive)
            {
                case "Menu": activeHud = mainMenuHud; break;
                case "Game": activeHud = gamePlayHud; break;
            }
            foreach (EasyDraw hudElement in activeHud)
            {
                hudElement.Text("");
                hudElement.ClearTransparent();
            }
        }
        public void Update()
        {
            updateGamePlayHud();
        }
        void updateGamePlayHud()
        {
            string player1lives = playerManager.GetPlayerLives(0).ToString();
            string player2lives = playerManager.GetPlayerLives(1).ToString();
            hudHealthP1.ClearTransparent();
            hudHealthP2.ClearTransparent();
            hudHealthP1.Text(player1lives);
            hudHealthP2.Text(player2lives);
        }
    }
}

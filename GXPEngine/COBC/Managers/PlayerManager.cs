using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Managers
{
    public class PlayerManager
    {
        ArrayList _players = new ArrayList();
        Game game = MyGame.main;
        
        public PlayerManager()
        {
            AddPlayers();
        }
        public void Update()
        {
            PlayerBoundry();
        }
        //add two players to _players
        public void AddPlayers(int x1 = 100, int y1 = 540, int x2 = 1200, int y2 = 540)
        {
            _players.Add(new Player(x1, y1, true));
            _players.Add(new Player(x2, y2));      
        }
        //load all players from _players into game (by adding to canvas)
        public void LoadPlayers()
        {
            foreach (Player player in _players)
            {
                game.AddChild(player);
            }
        }
        public void PlayerBoundry()
        {
            foreach (Player player in _players)
            {
                if (player.x >= game.width - 64)
                {
                    player.x = game.width - 64;
                }
                if (player.x <= 0)
                {
                    player.x = 0;
                }
            }
        }

        public void PlayerDied(Player player)
        {
            player.SetIframes(300);
            player.DecLive();
            if (player.GetLives() > 0)
            {
                player.x = player.GetLastPlatform().x + 64;
                player.y = player.GetLastPlatform().y - 64;
            }
            else
            {
                player.LateDestroy();
            }
        }
    }
}

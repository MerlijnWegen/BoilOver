using System.Collections;

namespace GXPEngine.COBC.Managers
{
    public class PlayerManager
    {
        ArrayList _players = new ArrayList();
        Game game = Game.main;
        public PlayerManager()
        {
            AddPlayers();
        }
        public void Update()
        {
            PlayerBoundry();
        }
        //add two players to _players
        public void AddPlayers(int x1 = 180, int y1 = 340, int x2 = 1100, int y2 = 340)
        {
            _players.Add(new Player(x1, y1, true));
            _players.Add(new Player(x2, y2));
        }
        //load all players from _players into game (by adding to canvas)
        public void LoadPlayers()
        {
            foreach (Player player in _players)
            {
                if (player.GetLives() > 0)
                {
                    game.AddChild(player);
                }
               
            }
        }
        public int GetPlayerLives(int identifier)
        {
            Player player = (Player)_players[identifier];
            return player.GetLives();
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
                if (player.y < -16)
                {
                    player.y = -16;
                }
            }
        }
        public void reloadPlayers()
        {
            foreach(Player player in _players)
            {
                game.RemoveChild(player);
                game.AddChild(player);
            }
        }
        public void SetPlayersActive()
        {
            foreach (Player player in _players)
            {
                player.SetIsActive();
            }
        }
        public void SetPlayersInactive()
        {
            foreach (Player player in _players)
            {
                player.SetIsInactive();
            }
        }
        public void PlayerDied(Player player)
        {
            player.SetIframes(300);
            player.DecLive();
            AudioManager.Play("playerWater");
            if (player.GetLives() > 0)
            {
                AudioManager.Play("loseLife");
                player.x = player.GetLastPlatform().x + 64;
                player.y = player.GetLastPlatform().y - 64;
            }
            else
            {
                player.LateRemove();
                GameManager.GameOver(player.IsPlayerOne());
                
            }
        }
        public void ResetPlayers()
        {
            RemovePlayers();
            AddPlayers();
        }
        public void RemovePlayers()
        {
            foreach(Player player in _players)
            {
                player.Destroy();
            }
            _players.Clear();
        }
    }
}

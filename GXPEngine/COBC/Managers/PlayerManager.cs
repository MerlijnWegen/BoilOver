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
        
        //add two players to _players
        public void AddPlayers()
        {
            _players.Add(new Player(100, 540, true));
            _players.Add(new Player(1200, 540));
        }
        //load all players from _players into game (by adding to canvas)
        public void LoadPlayers()
        {
            foreach (Player player in _players)
            {
                game.AddChild(player);
            }
        }
    }
}

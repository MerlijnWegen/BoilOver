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
        public void addPlayers()
        {
            _players.Add(new Player(100, 500, true));
            _players.Add(new Player(1200, 500));
        }
        //load all players from _players into game (by adding to canvas)
        public void loadPlayers()
        {
            foreach (Player p in _players)
            {
                game.AddChild(p);
            }
        }
    }
}

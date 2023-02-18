using GXPEngine.COBC.Managers;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Classes
{
    public class KillFloor : AnimationSprite
    {
        PlayerManager playerManager;
        public KillFloor(PlayerManager playerManager,string image = "placerholderLava.png") : base(image,1,1)
        {
            this.SetScaleXY(game.width, 1.5f);
            this.SetXY(0, 700);
            this.playerManager= playerManager;
        }

        void OnCollision(GameObject other)
        {
            if (other.name == "Background")
            {
                return;
            }
            if (other is Player player)
            {
                playerManager.PlayerDied(player);
            }
        }

    }
}

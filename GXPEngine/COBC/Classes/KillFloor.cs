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
        public KillFloor(string image = "placerholderLava.png") : base(image,1,1)
        {
            this.SetScaleXY(game.width, 1.5f);
            this.SetXY(0, 700);
        }

        void OnCollision(GameObject other)
        {
            if (other.name == "Background")
            {
                return;
            }
            if (other is Player)
            {
                other.LateDestroy();
            }
        }

    }
}

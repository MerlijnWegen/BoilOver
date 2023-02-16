using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Classes
{
    public class Projectile : Sprite
    {   
        bool isRight;
        Player parentPlayer;
        int killtimer = 500;

        public Projectile(Player player, bool isRight,string pImage) : base(pImage)
        {
            this.isRight = isRight;
            this.parentPlayer = player; 
            this.x = player.x;
            this.y = player.y;
            SetScaleXY(4, 4);
        }
        
        void Update()
        {
            killtimer--;
            if (killtimer <= 0)
            {
                this.LateDestroy();
            }
            if (isRight)
            {
                x -= 5f;
            }
            else
            {
                x += 5f;
            }
        }
        void OnCollision(GameObject other)
        {
            if(other is Player pOther)
            {
                if(pOther != parentPlayer)
                {
                    pOther.Knockback(isRight);
                    this.LateDestroy();
                }
            }
            
        }
    }
}

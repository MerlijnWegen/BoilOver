using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Classes
{
    public class Platform : Sprite
    {
        
        public Platform(int x, int y, string image = "placeholder.png", int width = 3) : base(image)
        {
            this.SetScaleXY(width,0.5f);
            this.SetXY(x, y);
        }
    }
}

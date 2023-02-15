using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Classes
{
    public class Platform : Sprite
    {
        
        public Platform(int x, int y, string image = "placeholder.png") : base(image)
        {
            this.SetScaleXY(2,1);
            this.SetXY(x, y);
        }
    }
}

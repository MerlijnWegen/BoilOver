namespace GXPEngine.COBC.Classes
{
    public class Shield : Sprite
    {
        Player player;
        bool xMirror;
        float targetX;
        public Shield(Player player, bool xMirror, string sImage = "Shield.png") : base(sImage)
        {
            this.player = player;
            this.xMirror = xMirror;
            if (xMirror)
            {
                targetX = x - 10;
            }
            else
            {
                targetX = x + 350;
            }
            this.SetXY(targetX, y);
            this.SetScaleXY(5.1f,5.1f);
        }

    }
}

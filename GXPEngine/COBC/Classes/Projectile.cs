namespace GXPEngine.COBC.Classes
{
    public class Projectile : Sprite
    {
        bool isRight;
        Player parentPlayer;
        int killtimer = 500;

        public Projectile(Player player, bool isRight, string pImage) : base(pImage)
        {
            this.isRight = isRight;
            this.parentPlayer = player;
            this.x = player.x + 10;
            this.y = player.y + 1;
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
            if (other is Shield)
            {
                this.LateDestroy();
            }
            if (other is Player pOther)
            {
                if (pOther != parentPlayer && !pOther.GetStunned())
                {
                    if (!pOther.GetStunned())
                    {
                        pOther.PlayerHit(isRight);

                    }
                    this.LateDestroy();
                }

            }

        }
    }
}

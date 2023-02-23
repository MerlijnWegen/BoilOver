namespace GXPEngine.COBC.Classes
{
    public class Projectile : Sprite
    {
        bool isRight;
        Player parentPlayer;
        int killtimer = 500;
        bool playedSound;

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
                x -= 10f;
            }
            else
            {
                x += 10f;
            }
        }
        void OnCollision(GameObject other)
        {
            if(other is KillFloor && !playedSound)
            {
                AudioManager.Play("platformWater");
                playedSound = true;
            }
            if (other is Shield)
            {
                AudioManager.Play("shield");
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

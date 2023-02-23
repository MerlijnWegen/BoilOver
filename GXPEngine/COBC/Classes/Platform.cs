using System;

namespace GXPEngine.COBC.Classes
{
    public class Platform : Sprite
    {
        AnimationSprite backgroundPlatform;
        float platformFriction = 0.15f;
        bool isBreakable = false;
        bool isBreaking = false;
        int killtimer = 210;

        public Platform(int x, int y, string image, bool isAnimated = false) : base("invisPlatform.png")
        {
            this.SetScaleXY(3f, 0.8f);
            this.SetXY(x, y);
            SetBackgroundPlatform(image, isAnimated);
        }
        public Platform(int x, int y, string image, float platformFriction = 0.15f, float width = 3f) : base("invisPlatform.png")
        {
            this.SetScaleXY(width, 0.8f);
            this.SetXY(x, y);
            this.platformFriction = platformFriction;
            SetBackgroundPlatform(image, false);
        }
        public Platform(int x, int y, bool breakable, string image, float width = 3f) : base("invisPlatform.png")
        {
            this.SetScaleXY(width, 0.8f);
            this.SetXY(x, y);
            this.isBreakable = true;
            SetBackgroundPlatform(image, true);
        }
        public float GetPlatformFriction()
        {
            return platformFriction;
        }
        void SetBackgroundPlatform(string image, bool isAnimated)
        {
            if (isAnimated)
            {
                backgroundPlatform = new AnimationSprite(image, 3, 3, 1);
                backgroundPlatform.SetXY(-5, -70);
                backgroundPlatform.SetScaleXY(0.2f, 1f);
                backgroundPlatform.SetCycle(1, 7, 30);
            }
            else
            {
                backgroundPlatform = new AnimationSprite(image, 1, 1);
                backgroundPlatform.SetXY(-2, -60);
                backgroundPlatform.SetScaleXY(0.2f, 1f);

            }
            AddChild(backgroundPlatform);
        }
        void Update()
        {
            if (isBreakable && isBreaking)
            {
                backgroundPlatform.Animate();
                killtimer--;
                if (killtimer <= 0)
                {
                    this.LateDestroy();
                    backgroundPlatform.LateDestroy();
                }
            }
        }
        public void SetBreaking(bool value)
        {

            if(isBreakable)
            {
                isBreaking = value;
            }
        }
    }
}

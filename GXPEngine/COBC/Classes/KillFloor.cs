using GXPEngine.COBC.Managers;
using System.Collections;

namespace GXPEngine.COBC.Classes
{
    public class KillFloor : Sprite
    {
        ArrayList _sprite = new ArrayList();
        PlayerManager playerManager;
        AnimationSprite whatever = new AnimationSprite("Spritesheetwater1.png", 5, 4, 18);
        public KillFloor(PlayerManager playerManager, string image = "invisPlatform.png") : base(image)
        {
            _sprite.Add(whatever);
            whatever.SetCycle(0, 18, 30);
            
            this.AddChild(whatever);
            this.SetScaleXY(game.width, 1.5f);
            whatever.SetScaleXY(0.0008f,1);
            this.SetXY(0, 750);
            whatever.SetXY(0, -500);
            whatever.alpha = 0.95f;
            whatever.SetColor(230, 230, 230);
            this.playerManager = playerManager;
        }
        void Update()
        {
            whatever.Animate();   
            foreach(AnimationSprite sprite in _sprite)
            {
                RemoveChild(sprite);
                AddChild(sprite);
            }
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

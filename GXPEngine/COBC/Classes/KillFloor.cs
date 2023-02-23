using GXPEngine.COBC.Managers;
using System.Collections;

namespace GXPEngine.COBC.Classes
{
    public class KillFloor : Sprite
    {
        ArrayList _sprite = new ArrayList();
        PlayerManager playerManager;
        AnimationSprite texture = new AnimationSprite("Spritesheetwater1.png", 5, 4, 18);
        public KillFloor(PlayerManager playerManager, string image = "invisPlatform.png") : base(image)
        {
            _sprite.Add(texture);
            texture.SetCycle(0, 18, 30);
            
            this.AddChild(texture);
            this.SetScaleXY(game.width, 1.5f);
            texture.SetScaleXY(0.0008f,1);
            this.SetXY(0, 750);
            texture.SetXY(0, -500);
            texture.alpha = 0.95f;
            texture.SetColor(230, 230, 230);
            this.playerManager = playerManager;
        }
        void Update()
        {
            texture.Animate();   
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

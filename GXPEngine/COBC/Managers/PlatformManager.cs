using GXPEngine.COBC.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC.Managers
{
    public class PlatformManager
    {
        ArrayList _platforms = new ArrayList();
        Game game = MyGame.main;
        GameManager gameManager;
        bool movePlatforms;
        private float lowerSpeed = 0.1f;
        //add platforms to _platforms
        public PlatformManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        public void AddPlatforms()
        {
            _platforms.Add(new Platform(50, 600, "placeholder.png", 20));
            _platforms.Add(new Platform(100, 400));
            _platforms.Add(new Platform(1000, 400));
            _platforms.Add(new Platform(550, 400));
            _platforms.Add(new Platform(350, 200));
            _platforms.Add(new Platform(750, 200));
            _platforms.Add(new Platform(100, 0));
            _platforms.Add(new Platform(1000, 0));
            _platforms.Add(new Platform(550, 0));
            _platforms.Add(new Platform(100, -200));
            _platforms.Add(new Platform(1000, -200));
            _platforms.Add(new Platform(550, -200));


        }
        //load all platforms from _platforms into the game.
        public void LoadPlatforms()
        {
            foreach (Platform p in _platforms)
            {
                game.AddChild(p);
            }
        }
        public void TogglePlatformMovement()
        {
            movePlatforms= !movePlatforms;
        }
        //update per frame.
        public void Update()
        {
            lowerSpeed = gameManager.GetBaseVelocity();
            LowerPlatforms();
        }
        //lower each platforms in _platforms by lowerSpeed every frame
        public void LowerPlatforms()
        {
            if (movePlatforms)
            {
                foreach (Platform p in _platforms.ToArray())
                {
                    p.y += lowerSpeed;
                    if (p.y >= 800)
                    {
                        _platforms.Remove(p);
                        p.LateDestroy();
                    }
                }
            }
        }
    }
}

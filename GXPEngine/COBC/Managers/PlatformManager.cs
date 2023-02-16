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
        bool movePlatforms;
        private float lowerSpeed = 0.1f;
        //add platforms to _platforms
        public void AddPlatforms()
        {
            _platforms.Add(new Platform(50, 600, "placeholder.png", 20));
            _platforms.Add(new Platform(100, 400));
            _platforms.Add(new Platform(1000, 400));
            _platforms.Add(new Platform(550, 300));
            _platforms.Add(new Platform(200, 200));
            _platforms.Add(new Platform(900, 200));
            _platforms.Add(new Platform(100, 0));
            _platforms.Add(new Platform(1000, 0));

        }
        //load all platforms from _platforms into the game.
        public void LoadPlatforms()
        {
            foreach (Platform p in _platforms)
            {
                game.AddChild(p);
            }
        }
        public void TogglePlatform()
        {
            movePlatforms= !movePlatforms;
        }
        //update per frame.
        public void Update()
        {
            LowerPlatforms();
        }
        //lower each platforms in _platforms by lowerSpeed every frame
        public void LowerPlatforms()
        {
            if (movePlatforms)
            {
                foreach (Platform p in _platforms)
                {
                    p.y += lowerSpeed;
                    if (p.y >= 800)
                    {
                        p.LateDestroy();
                    }
                }
            }
        }
    }
}

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
        PlayerManager playerManager;
        Random randomPLayout = new Random(Time.time + 325235);
        Random randomPVariant = new Random(Time.time + 583812);
        bool movePlatforms;
        private float lowerSpeed = 0.1f;
        //add platforms to _platforms
        public PlatformManager(GameManager gameManager, PlayerManager playerManager)
        {
            this.gameManager = gameManager;
            this.playerManager = playerManager;
        }
        public void AddStartingPlatforms()
        {
            GetPlatformVariant(100, randomPVariant.Next(0,2), 400);
            GetPlatformVariant(1000, randomPVariant.Next(0, 2), 400);
            GetPlatformVariant(550, randomPVariant.Next(0, 2), 400);
            GetPlatformVariant(350, randomPVariant.Next(0, 2),200);
            GetPlatformVariant(750, randomPVariant.Next(0, 2),200);
            
            GetPlatformVariant(200, 0);
            GetPlatformVariant(400, 1);
            GetPlatformVariant(600, 2);
            GetPlatformVariant(800, 3);
            GetPlatformVariant(1000, 4);
        }
        void AddKillFloor()
        {
            KillFloor killFloor = new KillFloor(playerManager);
            game.AddChild(killFloor);
        }
        public void GetPlatformVariant(int x, int selector, int y = 0)
        {
            switch(selector)
            {
                case 0: _platforms.Add(new Platform(x, y, "nPlatform1.png", false)); break; // normal platform
                case 1: _platforms.Add(new Platform(x, y, "nPlatform2.png", false)); break; // normal platform
                case 2: _platforms.Add(new Platform(x, y, "nPlatform3.png", false)); break; // normal platform
                case 3: _platforms.Add(new Platform(x, y, "sPlatform.png",0.01f)); break; //slippery platform
                case 4: _platforms.Add(new Platform(x, y,true,"cPlatform.png")); break; // breakable platform
            } 
        }
        public void GetPlatformLayout()
        {
            int selector = randomPLayout.Next(0,2);
            switch (selector)
            {
                case 0: GetPlatformVariant(600, randomPVariant.Next());  GetPlatformVariant(600, randomPVariant.Next()); break;
                case 1: GetPlatformVariant(600, randomPVariant.Next());  GetPlatformVariant(600, randomPVariant.Next()); break;
                case 2: GetPlatformVariant(600, randomPVariant.Next());  GetPlatformVariant(600, randomPVariant.Next()); break;
            }
        }
        //load all platforms from _platforms into the game.
        public void LoadPlatforms()
        {
            foreach (Platform p in _platforms)
            {
                game.AddChild(p);
            }
            AddKillFloor();
        }
        public void StartPlatformMovement()
        {
            movePlatforms = true;
        }
        public void StopPlatformMovement()
        {
            movePlatforms= false;
        }
        //update per frame.
        public void Update()
        {
            lowerSpeed = gameManager.GetBasePlatformVelocity();
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

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
        ArrayList _killPlatforms = new ArrayList();
        Game game = MyGame.main;
        Platform newPlatform;
        KillFloor killFloor;
        PlayerManager playerManager;
        Random randomPLayout = new Random(Time.time + 325235);
        Random randomPVariant = new Random(Time.time + 583812);

        bool movePlatforms;
        private float lowerSpeed = 0.1f;

        int platformCooldown = 1300;
        int platformCounter;
        //add platforms to _platforms
        public PlatformManager( PlayerManager playerManager)
        {
            this.playerManager = playerManager;
            platformCounter = platformCooldown;
        }
        public void AddStartingPlatforms()
        {
            GetPlatformVariant(100, randomPVariant.Next(0, 3), 400);
            GetPlatformVariant(1000, randomPVariant.Next(0, 3), 400);
            GetPlatformVariant(550, randomPVariant.Next(0, 3), 400);

            GetPlatformVariant(350, randomPVariant.Next(0, 3),200);
            GetPlatformVariant(750, randomPVariant.Next(0, 3), 200);

            GetPlatformVariant(100, randomPVariant.Next(0, 3), 0);
            GetPlatformVariant(1000, randomPVariant.Next(0, 3), 0);
        }
        public void AddKillFloor()
        {
            _killPlatforms.Add(killFloor = new KillFloor(playerManager));
            foreach(KillFloor killFloor in _killPlatforms)
            {
                game.AddChild(killFloor);
            }
        }
        public void ReloadKillFloor()
        {
            foreach (KillFloor killFloor in _killPlatforms)
            {
                game.RemoveChild(killFloor);
                game.AddChild(killFloor);
            }
        }
        public void RemoveKillFloor()
        {
            killFloor.LateRemove();
        }
        public void GetPlatformVariant(int x, int selector, int y = -64)
        {
            switch(selector)
            {
                case 0: newPlatform = new Platform(x, y, "nPlatform1.png", false); break; // normal platform
                case 1: newPlatform = new Platform(x, y, "nPlatform2.png", false); break; // normal platform
                case 2: newPlatform = new Platform(x, y, "nPlatform3.png", false); break; // normal platform
                case 3: newPlatform = new Platform(x, y, "sPlatform.png",0.01f); break; //slippery platform
                case 4: newPlatform = new Platform(x, y,true,"cPlatform.png"); break; // breakable platform
            }
            _platforms.Add(newPlatform);
            game.AddChild(newPlatform);
            playerManager.reloadPlayers();
            ReloadKillFloor();
        }
        public void GetRandomPlatformLayout()
        {
            int selector = randomPLayout.Next(1,4);
            switch (selector)
            {
                case 1:
                    GetPlatformVariant(100, randomPVariant.Next(0, 5));
                    GetPlatformVariant(1000, randomPVariant.Next(0, 5));
                    GetPlatformVariant(550, randomPVariant.Next(0, 5));
                    break;
                case 2:
                    GetPlatformVariant(350, randomPVariant.Next(0, 5));
                    GetPlatformVariant(750, randomPVariant.Next(0, 5)); 
                    break;
                case 3:
                    GetPlatformVariant(150, randomPVariant.Next(0, 5));
                    GetPlatformVariant(850, randomPVariant.Next(0, 5));
                    break;
            }
        }
        //load all platforms from _platforms into the game.
        public void LoadPlatforms()
        {
            foreach (Platform p in _platforms)
            {
                game.AddChild(p);
            }
            
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
            if (movePlatforms)
            {
                lowerSpeed = GameManager.GetBasePlatformVelocity();
                LowerPlatforms();
                platformCounter--;
                if (platformCounter <= 0)
                {
                    platformCounter = platformCooldown;
                    Console.WriteLine("Spawned platforms.");
                    GetRandomPlatformLayout();
                }
            }
            else
            {

            }
        }
        public void Clear_Platforms()
        {
            foreach (Platform p in _platforms)
            {
                p.Destroy();
            }
            _platforms.Clear();
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

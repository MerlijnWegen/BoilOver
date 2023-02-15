using GXPEngine.COBC.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.COBC
{
    public class Player : AnimationSprite
    {
        int speed = 3;
        int jumpSpeed = 10;
        float fallingvelocity;
        float jumpingvelocity;

        bool grounded;
        bool isPlayerOne;
        public Player(int x, int y, bool isPlayerOne = false, string Sprite = "placeholder.png", int columns = 1, int rows = 1) : base(Sprite, columns, rows)
        {
            this.isPlayerOne = isPlayerOne;
            this.SetXY(x, y);
            scale = 1f;
            _animationDelay = 5;
        }

        public void Update()
        {
            Playermove();
            MoveDown();
        }

        void Playermove() // controls of the player
        {
                if (grounded && Input.GetKey(Key.W) && isPlayerOne || grounded && Input.GetKey(Key.UP) && !isPlayerOne) // w
                {
                    y += -jumpSpeed;

                }
                if (Input.GetKey(Key.A) && isPlayerOne || Input.GetKey(Key.LEFT) && !isPlayerOne) // a
                {
                    x += -speed;
                    Mirror(true, false);
                }

                if (Input.GetKey(Key.D) && isPlayerOne || Input.GetKey(Key.RIGHT) && !isPlayerOne) // d
                {
                    x += +speed;
                    Mirror(false, false);
                }
            
        }
        void jump()
        {
            if (grounded)
            {
                grounded = false;
                jumpingvelocity = 10;
            }
            else
            {
                jumpingvelocity -= fallingvelocity;
            }
            y -= jumpingvelocity;
        }
        void MoveDown()
        {
            // This is the gravity of the player wich increases over time and has a maximum increase
            y += fallingvelocity;

            if (fallingvelocity < 5)
            {
                fallingvelocity += 0.1f;
            }
        }

        void OnCollision(GameObject collider)
        {
            // this checks if the collided object is am object from the background layer
            if (collider.name == "Background")
            {
                return;
            }
            // this checks for collisions with tiles on the foreground
            if (collider is Platform)
            {
                grounded = true;
                fallingvelocity = 0;
            }


        }

    }
}

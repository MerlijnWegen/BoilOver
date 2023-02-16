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
        bool jumping;
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
            GameBoundry();
            //HitTest();
        }
        //stops the movemement for the player if the edges of the game are reached.
        void GameBoundry()
        {
            if (x >= game.width - width)
            {
                x = game.width - width;
            }
            if (x <= 0)
            {
                x = 0;
            }
        }
        //controls the player WASD for player 1, arrow keys for player 2
        void Playermove() // controls of the player
        {
                if (grounded && Input.GetKeyDown(Key.W) && isPlayerOne || grounded && Input.GetKeyDown(Key.UP) && !isPlayerOne) // w
                {
                    Jump();

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
        //if player is grounded jump
        void Jump()
        {
            if (grounded)
            {
                jumping = true;
                grounded = false;
                jumpingvelocity = jumpSpeed;
            }
        }
        //move player down each frame, speed depends on whether the player is jumping or not
        void MoveDown()
        {
            if (jumping)
            {
                //speed decrease increases towards the end to round off the jump.
                y -= jumpingvelocity;
                if(jumpingvelocity <= 2)
                {
                    jumpingvelocity -= 0.1f;
                }
                else
                {
                    jumpingvelocity -= 0.2f;
                }
                if(jumpingvelocity <= 0)
                {
                    jumping= false;
                }
            }
            //gravity after jump, or when falling.
            if (!jumping)
            {
                // This is the gravity of the player wich increases over time and has a maximum increase
                y += fallingvelocity;

                if (fallingvelocity < 5)
                {
                    fallingvelocity += 0.2f;
                }
            }
            
        }
        //TODO
        void OnLateDestroy()
        {
            Console.WriteLine("Player 1 died: " + isPlayerOne);
        }
        /*
        void HitTest()
        {
            if(other is Platform)
                y -= 10f;
                Console.WriteLine("test");
        }
        */
        //if colliding with a platform, stop falling
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

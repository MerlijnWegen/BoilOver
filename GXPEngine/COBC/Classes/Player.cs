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
        float knockbackVelocity;
        float fallingVelocity;
        float jumpingVelocity;

        bool grounded;
        bool jumping;
        bool isPlayerOne;
        bool xMirror;
        bool knockback;
        bool isRight;
        public Player(int x, int y, bool isPlayerOne = false, string Sprite = "placeholder.png", int columns = 1, int rows = 1) : base(Sprite, columns, rows)
        {
            this.isPlayerOne = isPlayerOne;
            this.SetXY(x, y);
            scale = 1f;
            _animationDelay = 5;
        }

        public void Update()
        {
            if (!knockback)
            {
                Playermove();
            }
            MoveDown();
            GameBoundry();
            knockbacktemp();
            
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
            if (Input.GetKeyDown(Key.R) && isPlayerOne || Input.GetKeyDown(Key.M) && !isPlayerOne)
            {
                PlayerShoot();
            }
                if (grounded && Input.GetKeyDown(Key.W) && isPlayerOne || grounded && Input.GetKeyDown(Key.UP) && !isPlayerOne) // w
                {
                    Jump();

                }
                if (Input.GetKey(Key.A) && isPlayerOne || Input.GetKey(Key.LEFT) && !isPlayerOne) // a
                {
                    x += -speed;
                    xMirror= true;
                    Mirror(xMirror, false);
                }

                if (Input.GetKey(Key.D) && isPlayerOne || Input.GetKey(Key.RIGHT) && !isPlayerOne) // d
                {
                    x += +speed;
                    xMirror= false;
                    Mirror(xMirror, false);
                }
            
        }
        void PlayerShoot()
        {
            Projectile test = new Projectile(this, xMirror, "projPlaceholder.png");
            parent.AddChild(test);
        }
        //if player is grounded jump
        void Jump()
        {
            if (grounded)
            {
                jumping = true;
                grounded = false;
                jumpingVelocity = jumpSpeed;
            }
        }
        //move player down each frame, speed depends on whether the player is jumping or not
        void MoveDown()
        {
            //clean this code (divide the code)
            if (knockback)
            {
                y -= jumpingVelocity;
                if (jumpingVelocity <= 2)
                {
                    jumpingVelocity -= 0.1f;
                }
                else
                {
                    jumpingVelocity -= 0.2f;
                }
                if (jumpingVelocity <= 0)
                {
                    knockback= false;
                }
            }
            if (jumping)
            {
                //speed decrease increases towards the end to round off the jump.
                y -= jumpingVelocity;
                if(jumpingVelocity <= 2)
                {
                    jumpingVelocity -= 0.1f;
                }
                else
                {
                    jumpingVelocity -= 0.2f;
                }
                if(jumpingVelocity <= 0)
                {
                    jumping= false;
                }
            }
            //gravity after jump, or when falling.
            if (!jumping)
            {
                // This is the gravity of the player wich increases over time and has a maximum increase
                y += fallingVelocity;

                if (fallingVelocity < 2)
                {
                    fallingVelocity += 0.1f;
                }
                if( fallingVelocity > 2)
                {
                    fallingVelocity += 0.2f;
                }
            }
            
        }
        public void Knockback(bool isRight2)
        {
            isRight = isRight2;
            grounded = false;
            knockback = true;
            jumpingVelocity = 8;
            
            
        }
        void knockbacktemp()
        {
            if (knockback)
            {
                if (isRight)
                {
                    x -= jumpingVelocity;
                }
                else
                {
                    x += jumpingVelocity;
                }
            }
            
        }

        //TODO
        void OnLateDestroy()
        {
            Console.WriteLine("Player 1 died: " + isPlayerOne);
        }
        
        //if colliding with a platform, stop falling
        void OnCollision(GameObject collider)
        {
            // this checks for collisions with tiles on the foreground
            if (collider is Platform)
            {
                if(y <= collider.y - 32)
                {
                    y = collider.y - 64;
                    grounded = true;
                    fallingVelocity = 0;
                }
               
            }


        }

    }
}

using GXPEngine.COBC.Classes;
using GXPEngine.COBC.Managers;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine.COBC
{
    public class Player : AnimationSprite
    {
        Projectile projectile;
        Platform lastplatform;
        Shield shield;

        //movement
        float maxSpeed = 3.5f;
        int jumpSpeed = 10;
        bool isFacingRight;
        bool isGoingRight;
        bool isGoingLeft;
        bool grounded;
        bool xMirror;
        bool isStunned;

        //physics
        float fallingVelocity;
        float jumpingVelocity;
        float targetSpeed;
        bool hasMomentumRight;
        bool hasMomentumLeft;
        bool hasMomentum;

        //actions
        bool isBlocking;
        bool isJumping;
        int projectileCounter;
        float timer;
        float projectileCooldown = 0.1f;

        //other
        int lives = 3;
        bool isPlayerOne;
        int iFrames = 0;
        
        public Player( int x, int y, bool isPlayerOne = false, string Sprite = "placeholder.png", int columns = 1, int rows = 1) : base(Sprite, columns, rows)
        {
            this.isPlayerOne = isPlayerOne;
            this.SetXY(x, y);
            scale = 1f;
            _animationDelay = 5;
        }

        public void Update()
        {
            Playermove();
            PlayerMomentum();
            PlayerActions();
            PlayerGravity();
            PlayerKnockback();
            

        }
        //stops the movemement for the player if the edges of the game are reached.

        //controls the player WASD for player 1, arrow keys for player 2
        void Playermove() // controls of the player
        {
            if (!isStunned)
            {
                //jump up
                if (grounded && Input.GetKeyDown(Key.W) && isPlayerOne || grounded && Input.GetKeyDown(Key.UP) && !isPlayerOne) // w
                {
                    Jump();
                }
                //drop down
                if (Input.GetKeyDown(Key.S) && isPlayerOne || Input.GetKeyDown(Key.DOWN) && !isPlayerOne) // d
                {
                    y += 33;
                    grounded = false;
                }
            }
                // go left
                if (Input.GetKeyDown(Key.A) && isPlayerOne || Input.GetKeyDown(Key.LEFT) && !isPlayerOne) // a
                {
                    isGoingLeft= true;
                if (!isBlocking)
                {
                    xMirror = true;
                }
                    
                }
                //go right
                if (Input.GetKeyDown(Key.D) && isPlayerOne || Input.GetKeyDown(Key.RIGHT) && !isPlayerOne) // d
                {
                    isGoingRight= true;
                if (!isBlocking)
                {
                    xMirror = false;
                }
                    
                }
            Mirror(xMirror, false);
            //release left
            if (Input.GetKeyUp(Key.A) && isPlayerOne || Input.GetKeyUp(Key.LEFT) && !isPlayerOne) // a
            {
                isGoingLeft = false;
            }
            //release right
            if (Input.GetKeyUp(Key.D) && isPlayerOne || Input.GetKeyUp(Key.RIGHT) && !isPlayerOne) // d
            {
                isGoingRight= false;
            }
        }
        void PlayerMomentum()
        {
            
            if(!isGoingLeft && !isGoingRight && hasMomentum ||isBlocking || isStunned)
            {
                if(targetSpeed > 0)
                {
                    targetSpeed -= 0.15f;

                }
                else if(targetSpeed < 0)
                {
                    hasMomentumLeft = false;
                    hasMomentumRight= false;
                    hasMomentum = false;
                    targetSpeed = 0;
                }
            }
            if (isGoingLeft && !isGoingRight && !isBlocking && !isStunned)
            {
                if (!xMirror) { xMirror = true; Mirror(xMirror, false); }
                if (targetSpeed < maxSpeed)
                {
                    targetSpeed += 0.1f;
                }
                else
                {
                    targetSpeed = 3;
                }
                hasMomentum= true;
                hasMomentumLeft = true;
                hasMomentumRight = false;
            }
            if (isGoingRight && !isGoingLeft && !isBlocking && !isStunned)
            {
                if (xMirror) { xMirror = false; Mirror(xMirror, false); }
                if (targetSpeed < maxSpeed)
                {
                    targetSpeed += 0.1f;
                }
                else
                {
                    targetSpeed = 3;
                }
                hasMomentum = true;
                hasMomentumRight= true;
                hasMomentumLeft= false;
            }
            if (hasMomentumLeft && hasMomentum)
            {
                x -= targetSpeed;
            }
            else if(hasMomentumRight && hasMomentum)
            {
                x += targetSpeed;
            }
            
        }
        void PlayerActions()
        {
            if (!isStunned)
            {
                if (!isBlocking)
                {
                    if (Input.GetKeyDown(Key.R) && isPlayerOne || Input.GetKeyDown(Key.M) && !isPlayerOne)
                    {
                        PlayerShoot();
                    }
                }
                
                if (Input.GetKeyDown(Key.E) && isPlayerOne || Input.GetKeyDown(Key.N) && !isPlayerOne)
                {
                    PlayerBlock();
                    isBlocking = true;
                }
                if (Input.GetKeyUp(Key.E) && isPlayerOne || Input.GetKeyUp(Key.N) && !isPlayerOne)
                {
                    StopBlocking();
                }
            }
        }
        void PlayerShoot()
        {
            if(projectileCounter == 0)
            {
                projectile = new Projectile(this, xMirror, "projPlaceholder.png");
                parent.AddChild(projectile);
                projectileCounter++;
            }
            else
            {
                resetProjectileLimit();
            }
        }
        void resetProjectileLimit()
        {
            timer += Time.deltaTime;
            if(timer > projectileCooldown)
            {
                timer -= projectileCooldown;
                projectileCounter = 0;
            }
        }
        void PlayerBlock()
        {
           shield = new Shield(this,xMirror);
           AddChild(shield);
        }
        void StopBlocking()
        {
            isBlocking = false;
            if(shield != null)
            {
                shield.Remove();
            }
            
        }

        //if player is grounded jump
        void Jump()
        {
            if (grounded)
            {
                isJumping = true;
                grounded = false;
                jumpingVelocity = jumpSpeed;
            }
        }
        //move player down each frame, speed depends on whether the player is jumping or not
        
        public void PlayerHit(bool isFacingRight)
        {
            this.isFacingRight = isFacingRight;
            if(iFrames <= 0)
            {
                isStunned = true;
                grounded = false;
                jumpingVelocity = 8;
            }
            
        }
        void PlayerKnockback()
        {
            if(iFrames > 0)
            {
                iFrames--;
            }
            else if (isStunned && iFrames <= 0)
            {
                
                
                if (isBlocking)
                {
                    StopBlocking();
                }
                y -= jumpingVelocity;
                if (jumpingVelocity <= 2)
                {
                    jumpingVelocity -= 0.1f;
                }
                else
                {
                    jumpingVelocity -= 0.2f;
                }
                if (jumpingVelocity <= 0.5f)
                {
                    isStunned = false;
                }
                if (isFacingRight)
                {
                    x -= jumpingVelocity;
                }
                else
                {
                    x += jumpingVelocity;
                }
            }

        }
        void PlayerGravity()
        {
            if (isJumping)
            {
                    //speed decrease increases towards the end to round off the jump.
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
                    isJumping = false;
                }
            }
                //gravity after jump, or when falling.
            if (!isJumping)
            {
                // This is the gravity of the player wich increases over time and has a maximum increase
                y += fallingVelocity;

                if (fallingVelocity < 2)
                {
                    fallingVelocity += 0.1f;
                }
                if (fallingVelocity > 2)
                {
                    fallingVelocity += 0.2f;
                }
            }
        }
        

        //if colliding with a platform, stop falling
        void OnCollision(GameObject collider)
        {
            // this checks for collisions with tiles on the foreground
            if (collider is Platform platform)
            {
                lastplatform = platform;
                if (y <= collider.y - 32)
                {
                    y = collider.y - 64;
                    grounded = true;
                    fallingVelocity = 0;
                }

            }
        }
        public Platform GetLastPlatform()
        {
            return lastplatform;
        }
        public void SetLastPlatform(Platform value)
        {
            lastplatform = value;
        }
        public bool GetIsJumping()
        {
            return isJumping;
        }
        public float GetFallingVelocity()
        {
            return fallingVelocity;
        }
        public void SetFallingVelocity(float value)
        {
            fallingVelocity= value;
        }
        public float GetJumpingVelocity()
        {
            return jumpingVelocity;
        }
        public void SetJumpingVelocity(float value)
        {
            jumpingVelocity = value;
        }
        public bool GetStunned()
        {
            return isStunned;
        }
        public int GetLives()
        {
            return lives;
        }
        public bool getGrounded()
        {
            return grounded;
        }
        public void SetGrounded(bool value)
        {
            grounded = value;
        }
        public void DecLive()
        {
            lives--;
        }
        public void SetIframes(int value)
        {
            iFrames += value;
            if(iFrames < 0)
            {
                iFrames = 0;
            }
        }
    }
}

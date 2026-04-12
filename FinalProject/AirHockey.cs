using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirHockey
{
    enum PaddleDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    enum GameState
    {
        Stopped, 
        Playing, 
        GameOver
    }


    public partial class AirHockey : Form
    {
        int leftScore = 0;
        int rightScore = 0;
        int goalTop = 200;
        int goalBottom = 380;
        Random rand = new Random();
        const int WIN_SCORE = 5;

        Paddle leftPaddle;
        Paddle rightPaddle;
        Puck puck;

        PaddleDirection leftVertical = PaddleDirection.None;
        PaddleDirection rightHorizontal = PaddleDirection.None;

        PaddleDirection rightVertical = PaddleDirection.None;
        PaddleDirection leftHorizontal = PaddleDirection.None;

        GameState currentGameState = GameState.Stopped;

        SoundPlayer puckHitSound = new SoundPlayer(Application.StartupPath + @"\Resources\puckhit.wav");
        SoundPlayer goalSirenSound = new SoundPlayer(Application.StartupPath + @"\Resources\goalsiren.wav");

        public AirHockey()
        {
            InitializeComponent();

            this.KeyPreview = true;

            leftPaddle = new Paddle(picLeftPaddle, 18);
            rightPaddle = new Paddle(picRightPaddle, 18);
            puck = new Puck(picPuck, 7, 20);

            lblLeftScore.Text = "0";
            lblRightScore.Text = "0";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            leftScore = 0;
            rightScore = 0;

            lblLeftScore.Text = "0";
            lblRightScore.Text = "0";

            ResetPuck();

            currentGameState = GameState.Playing;
            gameTimer.Start();

            this.ActiveControl = null;
            this.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();

            Form1 startFrom = new Form1();
            startFrom.Show();
            this.Hide();
        }

        private void AirHockey_KeyDown(object sender, KeyEventArgs e)
        {
            // LEFT PLAYER
            if (e.KeyCode == Keys.W)
                leftVertical = PaddleDirection.Up;

            if (e.KeyCode == Keys.S)
                leftVertical = PaddleDirection.Down;

            if (e.KeyCode == Keys.A)
                leftHorizontal = PaddleDirection.Left;

            if (e.KeyCode == Keys.D)
                leftHorizontal = PaddleDirection.Right;

            // RIGHT PLAYER
            if (e.KeyCode == Keys.I)
                rightVertical = PaddleDirection.Up;

            if (e.KeyCode == Keys.K)
                rightVertical = PaddleDirection.Down;

            if (e.KeyCode == Keys.J)
                rightHorizontal = PaddleDirection.Left;

            if (e.KeyCode == Keys.L)
                rightHorizontal = PaddleDirection.Right;
        }

        private void AirHockey_KeyUp(object sender, KeyEventArgs e)
        {
            // LEFT PLAYER
            if (e.KeyCode == Keys.W && leftVertical == PaddleDirection.Up)
                leftVertical = PaddleDirection.None;

            if (e.KeyCode == Keys.S && leftVertical == PaddleDirection.Down)
                leftVertical = PaddleDirection.None;

            if (e.KeyCode == Keys.A && leftHorizontal == PaddleDirection.Left)
                leftHorizontal = PaddleDirection.None;

            if (e.KeyCode == Keys.D && leftHorizontal == PaddleDirection.Right)
                leftHorizontal = PaddleDirection.None;

            // RIGHT PLAYER
            if (e.KeyCode == Keys.I && rightVertical == PaddleDirection.Up)
                rightVertical = PaddleDirection.None;

            if (e.KeyCode == Keys.K && rightVertical == PaddleDirection.Down)
                rightVertical = PaddleDirection.None;

            if (e.KeyCode == Keys.J && rightHorizontal == PaddleDirection.Left)
                rightHorizontal = PaddleDirection.None;

            if (e.KeyCode == Keys.L && rightHorizontal == PaddleDirection.Right)
                rightHorizontal = PaddleDirection.None;
        }

        // This method runs every time the game time ticks.
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //If game is not currently played stop
            if (currentGameState != GameState.Playing)
            {
                return;
            }

            MovePaddles();
            puck.Move();
            CheckWallBounce();
            CheckSideWallBounce();
            CheckPaddleCollision();
            CheckForGoal();
            CheckWinner();

        }

        private void MovePaddles()
        {
            //Find middle of the form to prevent paddles from crossing into the other player's side
            int middleX = this.ClientSize.Width / 2;

            // LEFT PADDLE
            if (leftVertical == PaddleDirection.Up && picLeftPaddle.Top > 0)
                leftPaddle.MoveUp();

            if (leftVertical == PaddleDirection.Down && picLeftPaddle.Bottom < this.ClientSize.Height)
                leftPaddle.MoveDown();

            if (leftHorizontal == PaddleDirection.Left && picLeftPaddle.Left > 0)
                leftPaddle.MoveLeft();

            if (leftHorizontal == PaddleDirection.Right && picLeftPaddle.Right < middleX)
                leftPaddle.MoveRight();

            // RIGHT PADDLE
            if (rightVertical == PaddleDirection.Up && picRightPaddle.Top > 0)
                rightPaddle.MoveUp();

            if (rightVertical == PaddleDirection.Down && picRightPaddle.Bottom < this.ClientSize.Height)
                rightPaddle.MoveDown();

            if (rightHorizontal == PaddleDirection.Left && picRightPaddle.Left > middleX)
                rightPaddle.MoveLeft();

            if (rightHorizontal == PaddleDirection.Right && picRightPaddle.Right < this.ClientSize.Width)
                rightPaddle.MoveRight();
        }
        // This method makes the puck bounce off the walls
        private void CheckWallBounce()
        {
            if (picPuck.Top <= 0)
            {
                picPuck.Top = 0;
                puck.BounceVertical();
            }

            if (picPuck.Bottom >= this.ClientSize.Height)
            {
                puck.BounceVertical();
            }
        }

        // This method makes the puck bounce off the left and right walls, but only if it's not in the goal range
        private void CheckSideWallBounce()
        {

            if (picPuck.Left <= 0 && !PuckIsInGoalRange())
            {
                puck.BounceHorizontal();
                picPuck.Left = 0;
            }

            if (picPuck.Right >= this.ClientSize.Width && !PuckIsInGoalRange())
            {
                puck.BounceHorizontal();
                picPuck.Left = this.ClientSize.Width - picPuck.Width;
            }
        }

        // This method checks if the puck hits either paddle and changes the puck's direction and speed accordingly
        private void CheckPaddleCollision()
        {
            if (picPuck.Bounds.IntersectsWith(picLeftPaddle.Bounds))
            {
                puckHitSound.Play();

                //move puck so it is no longer inside the paddle
                picPuck.Left = picLeftPaddle.Right;

                //Send puck right and increase speed
                puck.speedX = Math.Abs(puck.speedX) + 3;

                //Finds center of paddle and puck
                int paddleCenter = picLeftPaddle.Top + (picLeftPaddle.Height / 2);
                int puckCenter = picPuck.Top + (picPuck.Height / 2);
                int difference = puckCenter - paddleCenter;

                //If the puck hits the top of the paddle send it up, if it hits the bottom send it down, if it hits the middle keep it straight
                if (difference < -20)
                {
                    puck.speedY = -6;
                }
                else if (difference < -5)
                {
                    puck.speedY = -3;
                }
                else if (difference > 20)
                {
                    puck.speedY = 6;
                }
                else if (difference > 5)
                {
                    puck.speedY = 3;
                }
                else
                {
                    if (puck.speedY < 0)
                        puck.speedY = -2;
                    else
                        puck.speedY = 2;
                }
                //add a little extra speed if the paddle is moving in a direction when it hits the puck
                if (leftVertical == PaddleDirection.Up)
                {
                    puck.speedY -= 2;
                }
                else if (leftVertical == PaddleDirection.Down)
                {
                    puck.speedY += 2;
                }
                else if (leftHorizontal == PaddleDirection.Right)
                {
                    puck.speedX += 1;
                }
                else if (leftHorizontal == PaddleDirection.Left)
                {
                    puck.speedX -= 1;
                }
               // Prevent puck from becoming too fast;
                LimitPuckSpeed();
            }

            if (picPuck.Bounds.IntersectsWith(picRightPaddle.Bounds))
            {
                puckHitSound.Play();

                picPuck.Left = picRightPaddle.Left - picPuck.Width;

                puck.speedX = -Math.Abs(puck.speedX) - 3;

                int paddleCenter = picRightPaddle.Top + (picRightPaddle.Height / 2);
                int puckCenter = picPuck.Top + (picPuck.Height / 2);
                int difference = puckCenter - paddleCenter;

                if (difference < -20)
                {
                    puck.speedY = -6;
                }
                else if (difference < -5)
                {
                    puck.speedY = -3;
                }
                else if (difference > 20)
                {
                    puck.speedY = 6;
                }
                else if (difference > 5)
                {
                    puck.speedY = 3;
                }
                else
                {
                    if (puck.speedY < 0)
                        puck.speedY = -2;
                    else
                        puck.speedY = 2;
                }

                if (rightVertical == PaddleDirection.Up)
                {
                    puck.speedY -= 2;
                }
                else if (rightVertical == PaddleDirection.Down)
                {
                    puck.speedY += 2;
                }
                else if (rightVertical == PaddleDirection.Right)
                {
                    puck.speedX += 1;
                }
                else if (rightVertical == PaddleDirection.Left)
                {
                    puck.speedX -= 1;
                }
            }
        }
        private void LimitPuckSpeed()
        {
            //Limits horizontal speed
            if (puck.speedX > 20)
                puck.speedX = 20;
            if (puck.speedX < -20)
                puck.speedX = -20;

            // Limits vertical speed
            if (puck.speedY > 13)
                puck.speedY = 13;
            if (puck.speedY < -13)
                puck.speedY = -13;
        }
        // this method checks if the puck is in the goal range from the top to bottom of the goal
        private bool PuckIsInGoalRange()
        {
            // find puck center
            int puckCenterY = picPuck.Top + (picPuck.Height / 2);

            return puckCenterY >= goalTop && puckCenterY <= goalBottom;

        }

        // checks if puck scored on left or right side and updates score, plays sound, and resets puck
        private void CheckForGoal()
        {
            // if it goes in left side right player scores
            if (picPuck.Left <= 0 && PuckIsInGoalRange())
            {
                rightScore++;
                lblRightScore.Text = rightScore.ToString();

                goalSirenSound.Play();

                ResetPuck();
            }
            // if it goes in right side left player scores
            if (picPuck.Right >= this.ClientSize.Width && PuckIsInGoalRange())
            {
                leftScore++;
                lblLeftScore.Text = leftScore.ToString();

                goalSirenSound.Play();

                ResetPuck();
            }
        }
        // Either players reached the capped score set
        private void CheckWinner()
        {// Check if left player wins
            if (leftScore >= WIN_SCORE)
            {
                currentGameState = GameState.GameOver;
                gameTimer.Stop();
                MessageBox.Show("Left Player Wins!");
            }
            // Check if right player wins
            if (rightScore >= WIN_SCORE)
            {
                currentGameState = GameState.GameOver;
                gameTimer.Stop();
                MessageBox.Show("Right Player Wins!");
            }
        }
        // restes puck in the middle of the form and gives it a random direction and speed to start moving towards one of the players
        private void ResetPuck()
        {
            // put puck in the center of the form
            picPuck.Left = (this.ClientSize.Width / 2) - (picPuck.Width / 2);
            picPuck.Top = (this.ClientSize.Height / 2) - (picPuck.Height / 2);
            // random horizontal direction and speed
            puck.speedX = rand.Next(0,2) == 0 ? 7 : -7;
            // random vertical speed
            puck.speedY = rand.Next(-6, 6);

            if (puck.speedY == 0)
            {
                puck.speedY = 2;
            }
        }

        private void AirHockey_Load(object sender, EventArgs e)
        {

        }
    }
}

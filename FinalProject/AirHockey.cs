using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        const int WIN_SCORE = 5;

        Paddle leftPaddle;
        Paddle rightPaddle;
        Puck puck;

        PaddleDirection leftDirection = PaddleDirection.None;
        PaddleDirection rightDirection = PaddleDirection.None;

        GameState currentGameState = GameState.Stopped;

        public AirHockey()
        {
            InitializeComponent();

            this.KeyPreview = true;

            leftPaddle = new Paddle(picLeftPaddle, 23);
            rightPaddle = new Paddle(picRightPaddle, 23);
            puck = new Puck(picPuck, 5, 10);

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
            //Left paddle controls
            if (e.KeyCode == Keys.W)
            {
                leftDirection = PaddleDirection.Up;
            }

            if (e.KeyCode == Keys.S)
            {
                leftDirection = PaddleDirection.Down;
            }

            if (e.KeyCode == Keys.A)
            {
                leftDirection |= PaddleDirection.Left;
            }
            if (e.KeyCode == Keys.D)
            {
                leftDirection |= PaddleDirection.Right;
            }

            //Right Paddle Controls
            if (e.KeyCode == Keys.I)
            {
                rightDirection = PaddleDirection.Up;
            }

            if (e.KeyCode == Keys.K)
            {
                rightDirection = PaddleDirection.Down;
            }

            if (e.KeyCode == Keys.J)
            {
                rightDirection = PaddleDirection.Left;
            }

            if (e.KeyCode == Keys.L)
            {
                rightDirection = PaddleDirection.Right;
            }
        }

        private void AirHockey_KeyUp(object sender, KeyEventArgs e)
        {
            //Left Paddle
            if (e.KeyCode == Keys.W && leftDirection == PaddleDirection.Up)
            {
                leftDirection = PaddleDirection.None;
            }

            if (e.KeyCode == Keys.S && leftDirection == PaddleDirection.Down)
            {
                leftDirection = PaddleDirection.None;
            }
            if (e.KeyCode == Keys.A && leftDirection == PaddleDirection.Left)
            {
                leftDirection = PaddleDirection.None;
            }
            if (e.KeyCode == Keys.D && leftDirection == PaddleDirection.Right)
            {
                leftDirection = PaddleDirection.None;
            }

            //Right Paddle
            if (e.KeyCode == Keys.I && rightDirection == PaddleDirection.Up)
            {
                rightDirection = PaddleDirection.None;
            }

            if (e.KeyCode == Keys.K && rightDirection == PaddleDirection.Down)
            {
                rightDirection = PaddleDirection.None;
            }

            if(e.KeyCode == Keys.J && rightDirection == PaddleDirection.Left)
            {
                rightDirection = PaddleDirection.None;
            }

            if(e.KeyCode == Keys.L && rightDirection == PaddleDirection.Right)
            {
                rightDirection = PaddleDirection.None;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
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
            //Left
            if (leftDirection == PaddleDirection.Up && picLeftPaddle.Top > 0)
            {
                leftPaddle.MoveUp();
            }

            if (leftDirection == PaddleDirection.Down && picLeftPaddle.Bottom < this.ClientSize.Height)
            {
                leftPaddle.MoveDown();
            }

            if (leftDirection == PaddleDirection.Left && picLeftPaddle.Left > 0)
                 leftPaddle.MoveLeft();

            if (leftDirection == PaddleDirection.Right && picLeftPaddle.Right < this.ClientSize.Width / 2)
                leftPaddle.MoveRight();


            //Right
            if (rightDirection == PaddleDirection.Up && picRightPaddle.Top > 0)
            {
                rightPaddle.MoveUp();
            }

            if (rightDirection == PaddleDirection.Down && picRightPaddle.Bottom < this.ClientSize.Height)
            {
                rightPaddle.MoveDown();
            }

            if (rightDirection == PaddleDirection.Left && picRightPaddle.Left > this.ClientSize.Width / 2)
                rightPaddle.MoveLeft();

            if (rightDirection == PaddleDirection.Right && picRightPaddle.Right < this.ClientSize.Width)
                rightPaddle.MoveRight();
        }

        private void CheckWallBounce()
        {
            if (picPuck.Top <= 0)
            {
                puck.BounceVertical();
            }

            if (picPuck.Bottom >= this.ClientSize.Height)
            {
                puck.BounceVertical();
            }
        }

        private void CheckSideWallBounce()
        {
            
            if(picPuck.Left <= 0 && !PuckIsInGoalRange())
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

        private void CheckPaddleCollision()
        {
            if (picPuck.Bounds.IntersectsWith(picLeftPaddle.Bounds))
            {
                puck.speedX = Math.Abs(puck.speedX);

                int paddleCenter = picLeftPaddle.Top + (picLeftPaddle.Height / 2);
                int puckCenter = picPuck.Top + (picPuck.Height / 2);

                if (puckCenter < paddleCenter - 15)
                {
                    puck.speedY = -5;
                }
                else if (puckCenter > paddleCenter + 15)
                {
                    puck.speedY = 5;
                }
                else
                {
                    puck.speedY = 0;
                }
            }

             if (picPuck.Bounds.IntersectsWith(picRightPaddle.Bounds))
            {
               puck.speedX = -Math.Abs(puck.speedX);

                int paddleCenter = picRightPaddle.Top + (picRightPaddle.Height / 2);
                int puckCenter = picPuck.Top + (picPuck.Height / 2);

                if (puckCenter < paddleCenter - 15)
                {
                    puck.speedY = -5;
                }
                else if (puckCenter > paddleCenter + 15)
                {
                    puck.speedY = 5;
                }
                else
                {
                    puck.speedY = 0;
                }
            }

            
        }

        private bool PuckIsInGoalRange()
        {
            int puckCenterY = picPuck.Top + (picPuck.Height / 2);

            return puckCenterY >= goalTop && puckCenterY <= goalBottom;
            
        }
        

        private void CheckForGoal()
        {
         

            if (picPuck.Left <= 0 && PuckIsInGoalRange())
            {
                rightScore++;
                lblRightScore.Text = rightScore.ToString();
                ResetPuck();
            }

            if (picPuck.Right >= this.ClientSize.Width && PuckIsInGoalRange())
            {
                leftScore++;
                lblLeftScore.Text = leftScore.ToString();
                ResetPuck();
            }
        }

        private void CheckWinner()
        {
            if (leftScore >= WIN_SCORE)
            {
                currentGameState = GameState.GameOver;
                gameTimer.Stop();
                MessageBox.Show("Left Player Wins!");
            }

            if (rightScore >= WIN_SCORE)
            {
                currentGameState = GameState.GameOver;
                gameTimer.Stop();
                MessageBox.Show("Right Player Wins!");
            }
        }

        private void ResetPuck()
        {
            picPuck.Left = (this.ClientSize.Width / 2) - (picPuck.Width / 2);
            picPuck.Top = (this.ClientSize.Height / 2) - (picPuck.Height / 2);

            puck.speedX = 25;
            puck.speedY = 15;
        }

        
    }
}

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

        private void CheckPaddleCollision()
        {
            if (picPuck.Bounds.IntersectsWith(picLeftPaddle.Bounds))
            {
                puckHitSound.Play();


                picPuck.Left = picLeftPaddle.Right;

                puck.speedX = Math.Abs(puck.speedX) + 3;

                int paddleCenter = picLeftPaddle.Top + (picLeftPaddle.Height / 2);
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
            if (puck.speedX > 20)
                puck.speedX = 20;
            if (puck.speedX < -20)
                puck.speedX = -20;

            if (puck.speedY > 13)
                puck.speedY = 13;
            if (puck.speedY < -13)
                puck.speedY = -13;
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

                goalSirenSound.Play();

                ResetPuck();
            }

            if (picPuck.Right >= this.ClientSize.Width && PuckIsInGoalRange())
            {
                leftScore++;
                lblLeftScore.Text = leftScore.ToString();

                goalSirenSound.Play();

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

            puck.speedX = rand.Next(0,2) == 0 ? 7 : -7;
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

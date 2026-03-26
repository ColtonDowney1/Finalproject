using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirHockey
{
     class Paddle
    {
        public PictureBox paddleBox;
        public int speed;

        public Paddle(PictureBox pb, int paddleSpeed)
        {
            paddleBox = pb; 
            speed = paddleSpeed;

        }

        public void MoveUp()
        {
            paddleBox.Top -= speed;
        }

        public void MoveDown()
        {
            paddleBox.Top += speed;
        }
        public void MoveLeft()
        {
            paddleBox.Left -= speed;
        }

        public void MoveRight()
        {
            paddleBox.Left += speed;
        }
    }
}

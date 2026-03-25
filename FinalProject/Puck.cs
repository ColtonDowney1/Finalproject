using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirHockey
{
     class Puck
    {
        public PictureBox puckBox;
        public int speedX;
        public int speedY;

        public Puck(PictureBox pb, int xspeed, int yspeed)
        {
            puckBox = pb;
            speedX = xspeed;
            speedY = yspeed;            
        }
        public void Move()
        {
            puckBox.Left += speedX;
            puckBox.Top += speedY;
        }
        public void BounceVertical()
        {
            speedY = -speedY;

        }
        public void BounceHorizontal()
        {
            speedX = -speedX; 
        }
        
    }
}

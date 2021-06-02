using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip
{
    public class HpAnimation
    {
        Bitmap[] images;
        int frame;
        public HpAnimation(Bitmap[] Frames)
        {
            images = Frames; //load all 21 frames
            frame = images.Length-1; //start with full hp
        }

        public Bitmap GiveNextImage(int hpPlayer)
        {
            Bitmap bitmap = null;
            if (frame > 0 )
            {
                if (hpPlayer < frame)
                {
                    frame--;
                }
                else if (hpPlayer > frame)
                {
                    frame++;
                }
                bitmap = images[frame];
                return bitmap;
            }
            else
                return bitmap = images[0];
        }

    }
}

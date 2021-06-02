using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public class ExplosionAnimation
    {
        Bitmap[] images;
        int frame;
        public ExplosionAnimation(Bitmap[] Frames)
        {
            images = Frames;
            frame = 0;
        }
        public async Task GiveNextImageAsync(PictureBox explosion)
        {
            Bitmap bitmap = null;

            for (int i = 0; i < 26; i++)
            {
                if (images.Length > frame)
                {
                    bitmap = images[frame++];
                }
                else
                {
                    bitmap = images[frame - 1];
                    frame = 0;
                }
                explosion.Image = bitmap;
                await Task.Delay(75);
            }
            explosion.Dispose();
        }
    }
}

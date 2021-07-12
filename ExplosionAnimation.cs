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
        Bitmap[] images = new Bitmap[] { Properties.Resources.explosion1,
            Properties.Resources.explosion2, Properties.Resources.explosion3, Properties.Resources.explosion4,
            Properties.Resources.explosion5,Properties.Resources.explosion6,Properties.Resources.explosion7,Properties.Resources.explosion8,
            Properties.Resources.explosion9,Properties.Resources.explosion10,Properties.Resources.explosion11,Properties.Resources.explosion12
            ,Properties.Resources.explosion13,Properties.Resources.explosion14,Properties.Resources.explosion15,Properties.Resources.explosion16
            ,Properties.Resources.explosion17,Properties.Resources.explosion18,Properties.Resources.explosion19,Properties.Resources.explosion20
            ,Properties.Resources.explosion21,Properties.Resources.explosion22,Properties.Resources.explosion23,Properties.Resources.explosion24
            ,Properties.Resources.explosion25,Properties.Resources.explosion26};
        int frame = 0;

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

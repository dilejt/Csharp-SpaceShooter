using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public class CreateSmallEnemy : CreateEnemy
    {
        public CreateSmallEnemy(PictureBox pictureBoxBg, Engine engine) : base(pictureBoxBg, engine)
        {

        }
        protected override void InitializeEnemy(PictureBox pictureBoxBg)
        {
            Random rand = new Random();
            if (engine.EnemySmallCount < engine.EnemySmallMaxCount && ((engine.Time / 100) % rand.Next(30, 40) == 0))
            {
                enemy = new PictureBox();
                parentBox = pictureBoxBg;
                enemy.BackgroundImage = Properties.Resources.enemySmall;
                enemy.Size = new Size(60, 60);
                enemy.BackColor = Color.Transparent;
                enemy.Tag = "enemySmall";
                enemy.BackgroundImageLayout = ImageLayout.Stretch;
                enemy.Left = rand.Next(0, pictureBoxBg.Parent.Width - enemy.Width);
                enemy.Top = 0 - enemy.Height;
                if (!CollisionDetection("enemySmall"))
                {
                    engine.EnemySmallCount++;
                    parentBox.Controls.Add(enemy);
                }
            }
        }

    }
}

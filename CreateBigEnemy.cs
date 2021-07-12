using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceShip
{
    public class CreateBigEnemy : CreateEnemy
    {
        public CreateBigEnemy(PictureBox pictureBoxBg, Engine engine) : base(pictureBoxBg,engine)
        {

        }

        protected override void InitializeEnemy(PictureBox pictureBoxBg)
        {
            Random rand = new Random();
            if (engine.EnemyBigCount < engine.EnemyBigMaxCount && (engine.Time / 100 % rand.Next(35, 40) == 0))
            {
                enemy = new PictureBox();
                parentBox = pictureBoxBg;
                enemy.BackgroundImage = Properties.Resources.enemyBig;
                enemy.Size = new Size(80, 108);
                enemy.BackColor = Color.Transparent;
                enemy.Tag = "enemyBig";
                enemy.BackgroundImageLayout = ImageLayout.Stretch;
                enemy.Left = rand.Next(0, pictureBoxBg.Parent.Width - enemy.Width);
                enemy.Top = 0 - enemy.Height;
                if (!CollisionDetection("enemyBig"))
                {
                    engine.EnemyBigCount++;
                    parentBox.Controls.Add(enemy);
                }
            }
        }
    }
}

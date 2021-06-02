using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public class CreateSmallEnemy 
    {
        private bool isCollide = false;
        PictureBox parentBox;
        PictureBox enemy;
        Engine engine;
        public CreateSmallEnemy(PictureBox pictureBoxBg, Engine engine)
        {
            this.engine = engine;
            Random rand = new Random();
            if (engine.EnemySmallCount < engine.EnemySmallMaxCount && ((engine.Time / 100) % rand.Next(30, 40) == 0))
            {
                if (engine.isEnemySmall == false)
                {
                    enemy = new PictureBox();
                    parentBox = pictureBoxBg;
                    engine.isEnemySmall = true;
                    enemy.BackgroundImage = Properties.Resources.enemySmall;
                    enemy.Size = new Size(60, 60);
                    enemy.BackColor = Color.Transparent;
                    enemy.Tag = "enemySmall";
                    enemy.BackgroundImageLayout = ImageLayout.Stretch;
                    enemy.Left = rand.Next(0, pictureBoxBg.Parent.Width - enemy.Width);
                    enemy.Top = 0 - enemy.Height;
                    collisionDetection();
                }
            }
            else
            {
                engine.isEnemySmall = false;
            }
        }

        public void collisionDetection()
        {
            foreach (Control enemySmall in parentBox.Controls)
            {
                if (enemySmall.Tag != null && enemySmall.Tag.ToString() == "enemySmall")
                {
                    if (enemySmall.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        isCollide = true;
                        break;
                    }
                }
            }
            if (!isCollide)
            {
                engine.EnemySmallCount++;
                parentBox.Controls.Add(enemy);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public class CreateBigEnemy
    {
        private bool isCollide = false;
        PictureBox parentBox;
        PictureBox enemy;
        Engine engine;
        public CreateBigEnemy(PictureBox pictureBoxBg, Engine engine)
        {
            this.engine = engine;
            Random rand = new Random();
            if (engine.EnemyBigCount < engine.EnemyBigMaxCount && ((engine.Time / 100) % rand.Next(35, 40) == 0))
            {
                if (engine.isEnemySmall == false)
                {
                    enemy = new PictureBox();
                    parentBox = pictureBoxBg;
                    engine.isEnemyBig = true;
                    enemy.BackgroundImage = Properties.Resources.enemyBig;
                    enemy.Size = new Size(80, 108);
                    enemy.BackColor = Color.Transparent;
                    enemy.Tag = "enemyBig";
                    enemy.BackgroundImageLayout = ImageLayout.Stretch;
                    enemy.Left = rand.Next(0, pictureBoxBg.Parent.Width - enemy.Width);
                    enemy.Top = 0 - enemy.Height;
                    collisionDetection();
                }
            }
            else
            {
                engine.isEnemyBig = false;
            }
        }

        public void collisionDetection()
        {
            foreach (Control enemyBig in parentBox.Controls)
            {
                if (enemyBig.Tag != null && enemyBig.Tag.ToString() == "enemyBig")
                {
                    if (enemyBig.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        isCollide = true;
                        break;
                    }
                }
            }
            if (!isCollide)
            {
                engine.EnemyBigCount++;
                parentBox.Controls.Add(enemy);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public abstract class CreateEnemy
    {
        protected PictureBox parentBox;
        protected PictureBox enemy;
        protected Engine engine;

        public CreateEnemy(PictureBox pictureBoxBg, Engine engine)
        {
            this.engine = engine;
            InitializeEnemy(pictureBoxBg);
        }

        protected abstract void InitializeEnemy(PictureBox pictureBoxBg);
        public bool CollisionDetection(string tagName)
        {
            foreach (Control enemyControl in parentBox.Controls)
            {
                if (enemyControl.Tag != null && enemyControl.Tag.ToString() == tagName)
                {
                    if (enemyControl.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}

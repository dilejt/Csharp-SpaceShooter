using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public class CustomButton : PictureBox
    {
        public Bitmap buttonMask;
        public Bitmap buttonClicked;
        public Bitmap button;
        public CustomButton(Bitmap buttonMask, Bitmap buttonClicked, Bitmap button, string buttonName, int buttonWidth, int buttonHeight)
        {
            this.buttonMask = buttonMask;
            this.buttonClicked = buttonClicked;
            this.button = button;
            this.BackColor = Color.Transparent;
            this.Image = button;
            this.Name = buttonName;
            this.Width = buttonWidth;
            this.Height = buttonHeight;
        }
        public bool MouseIsOverButton(Point location)
        {
            if (location.X < 0) return false;
            if (location.Y < 0) return false;
            if (location.X >= this.buttonMask.Width) return false;
            if (location.Y >= this.buttonMask.Height) return false;

            Color color = this.buttonMask.GetPixel(location.X, location.Y);
            return ((color.A == 255) && (color.R == 0) && (color.G == 0) && (color.B == 0));
        }
        public void hoverEffect(MouseEventArgs e)
        {
            Image image = this.button;
            if (this.MouseIsOverButton(e.Location))
            {
                image = this.buttonClicked;
            }
            else
            {
                image = this.button;
            }

            if (this.Image != image)
                this.Image = image;
        }
        public void CenterPictureBoxHorizontal(PictureBox picBox)
        {
            picBox.Left = (picBox.Parent.ClientSize.Width / 2) - (picBox.Image.Width / 2);
        }
        public void CenterPictureBoxVertical(PictureBox picBox, double height)
        {
            picBox.Top = Convert.ToInt32((picBox.Parent.ClientSize.Height / height) - (picBox.Image.Height / height));
        }
    }
}

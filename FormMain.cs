using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public partial class FormMain : Form
    {
        Engine engine;
        CustomButton pictureBoxPlay;
        CustomButton pictureBoxLeader;
        CustomButton pictureBoxExit;
        CustomButton backToMenuButton;
        TableLayoutPanel tableLeaderBoard;
        bool leaderBoardMenuInitialized = false;
        public FormMain()
        {
            InitializeComponent();
            Screen myScreen = Screen.FromControl(this);
            Rectangle area = myScreen.WorkingArea;
            this.Height = area.Height;
            this.Width = Convert.ToInt32(Convert.ToDouble(this.Height) * (6.0 / 9.0));
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            initializeGameMenu();
        }

        private void initializeGameMenu()
        {
            pictureBoxPlay = new CustomButton(Properties.Resources.buttonMask, Properties.Resources.buttonClicked, Properties.Resources.button, "pictureBoxPlay", 137, 49);
            pictureBoxLeader = new CustomButton(Properties.Resources.buttonMask, Properties.Resources.buttonClicked, Properties.Resources.button, "pictureBoxLeader", 137, 49);
            pictureBoxExit = new CustomButton(Properties.Resources.buttonMask, Properties.Resources.buttonClicked, Properties.Resources.button, "pictureBoxExit", 137, 49);
            pictureBoxPlay.Paint += menuButton_Paint;
            pictureBoxPlay.MouseHover += pictureBoxPlay_MouseHover;
            pictureBoxPlay.MouseLeave += pictureBoxPlay_MouseLeave;
            pictureBoxPlay.MouseMove += pictureBoxPlay_MouseMove;
            pictureBoxPlay.MouseClick += pictureBoxPlay_MouseClick;

            pictureBoxLeader.Paint += menuButton_Paint;
            pictureBoxLeader.MouseHover += pictureBoxLeader_MouseHover;
            pictureBoxLeader.MouseLeave += pictureBoxLeader_MouseLeave;
            pictureBoxLeader.MouseMove += pictureBoxLeader_MouseMove;
            pictureBoxLeader.MouseClick += pictureBoxLeader_MouseClick;

            pictureBoxExit.Paint += menuButton_Paint;
            pictureBoxExit.MouseHover += pictureBoxExit_MouseHover;
            pictureBoxExit.MouseLeave += pictureBoxExit_MouseLeave;
            pictureBoxExit.MouseMove += pictureBoxExit_MouseMove;
            pictureBoxExit.MouseClick += pictureBoxExit_MouseClick;

            pictureBoxBg.Controls.Add(pictureBoxPlay);
            pictureBoxBg.Controls.Add(pictureBoxLeader);
            pictureBoxBg.Controls.Add(pictureBoxExit);

            pictureBoxBg.BackgroundImage = Properties.Resources.menu;
        }

        private void menuButton_Paint(object sender, PaintEventArgs e)
        {
            using (Font font = new Font("Comic Sans MS", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                string text = "";
                double height = 0;
                switch (((CustomButton)sender).Name.ToString())
                {
                    case "pictureBoxPlay":
                        text = "Play";
                        height = 6.0;
                        break;
                    case "pictureBoxLeader":
                        text = "Leaderboard";
                        height = 2.0;
                        break;
                    case "pictureBoxExit":
                        text = "Exit";
                        height = 1.2;
                        break;
                }
                Rectangle rect = new Rectangle(0, 0, 137, 49);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                ((CustomButton)sender).CenterPictureBoxHorizontal(((CustomButton)sender));
                ((CustomButton)sender).CenterPictureBoxVertical(((CustomButton)sender), height);
                e.Graphics.DrawString(text, font, Brushes.White, rect, stringFormat);

            }
        }

        private void pictureBoxPlay_MouseClick(object sender, MouseEventArgs e)
        {
            if (((CustomButton)sender).MouseIsOverButton(e.Location))
            {
                pictureBoxPlay.Visible = false;
                pictureBoxLeader.Visible = false;
                pictureBoxExit.Visible = false;
                engine = new Engine(pictureBoxBg);
            }
        }

        private void pictureBoxPlay_MouseHover(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).buttonClicked;
        }

        private void pictureBoxPlay_MouseLeave(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).button;
        }

        private void pictureBoxPlay_MouseMove(object sender, MouseEventArgs e)
        {
            ((CustomButton)sender).hoverEffect(e);
        }

        private void pictureBoxLeader_MouseClick(object sender, MouseEventArgs e)
        {
            if (((CustomButton)sender).MouseIsOverButton(e.Location))
            {
                if(!leaderBoardMenuInitialized)
                    initializeLeaderBoardMenu();
                changeVisibilityToLeaderBoardMenu(true);
                changeVisibilityToMainMenu(false);
                showLeaderBoard();
            }
        }

        private void initializeLeaderBoardMenu()
        {
            backToMenuButton = new CustomButton(Properties.Resources.backArrowMask, Properties.Resources.backArrowClicked, Properties.Resources.backArrow, "backButton", 85, 100);
            backToMenuButton.SizeMode = PictureBoxSizeMode.StretchImage;
            backToMenuButton.Top = pictureBoxBg.Height - 20 - backToMenuButton.Height;
            backToMenuButton.Left = 20;
            pictureBoxBg.Controls.Add(backToMenuButton);
            backToMenuButton.MouseHover += backToMenuButton_MouseHover;
            backToMenuButton.MouseLeave += backToMenuButton_MouseLeave;
            backToMenuButton.MouseMove += backToMenuButton_MouseMove;
            backToMenuButton.MouseClick += backToMenuButton_MouseClick;
            leaderBoardMenuInitialized = true;
        }

        private void showLeaderBoard()
        {
            DatabaseConnection dbConn = new DatabaseConnection("datasource=localhost;port=3306;username=root;password=;database=spaceshooter");
            List<string> tableData = dbConn.GetData();
            if (tableData != null && tableData.Count != 0)
            {
                tableLeaderBoard = new TableLayoutPanel();
                tableLeaderBoard.CellPaint += tableLeaderBoard_CellPaint;
                tableLeaderBoard.RowCount = tableData.Count / 4;
                tableLeaderBoard.ColumnCount = 4;
                tableLeaderBoard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                tableLeaderBoard.AutoScroll = true;
                tableLeaderBoard.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);
                tableLeaderBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                for (int i = 0, sum = 0; i < tableLeaderBoard.RowCount; i++)
                {
                    tableLeaderBoard.ColumnStyles.Add(new ColumnStyle() { Width = 25F, SizeType = SizeType.Percent });
                    for (int j = 0; j < tableLeaderBoard.ColumnCount; j++)
                    {
                        tableLeaderBoard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tableLeaderBoard.Controls.Add(new Label() { Text = tableData[sum++], AutoSize = true, BackColor = Color.Transparent }, j, i);
                    }
                }
                tableLeaderBoard.MaximumSize = new Size(Convert.ToInt32(pictureBoxBg.Width * 0.8), pictureBoxBg.Height - pictureBoxPlay.Location.Y);
                tableLeaderBoard.Width = Convert.ToInt32(pictureBoxBg.Width * 0.8);
                tableLeaderBoard.Left = (pictureBoxBg.Width / 2) - (tableLeaderBoard.Width / 2);
                tableLeaderBoard.Height = pictureBoxBg.Height;
                pictureBoxBg.Controls.Add(tableLeaderBoard);
                
            }
        }

        private void backToMenuButton_MouseHover(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).buttonClicked;
        }

        private void backToMenuButton_MouseLeave(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).button;
        }

        private void backToMenuButton_MouseMove(object sender, MouseEventArgs e)
        {
            ((CustomButton)sender).hoverEffect(e);
        }

        private void backToMenuButton_MouseClick(object sender, MouseEventArgs e)
        {
            //if (((CustomButton)sender).MouseIsOverButton(e.Location))
            //{
                tableLeaderBoard.Dispose();
                changeVisibilityToMainMenu(true);
                changeVisibilityToLeaderBoardMenu(false);
            //}
        }

        private void changeVisibilityToMainMenu(bool visible)
        {
            pictureBoxPlay.Visible = visible;
            pictureBoxLeader.Visible = visible;
            pictureBoxExit.Visible = visible;
        }

        private void changeVisibilityToLeaderBoardMenu(bool visible)
        {
            backToMenuButton.Visible = visible;
        }

        private void tableLeaderBoard_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(48, 112, 176)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
            else
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(194, 163, 39)))
                    e.Graphics.FillRectangle(brush, e.CellBounds);

            var rectangle = e.CellBounds;
            rectangle.Inflate(-1, -1);
            ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Purple, ButtonBorderStyle.Solid);
        }

        private void pictureBoxLeader_MouseHover(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).buttonClicked;
        }

        private void pictureBoxLeader_MouseLeave(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).button;
        }

        private void pictureBoxLeader_MouseMove(object sender, MouseEventArgs e)
        {
            ((CustomButton)sender).hoverEffect(e);
        }

        private void pictureBoxExit_MouseClick(object sender, MouseEventArgs e)
        {
            if (((CustomButton)sender).MouseIsOverButton(e.Location))
            {
                Application.Exit();
            }
        }

        private void pictureBoxExit_MouseHover(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).buttonClicked;
        }

        private void pictureBoxExit_MouseLeave(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).button;
        }

        private void pictureBoxExit_MouseMove(object sender, MouseEventArgs e)
        {
            ((CustomButton)sender).hoverEffect(e);
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (engine != null)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        engine.downKey = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        engine.upKey = true;
                    }
                }
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                {
                    if (e.KeyCode == Keys.Right)
                    {
                        engine.rightKey = true;
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        engine.leftKey = true;
                    }
                }
                if (e.KeyCode == Keys.Escape)
                {
                    engine.escKey = true;
                }
            }

        }
        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (engine != null)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        engine.downKey = false;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        engine.upKey = false;
                    }
                }
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                {
                    if (e.KeyCode == Keys.Right)
                    {
                        engine.rightKey = false;
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        engine.leftKey = false;
                    }
                }
            }

        }


    }
}
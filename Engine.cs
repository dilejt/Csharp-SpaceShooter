using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShip
{
    public class Engine
    {
        private bool _isGameFinished;
        public bool IsGameFinished
        {
            get => _isGameFinished;
            set
            {
                _isGameFinished = value;
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (value >= 0)
                    _score = value;
            }
        }

        private int _speed;
        public int Speed
        {
            get => _speed;
            set
            {
                if (value >= 0)
                    _speed = value;
            }
        }

        private int _shotingspeed;
        public int ShotingSpeed
        {
            get => _shotingspeed;
            set
            {
                if (value >= 0)
                    _shotingspeed = value;
            }
        }

        private int _time;
        public int Time
        {
            get => _time;
            set
            {
                _time = value;
            }
        }

        private int _playerX;
        public int PlayerX
        {
            get => _playerX;
            set
            {
                if (value >= 0 && value <= pictureBoxBg.Width - playerModel.Width)
                    _playerX = value;
            }
        }

        private int _playerY;
        public int PlayerY
        {
            get => _playerY;
            set
            {
                if (value >= 0 && value <= pictureBoxBg.Height - playerModel.Height - pictureBoxBottomGUI.Height)
                    _playerY = value;
            }
        }


        private int _enemySmallCount;
        public int EnemySmallCount
        {
            get => _enemySmallCount;
            set
            {
                if (value >= 0 && value <= EnemySmallMaxCount)
                    _enemySmallCount = value;
            }
        }

        private int _enemySmallMaxCount;
        public int EnemySmallMaxCount
        {
            get => _enemySmallMaxCount;
            set
            {
                if (value >= 0)
                    _enemySmallMaxCount = value;
            }
        }

        private int _enemyBigCount;
        public int EnemyBigCount
        {
            get => _enemyBigCount;
            set
            {
                if (value >= 0 && value <= EnemyBigMaxCount)
                    _enemyBigCount = value;
            }
        }

        private int _enemyBigMaxCount;
        public int EnemyBigMaxCount
        {
            get => _enemyBigMaxCount;
            set
            {
                if (value >= 0)
                    _enemyBigMaxCount = value;
            }
        }

        private int _enemySmallSpeed;
        public int EnemySmallSpeed
        {
            get => _enemySmallSpeed;
            set
            {
                if (value >= 0)
                    _enemySmallSpeed = value;
            }
        }

        private int _enemyBigSpeed;
        public int EnemyBigSpeed
        {
            get => _enemyBigSpeed;
            set
            {
                if (value >= 0)
                    _enemyBigSpeed = value;
            }
        }

        private int _hpPlayer;
        public int HpPlayer
        {
            get => _hpPlayer;
            set
            {
                if (value > 20)
                    _hpPlayer = 20;
                else
                    _hpPlayer = value;

            }
        }


        int oldTimeValueScore;
        public bool upKey = false;
        public bool downKey = false;
        public bool leftKey = false;
        public bool rightKey = false;
        public bool escKey = false;
        private bool isMenuShowed = false;
        private bool isShooted = false;
        public bool isEnemySmall = false;
        public bool isEnemyBig = false;

        public PictureBox pictureBoxBg;
        public PictureBox playerModel;
        public PictureBox pictureBoxBottomGUI;
        public PictureBox pausePicture;
        public PictureBox playerBullet;
        public PictureBox hpBar;
        public PictureBox explosion;
        public CustomButton buttonReplay;
        public TextBox inputName;
        public Label labelScore;
        public Label labelTime;
        public Timer animationTimer;
        public Timer loopTimer;
        public Timer pauseTimer;
        public Stopwatch stopwatch;
        public HpAnimation animate;
        public ExplosionAnimation explosionAnimation;
        public Bitmap[] explosionPictures;

        public Engine(PictureBox pictureBoxBg)
        {
            this.pictureBoxBg = pictureBoxBg;
            pictureBoxBg.Image = Properties.Resources.background;
            pictureBoxBg.BackgroundImage = null;
            pictureBoxBg.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBoxBottomGUI = new PictureBox();
            pictureBoxBottomGUI.BackColor = Color.Transparent;
            pictureBoxBottomGUI.Dock = DockStyle.Bottom;
            pictureBoxBottomGUI.Height = 74;
            pictureBoxBottomGUI.BackgroundImage = Properties.Resources.gui;
            pictureBoxBottomGUI.BackgroundImageLayout = ImageLayout.Center;
            pictureBoxBg.Controls.Add(pictureBoxBottomGUI);

            labelScore = new Label();
            labelScore.BackColor = Color.Transparent;
            labelScore.ForeColor = Color.AntiqueWhite;
            labelScore.AutoSize = true;
            labelScore.Font = new Font(labelScore.Font.FontFamily, 16, FontStyle.Regular);
            /*
            PrivateFontCollection customFont = new PrivateFontCollection();
            customFont.AddFontFile(@"DS-DIGIB.ttf");
            labelScore.Font = new Font(customFont.Families[0], 16, FontStyle.Regular);
            */
            pictureBoxBg.Controls.Add(labelScore);

            labelTime = new Label();
            labelTime.BackColor = Color.Transparent;
            labelTime.ForeColor = Color.AntiqueWhite;
            labelTime.Top = 0;
            labelTime.AutoSize = true;
            labelTime.Left = pictureBoxBg.Width - labelTime.Width;
            labelTime.Font = new Font(labelTime.Font.FontFamily, 16, FontStyle.Regular);
            /*
            customFont.AddFontFile(@"DS-DIGIB.ttf");
            labelTime.Font = new Font(customFont.Families[0], 16, FontStyle.Regular);
            */
            pictureBoxBg.Controls.Add(labelTime);

            hpBar = new PictureBox();
            hpBar.Image = Properties.Resources.hp20;
            hpBar.SizeMode = PictureBoxSizeMode.AutoSize;
            hpBar.BackColor = Color.Transparent;
            hpBar.Padding = new Padding(22);
            hpBar.Top = hpBar.Height / 2 - 22;
            pictureBoxBottomGUI.Controls.Add(hpBar);

            StartGame();
        }

        private void StartGame()
        {
            Time = 0;
            oldTimeValueScore = 0;
            HpPlayer = 20;
            IsGameFinished = false;
            Score = 0;
            Speed = 2;
            EnemySmallSpeed = 2;
            EnemyBigSpeed = 1;
            ShotingSpeed = 10;
            EnemySmallMaxCount = 4;
            EnemyBigMaxCount = 1;
            playerModel = new PictureBox();
            playerModel.BackgroundImage = Properties.Resources.player;
            pictureBoxBg.Controls.Add(playerModel);
            playerModel.BackColor = Color.Transparent;
            playerModel.Width = 70;
            playerModel.Height = 70;
            playerModel.BackgroundImageLayout = ImageLayout.Stretch;
            PlayerX = pictureBoxBg.Width / 2;
            PlayerY = pictureBoxBg.Height / 2;
            playerModel.Location = new Point(PlayerX, PlayerY);

            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += new EventHandler(animationTimer_Tick);
            animationTimer.Start();

            loopTimer = new Timer();
            loopTimer.Interval = 1;
            loopTimer.Tick += new EventHandler(loopTimer_Tick);
            loopTimer.Start();

            pauseTimer = new Timer();
            pauseTimer.Interval = 1;
            pauseTimer.Tick += new EventHandler(pauseTimer_Tick);
            pauseTimer.Start();

            stopwatch = new Stopwatch();
            stopwatch.Start();

            Bitmap bitmapHP = new Bitmap(144, 39);
            Bitmap[] hpPictures = new Bitmap[] { Properties.Resources.hp0, Properties.Resources.hp1,
                Properties.Resources.hp2, Properties.Resources.hp3, Properties.Resources.hp4,
            Properties.Resources.hp5,Properties.Resources.hp6,Properties.Resources.hp7,Properties.Resources.hp8,
            Properties.Resources.hp9,Properties.Resources.hp10,Properties.Resources.hp11,Properties.Resources.hp12
            ,Properties.Resources.hp13,Properties.Resources.hp14,Properties.Resources.hp15,Properties.Resources.hp16
            ,Properties.Resources.hp17,Properties.Resources.hp18,Properties.Resources.hp19,Properties.Resources.hp20};
            animate = new HpAnimation(hpPictures);
            
            Bitmap bitmapExplosion = new Bitmap(70, 70);
            explosionPictures = new Bitmap[] { Properties.Resources.explosion1,
                Properties.Resources.explosion2, Properties.Resources.explosion3, Properties.Resources.explosion4,
            Properties.Resources.explosion5,Properties.Resources.explosion6,Properties.Resources.explosion7,Properties.Resources.explosion8,
            Properties.Resources.explosion9,Properties.Resources.explosion10,Properties.Resources.explosion11,Properties.Resources.explosion12
            ,Properties.Resources.explosion13,Properties.Resources.explosion14,Properties.Resources.explosion15,Properties.Resources.explosion16
            ,Properties.Resources.explosion17,Properties.Resources.explosion18,Properties.Resources.explosion19,Properties.Resources.explosion20
            ,Properties.Resources.explosion21,Properties.Resources.explosion22,Properties.Resources.explosion23,Properties.Resources.explosion24
            ,Properties.Resources.explosion25,Properties.Resources.explosion26};
        }

        private void pauseTimer_Tick(object sender, EventArgs e)
        {
            if (!IsGameFinished)
            {
                if (escKey)
                {
                    escKey = false;
                    if (isMenuShowed)
                    {
                        if (pausePicture != null)
                        {
                            loopTimer.Start();
                            stopwatch.Start();
                            animationTimer.Start();
                            pausePicture.Dispose();
                            isMenuShowed = false;
                        }
                    }
                    else
                    {
                        loopTimer.Stop();
                        stopwatch.Stop();
                        animationTimer.Stop();
                        showPauseMenu();
                    }
                }
                shootBeam();

            }
        }
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            hpBar.Image = animate.GiveNextImage(HpPlayer);
        }
        private void showPauseMenu()
        {
            pausePicture = new PictureBox();
            pausePicture.Location = new Point(-pictureBoxBg.Width / 4, 0);
            pausePicture.BackgroundImage = Properties.Resources.pause;
            pausePicture.BackgroundImageLayout = ImageLayout.Stretch;
            pausePicture.Size = new Size(pictureBoxBg.Width + pictureBoxBg.Width / 2, pictureBoxBg.Height);
            pictureBoxBg.Controls.Add(pausePicture);
            pausePicture.BringToFront();
            isMenuShowed = true;
        }
        private void shootBeam()
        {
            if (Time / 100 % ShotingSpeed == 0)
            {
                if (isShooted == false)
                    makeBullet();
            }
            else
                isShooted = false;
        }
        private void makeBullet()
        {
            isShooted = true;
            PictureBox bullet = new PictureBox();
            bullet.BackgroundImage = Properties.Resources.beam;
            bullet.Size = new Size(5, 30);
            bullet.BackColor = Color.Transparent;
            bullet.Tag = "bullet";
            bullet.Name = "bullet";
            bullet.BackgroundImageLayout = ImageLayout.Stretch;
            bullet.Top = PlayerY - bullet.Height;
            bullet.Left = PlayerX + playerModel.Width / 2 - bullet.Width / 2;
            pictureBoxBg.Controls.Add(bullet);
        }
        private void loopTimer_Tick(object sender, EventArgs e)
        {
            if (!IsGameFinished)
            {
                Time = Convert.ToInt32(stopwatch.ElapsedMilliseconds);

                if ((Time / 100) == oldTimeValueScore + 1)
                    Score++;
                oldTimeValueScore = Time / 100;

                labelScore.Text = "SCORE:" + Score.ToString();
                labelTime.Text = "TIME:"+(Time/1000).ToString();
                labelTime.Left = pictureBoxBg.Width - labelTime.Width;

                playerMovement();

                _ = new CreateSmallEnemy(pictureBoxBg, this);
                _ = new CreateBigEnemy(pictureBoxBg, this);

                if (HpPlayer <= 0)
                {
                    makeExplosion(playerModel.Location.X, playerModel.Location.Y);
                    playerModel.Dispose();
                    gameOver();
                }
            }
            //sprites iteractions
            foreach (Control control in pictureBoxBg.Controls)
            {
                if (control.Tag != null)
                {
                    if (control.Tag.ToString() == "bullet")
                    {
                        spritesMovement(control, -20);
                        if (((PictureBox)control).Top < 0 - ((PictureBox)control).Height - 20)
                        {
                            destroySprite(control);
                        }
                    }
                    if (control.Tag.ToString() == "enemySmall")
                    {
                        spritesMovement(control, EnemySmallSpeed);
                        EnemySmallCount = checkSpritesInteraction(control, EnemySmallCount, 2, 4, 5);
                    }
                    if (control.Tag.ToString() == "enemyBig")
                    {
                        spritesMovement(control, EnemyBigSpeed);
                        EnemyBigCount = checkSpritesInteraction(control, EnemyBigCount, 4, 8, 15);
                    }
                }
            }
        }
        private void spritesMovement(Control control, int speed)
        {
            control.Top += speed;
        }
        private int checkSpritesInteraction(Control sprite, int spritesCount, int damageCollsion, int damageEscape, int scoreValue)
        {
            if (((PictureBox)sprite).Top > pictureBoxBg.Height)
            {
                destroySprite(sprite);
                spritesCount--;
                HpPlayer -= damageEscape;
            }
            if (sprite.Bounds.IntersectsWith(playerModel.Bounds))
            {
                destroySprite(sprite);
                spritesCount--;
                HpPlayer -= damageCollsion;
                makeExplosion(sprite.Location.X, sprite.Location.Y);
            }
            foreach (Control bullet in pictureBoxBg.Parent.Controls.Find("bullet", true))
            {
                if (sprite.Bounds.IntersectsWith(bullet.Bounds))
                {
                    destroySprite(sprite);
                    destroySprite(bullet);
                    Score += scoreValue;
                    spritesCount--;
                    makeExplosion(sprite.Location.X, sprite.Location.Y);
                }
            }
            return spritesCount;
        }
        private void makeExplosion(int x, int y)
        {
            explosion = new PictureBox();
            explosion.Image = Properties.Resources.explosion1;
            explosion.SizeMode = PictureBoxSizeMode.StretchImage;
            explosion.BackColor = Color.Transparent;
            explosion.Size = new Size(70, 70);
            explosion.Location = new Point(x, y);
            pictureBoxBg.Controls.Add(explosion);
            explosionAnimation = new ExplosionAnimation(explosionPictures);
            _ = explosionAnimation.GiveNextImageAsync(explosion);
        }
        private void destroySprite(Control control)
        {
            control.Dispose();
            pictureBoxBg.Parent.Controls.Remove(control);
        }
        public void playerMovement()
        {
            if (downKey)
            {
                PlayerY += Speed;
            }
            if (upKey)
            {
                PlayerY -= Speed;
            }
            if (rightKey)
            {
                PlayerX += Speed;
            }
            if (leftKey)
            {
                PlayerX -= Speed;
            }
            playerModel.Location = new Point(PlayerX, PlayerY);
        }
        private void gameOver()
        {
            IsGameFinished = true;
            stopwatch.Stop();
            PictureBox gameOverPicture = new PictureBox();
            gameOverPicture.BackColor = Color.Transparent;
            gameOverPicture.Image = Properties.Resources.endGame;
            gameOverPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            gameOverPicture.Parent = pictureBoxBg;
            gameOverPicture.Left = (gameOverPicture.Parent.ClientSize.Width / 2) - (gameOverPicture.Width / 2);
            gameOverPicture.Top = Convert.ToInt32((gameOverPicture.Parent.ClientSize.Height / 2) - (gameOverPicture.Image.Height / 2));
            pictureBoxBg.Controls.Add(gameOverPicture);
            gameOverPicture.BringToFront();

            buttonReplay = new CustomButton(Properties.Resources.buttonReplayMask, Properties.Resources.buttonReplayClicked, Properties.Resources.buttonReplay, "buttonReplay", 60, 60)
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Top = gameOverPicture.Top + gameOverPicture.Height + 20,
                Parent = pictureBoxBg
            };
            buttonReplay.Left = gameOverPicture.Left + gameOverPicture.Width / 2 - buttonReplay.Width / 2;
            buttonReplay.MouseHover += buttonReplay_MouseHover;
            buttonReplay.MouseLeave += buttonReplay_MouseLeave;
            buttonReplay.MouseMove += buttonReplay_MouseMove;
            buttonReplay.MouseClick += buttonReplay_MouseClick;
            pictureBoxBg.Controls.Add(buttonReplay);
            buttonReplay.BringToFront();

            inputName = new TextBox()
            {
                Width = gameOverPicture.Width,
                Left = (gameOverPicture.Parent.ClientSize.Width / 2) - (gameOverPicture.Width / 2),
                Top = gameOverPicture.Parent.ClientSize.Height / 4,
                Font = new Font("Comic Sans MS", 12),
                AutoSize = true,
                MaxLength = 20
                
            };
            pictureBoxBg.Controls.Add(inputName);
            inputName.BringToFront();

            PictureBox nameText = new PictureBox();
            nameText.BackColor = Color.Transparent;
            nameText.Image = Properties.Resources.name;
            nameText.SizeMode = PictureBoxSizeMode.StretchImage;
            nameText.Parent = pictureBoxBg;
            nameText.Height = 200;
            nameText.Left = (nameText.Parent.ClientSize.Width / 2) - (nameText.Width / 2);
            nameText.Top = gameOverPicture.Parent.ClientSize.Height / 4 - nameText.Height;
            pictureBoxBg.Controls.Add(nameText);
            nameText.BringToFront();
        }
        private void buttonReplay_MouseClick(object sender, MouseEventArgs e)
        {
            DatabaseConnection dbConn = new DatabaseConnection("datasource=localhost;port=3306;username=root;password=;database=spaceshooter");
            dbConn.InsertData(Time, Score, inputName.Text);
            Application.Restart();
        }
        private void buttonReplay_MouseMove(object sender, MouseEventArgs e)
        {
            ((CustomButton)sender).hoverEffect(e);
        }
        private void buttonReplay_MouseLeave(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).button;
        }
        private void buttonReplay_MouseHover(object sender, EventArgs e)
        {
            ((CustomButton)sender).Image = ((CustomButton)sender).buttonClicked;
        }
    }
}

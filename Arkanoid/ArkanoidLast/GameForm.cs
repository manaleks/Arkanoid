using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ArkanoidLast
{
    public partial class GameForm : Form
    {
        // Уровни
        List<char[,]> levels;
        int currentLevel = 0;

        // Статистика
        int livesRacket1, livesRacket2;
        int scoreRacket1, scoreRacket2;
        int leader = 2; // ведущий - тот, кто последний отбил мяч  (1 - racket1, 2 - racket2)
        string winner = "";

        // Основные элементы (ракетки, мяч, блоки)
        Racket racketBOTTOM, racketTOP;
        Ball ball;
        List<Block> blocks; // список активных (неуничтоженных) блоков
         
        ArkaSocket arkaSocket = new ArkaSocket(); // точка соединения с другим приложением.

        int LastCursor;     // Для определения потери сети в режиме сервера

        /// <summary>
        /// Конструктор для игры с компьютером
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            createLevel(); // загружаем схему уровня
        }

        /// <summary>
        /// Конструктор для игры по сети
        /// </summary>
        /// <param name="isItServ">Является ли компьютер сервером</param>
        public GameForm(bool isItServ)
        {
            ThreadRun(isItServ);
            InitializeComponent();
            createLevel(); // загружаем схему уровня
        }

        void ThreadRun(bool isItServ)
        {
            arkaSocket = new ArkaSocket(isItServ);  // создание сокета: сервера или клиента
            if (isItServ)
                new Thread(arkaSocket.ServerRun).Start();   // запуск потока сервера
            else
                new Thread(arkaSocket.ClientRun).Start();  // запуск потока клиента
        }

        private void createLevel()
        {
            /* Схема уровня
             * '1' = бежевый блок
             * '2' = красный блок
             * '3' = синий блок
             * '4' = оранжевый блок
             * 'B' = бонусный блок +100
             * 'S' = бонусный блок, увеличивающий размер ракетки
            */
            levels = new List<char[,]>() {
                new char[17, 17] {
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ','S','1','B','1','S',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},

                },
                new char[17, 17] {
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ','S',' ',' ',' ',' ',' ','S',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ','3',' ',' ',' ',' ',' ','3',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ','3',' ',' ',' ','3',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ','3',' ',' ',' ','3',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ','B','2','2','B','2','2','B',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ','2','1','1','1','1','1','2',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ','2','1','3','1','1','1','3','1','2',' ',' ',' ',' '},
                    {' ',' ',' ',' ','B','1','S','1','1','1','S','1','B',' ',' ',' ',' '},
                    {' ',' ',' ','2','4','1','1','1','1','1','1','1','4','2',' ',' ',' '},
                    {' ',' ',' ','2','4','1','1','1','1','1','1','1','4','2',' ',' ',' '},
                    {' ',' ',' ','2',' ','4','1','1','1','1','1','4',' ','2',' ',' ',' '},
                    {' ',' ',' ','2',' ','4','4','4','4','4','4','4',' ','2',' ',' ',' '},
                    {' ',' ',' ','2',' ','4',' ',' ',' ',' ',' ','4',' ','2',' ',' ',' '},
                    {' ',' ',' ','2',' ','4',' ',' ',' ',' ',' ','4',' ','2',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ','S','3',' ','3','S',' ',' ',' ',' ',' ',' '},
                    {' ',' ',' ',' ',' ',' ','3','3',' ','3','3',' ',' ',' ',' ',' ',' '},
                },
new char[10, 10] {
{' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
{' ',' ',' ',' ',' ','S',' ',' ',' ',' '},
{' ',' ',' ',' ',' ','3',' ',' ',' ',' '},
{' ',' ',' ',' ',' ',' ','3',' ',' ',' '},
{' ',' ',' ',' ',' ',' ','3',' ',' ',' '},
{' ',' ',' ',' ',' ','B','2','2','B','2'},
{' ',' ',' ',' ',' ','2','1','1','1','1'},
{' ',' ',' ',' ','2','1','3','1','1','1'},
{' ',' ',' ',' ','B','1','S','1','1','1'},
{' ',' ',' ','2','4','1','1','1','1','1'},
},
            };
        }

        private void startGame()
        {
            currentLevel = arkaSocket.currentLevel;
            WinLabel.Hide();
            Restart.Hide();
            Cursor.Hide();

            livesRacket1 = 3;
            livesRacket2 = 3;
            scoreRacket1 = 0;
            scoreRacket2 = 0;

            createBlocks();

            racketBOTTOM = new Racket(this, true);
            racketTOP = new Racket(this, false);

            Controls.Add(racketBOTTOM);
            Controls.Add(racketTOP);
            createNewBall();

            string[] delIndexMas = arkaSocket.blocksToDel.Split('.');
            foreach (string i in delIndexMas)
            {
                if (i != "")
                {
                    Controls.Remove(blocks[Convert.ToInt32(i)]);
                    blocks.RemoveAt(Convert.ToInt32(i));
                }
            }


            movementTimer.Start();
            scoreTimer.Start();
        }

        private void endGame()
        {
            movementTimer.Stop();
            scoreTimer.Stop();
            Cursor.Show();

            if (winner != "") 
                WinLabel.Text = "WINNER: " + winner; // победитель тот, у кого не закончились жизни
            else
            {
                if (scoreRacket1 > scoreRacket2) // или кто набрал больше очков к концу игры (когда все блоки уничтожены)
                WinLabel.Text = "WINNER: " + "Player1";
                else if (scoreRacket1 < scoreRacket2)
                    WinLabel.Text = "WINNER: " + "Player2";
                else WinLabel.Text = "WINNER: " + "Both";
            }
            WinLabel.Show();
            Restart.Show();
        }

        private void createNewBall() // создаем новый мяч и устанавливаем в середину ракетки
        {
            ball = new Ball();

            switch (leader)
            {
                case 1:
                    {
                        ball.Location = new Point(
                            racketBOTTOM.Location.X + racketBOTTOM.Size.Width / 2 - ball.Size.Width / 2,
                            racketBOTTOM.Location.Y - ball.Size.Height);
                        break;
                    }
                case 2:
                    {
                        ball.Location = new Point(
                            racketTOP.Location.X + racketTOP.Size.Width / 2 - ball.Size.Width / 2,
                            racketTOP.Location.Y + ball.Size.Height);
                        break;
                    }
            }

            Controls.Add(ball);
        }

        private void createBlocks() // загружаем блоки на форму согласно схеме уровня
        {
            blocks = new List<Block>();
            for (int i = 0; i < levels[currentLevel].GetLength(0); i++)
            {
                for (int j = 0; j < levels[currentLevel].GetLength(1); j++)
                {
                    char blockType = levels[currentLevel][i, j]; // получаем тип блока
                    if (blockType != ' ')
                    {
                        // Создаем объект блока и добавляем в список активных блоков (т.е. неуничтоженных)
                        Block block = new Block(blockType);
                        block.Location = new Point(j + j * block.Size.Width + (this.Width/2 - 408), i + i * block.Size.Height + this.Height/2 - 200);
                        blocks.Add(block);
                        Controls.Add(block);
                    }
                }
            }
    
        }

        private void movementTimer_Tick(object sender, EventArgs e)
        {   
            // Если работает форма сервера
            if (arkaSocket.isItServ)
            {   
                // если идёт сетевая игра
                if (arkaSocket.network)
                {
                    // Проверяем активность клиента
                    if (arkaSocket.clientCursor == LastCursor)
                        arkaSocket.standCount++;
                    else
                        arkaSocket.standCount = 0;

                    if(arkaSocket.standCount > 50)
                    {
                        arkaSocket.standCount = 0;
                        arkaSocket.network = false;
                    }

                    // Управляем движением ракеток
                    // ракетка клиента:
                    // когда курсор внутри поля
                    if (arkaSocket.clientCursor > leftSideBar.Right && arkaSocket.clientCursor < rightSideBar.Left)
                    {
                        arkaSocket.posBOTTOM = arkaSocket.clientCursor - (racketBOTTOM.Width / 2);
                        racketBOTTOM.Left = arkaSocket.posBOTTOM;
                    }
                    // когда курсор справа от поля
                    if (arkaSocket.clientCursor > rightSideBar.Left)
                    {
                        arkaSocket.posBOTTOM = rightSideBar.Left - racketBOTTOM.Width / 2;
                        racketBOTTOM.Left = arkaSocket.posBOTTOM;
                    }
                    //когда курсор слева от поля
                    if (arkaSocket.clientCursor < leftSideBar.Right)
                    {
                        arkaSocket.posBOTTOM = leftSideBar.Right - racketBOTTOM.Width / 2;
                        racketBOTTOM.Left = arkaSocket.posBOTTOM;
                    }
                }
                else
                    racketBOTTOM.Left = ball.Left + (ball.Width / 2) - (racketBOTTOM.Width / 2);

                // Ракетка сервера:
                // когда курсор внутри поля
                if (Cursor.Position.X > leftSideBar.Right && Cursor.Position.X < rightSideBar.Left)
                {
                    arkaSocket.posTOP = Cursor.Position.X - (racketTOP.Width / 2);
                    racketTOP.Left = arkaSocket.posTOP;
                }
                // когда курсор справа от поля
                if (Cursor.Position.X > rightSideBar.Left)
                {
                    arkaSocket.posTOP = rightSideBar.Left - racketTOP.Width / 2;
                    racketTOP.Left = arkaSocket.posTOP; 
                }
                //когда курсор слева от поля
                if (Cursor.Position.X < leftSideBar.Right)
                {
                    arkaSocket.posTOP = leftSideBar.Right - racketTOP.Width / 2;
                    racketTOP.Left = arkaSocket.posTOP; 
                }


                // движение мяча, если он запущен
                if (ball.launched)
                    ball.move();
                else // иначе, мяч находится на ракетке ведущего
                    switch (leader)
                    {
                        case 1:
                            {
                                ball.Location = new Point(racketBOTTOM.Location.X + racketBOTTOM.Size.Width / 2 - ball.Size.Width / 2, racketBOTTOM.Location.Y - ball.Size.Height);
                                break;
                            }
                        case 2:
                            {
                                ball.Location = new Point(racketTOP.Location.X + racketTOP.Size.Width / 2 - ball.Size.Width / 2, racketTOP.Location.Y + ball.Size.Height + 1);
                                break;
                            }
                    }

                // Столкновение с блоком
                for (int i = 0; i < blocks.Count; i++)
                {
                    bool blockDestroyed = false;

                    // Проверяем, был ли блок уничтожен мячом
                    if (ball.collision(blocks[i])) blockDestroyed = true;

                    if (blockDestroyed)
                    {
                        switch (blocks[i].type)
                        {
                            // уничтожен бонусный блок
                            case 'B':
                                {
                                    if (leader == 1) scoreRacket1 += 50;
                                    else scoreRacket2 += 50;
                                    break;
                                }
                            case 'S':
                                {
                                    if (leader == 1) racketBOTTOM.upgradeSize();
                                    else racketTOP.upgradeSize();
                                }
                                break;
                        }
                        Controls.Remove(blocks[i]);
                        blocks.RemoveAt(i);
                        arkaSocket.blocksToDel += i + ".";

                        // засчитываем баллы ведущему
                        if (leader == 1) scoreRacket1 += 50;
                        else scoreRacket2 += 50;

                        if (blocks.Count == 0)
                        {
                            // Все блоки уничтожены => конец игры
                            endGame();
                        }
                    }
                }

                // Столкновение с ракеткой и определение ведущего
                if (ball.collision(racketBOTTOM)) leader = 1;
                if (ball.collision(racketTOP)) leader = 2;

                // Столкновение с левым или правым краем игрового поля => отражение мяча по горизонтали
                if (ball.Location.X <= leftSideBar.Right || ball.Location.X + ball.Size.Width >= rightSideBar.Left)
                    ball.flipX();

                // Столкновение с верхним краем игрового поля => вычитаем жизнь racket2
                if (ball.Location.Y <= 0)
                {
                    Controls.Remove(ball);
                    livesRacket2--;
                    leader = ((leader == 1) ? 2 : 1);
                    if (!arkaSocket.network)
                        leader = 2;
                    if (livesRacket2 > 0)
                    {
                        createNewBall();
                    }
                    else
                    {
                        // Жизни закончились => конец игры
                        winner = "Player1";
                        endGame();
                    }
                }

                // Столкновение с нижним краем игрового поля => вычитаем жизнь racket1
                if (ball.Location.Y >= this.Height)
                {
                    Controls.Remove(ball);
                    livesRacket1--;
                    leader = ((leader == 1) ? 2 : 1);
                    if (livesRacket1 > 0)
                    {
                        createNewBall();
                    }
                    else
                    {
                        // Жизни закончились => конец игры
                        winner = "Player2";
                        endGame();
                    }
                }

                // Записываем данные для отправки клиенту.
                arkaSocket.ballY = ball.Location.Y;
                arkaSocket.ballX = ball.Location.X;

                arkaSocket.widthBOTTOM = racketBOTTOM.Width;
                arkaSocket.widthTOP = racketTOP.Width;

                arkaSocket.scoreBOTTOM = scoreRacket1;
                    arkaSocket.scoreTOP = scoreRacket2;
                arkaSocket.lifesBOTTOM = livesRacket1;
                  arkaSocket.lifesTOP = livesRacket2;

                // Выводим результаты
                scoreLabel.Text = "SCORE\n\nPLAYER1\n" + scoreRacket1 + "\nPLAYER2\n" + scoreRacket2;
                livesLabel.Text = "LIVES\n\nPLAYER1\n" + livesRacket1 + "\nPLAYER2\n" + livesRacket2;
                if(arkaSocket.network)
                    modeLbl.Text = "PvP";
                else
                    modeLbl.Text = "PvE";

                LastCursor = arkaSocket.clientCursor;
            }
            else
            {
                arkaSocket.clientCursor = Cursor.Position.X;    // Отсылаем курсор пользователя.

                racketBOTTOM.Left = arkaSocket.posBOTTOM; // устанавливаем центр нижней ракетки на курсор клиента

                // Отображаем верхнюю ракетку
                if (arkaSocket.network)
                    racketTOP.Left = arkaSocket.posTOP; // устанавливаем центр ракетки 2 на курсор клиента
                else
                    racketTOP.Left = ball.Left + (ball.Width / 2) - (racketTOP.Width / 2);

                if (arkaSocket.network)
                    ball.Location = new Point(arkaSocket.ballX, arkaSocket.ballY);

                for (int i = 0; i < blocks.Count; i++)
                    if (ball.collision(blocks[i]))
                    {
                        Controls.Remove(blocks[i]);
                        blocks.RemoveAt(i);
                    }

                        racketTOP.Width = arkaSocket.widthTOP;
                racketBOTTOM.Width = arkaSocket.widthBOTTOM;

                scoreLabel.Text = "SCORE\n\nPLAYER1\n" + arkaSocket.scoreBOTTOM + "\nPLAYER2\n" + arkaSocket.scoreTOP;
                livesLabel.Text = "LIVES\n\nPLAYER1\n" + arkaSocket.lifesBOTTOM + "\nPLAYER2\n" + arkaSocket.lifesTOP;
                if (arkaSocket.network)
                    modeLbl.Text = "PvP";
                else
                    modeLbl.Text = "PvE";
            }
        }

        private void scoreTimer_Tick(object sender, EventArgs e)
        {
            // Вычитаем 1 балл в секунду
            if (scoreRacket1 > 0) scoreRacket1 -= 1;
            if (scoreRacket2 > 0) scoreRacket2 -= 1;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (Controls.Contains(ball) && e.KeyCode == Keys.Space) // запуск мяча
                ball.launched = true;

            if (e.KeyCode == Keys.Escape)   // Выход из игры
            {
                arkaSocket.work = false;
                Thread.Sleep(100);
                Environment.Exit(0);
            }
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            arkaSocket.work = false;
            Thread.Sleep(100);
            Environment.Exit(0);
        }

        private void Restart_Click(object sender, EventArgs e) // загрузка новой игры
        {
            WinLabel.Hide();
            Restart.Hide();

            Controls.Remove(ball);
            Controls.Remove(racketBOTTOM);
            Controls.Remove(racketTOP);
            foreach (Block b in blocks) Controls.Remove(b);
            blocks.Clear();
            livesLabel.Show();
            arkaSocket.blocksToDel = "";

            startGame();
        }

        private void startLabel_Click(object sender, EventArgs e) // старт игры
        {
            startLabel.Hide();
            gameName.Hide();
            startPic.Hide();
            ExitLbl.Left = 10;
            ExitLbl.BackColor = Color.FromArgb(64,64,64);

            startGame();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            gameName.Location = new Point(this.Width / 2 - gameName.Width / 2, this.Height / 2 - gameName.Height);
            startLabel.Location = new Point(this.Width / 2 - startLabel.Width / 2, gameName.Bottom + 30);
            ExitLbl.Location = new Point(this.Width / 2 - ExitLbl.Width / 2, startLabel.Bottom + 30);
            rightSideBar.Left = this.Width - 175;
            leftSideBar.Height = this.Height;
            rightSideBar.Height = this.Width;
            scoreLabel.Left = rightSideBar.Left + 5;
            livesLabel.Left = rightSideBar.Left + 5;
            label3.Location = new Point(leftSideBar.Right + 5, this.Height - label3.Height-50);
            
            WinLabel.Location = new Point(this.Width / 2 - WinLabel.Width / 2 - 50, this.Height / 2 - WinLabel.Height);
            Restart.Location = new Point(this.Width / 2 - Restart.Width / 2, WinLabel.Location.Y + WinLabel.Height + 30);
        }
    }
}

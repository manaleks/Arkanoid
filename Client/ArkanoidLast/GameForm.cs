using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArkanoidLast
{
    public partial class GameForm : Form
    {
        // Уровни
        List<char[,]> levels;
        int currentLevel = 1;

        // Статистика
        int livesRacket1, livesRacket2;
        int scoreRacket1, scoreRacket2;
        int leader = 2; // ведущий - тот, кто последний отбил мяч  (1 - racket1, 2 - racket2)
        string winner = "";

        // Основные элементы (ракетки, мяч, блоки)
        Racket racket1, racket2;
        Ball ball;
        List<Block> blocks; // список активных (неуничтоженных) блоков

        public GameForm()
        {
            InitializeComponent();

        //    Cursor.Hide();
       //     this.FormBorderStyle = FormBorderStyle.None;

            createLevel(); // загружаем схему уровня
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
            };
        }

        private void startGame()
        {
            WinLabel.Hide();
            Restart.Hide();
            Cursor.Hide();

            livesRacket1 = 3;
            livesRacket2 = 3;
            scoreRacket1 = 0;
            scoreRacket2 = 0;

            createBlocks();

            racket1 = new Racket(this, true);
            racket2 = new Racket(this, false);

            Controls.Add(racket1);
            Controls.Add(racket2);
            createNewBall();

            movementTimer.Start();
            scoreTimer.Start();
        }

        private void endGame()
        {
            movementTimer.Stop();
            scoreTimer.Stop();
            Restart.Show();
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
                            racket1.Location.X + racket1.Size.Width / 2 - ball.Size.Width / 2,
                            racket1.Location.Y - ball.Size.Height);
                        break;
                    }
                case 2:
                    {
                        ball.Location = new Point(
                            racket2.Location.X + racket2.Size.Width / 2 - ball.Size.Width / 2,
                            racket2.Location.Y + ball.Size.Height);
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
            /*     // устанавливаем границы движения ракетки (только в игровом поле)
                 if (racket1.isMovingLeft && racket1.Location.X > leftSideBar.Right)
                     racket1.moveLeft();
                 if (racket1.isMovingRight && racket1.Location.X + racket1.Size.Width < rightSideBar.Left)
                     racket1.moveRight();

                 if (racket2.isMovingLeft && racket2.Location.X > leftSideBar.Right)
                     racket2.moveLeft();
                 if (racket2.isMovingRight && racket2.Location.X + racket2.Size.Width < rightSideBar.Left)
                     racket2.moveRight();*/

            racket1.Left = Cursor.Position.X - (racket1.Width / 2); // устанавливаем центр ракетки 1 на курсор сервера
            racket2.Left = Program.posDOWN - (racket1.Width / 2); // устанавливаем центр ракетки 2 на курсор клиента

            // движение мяча, если он запущен
            if (ball.launched)
            {
                ball.move();
            }
            else // иначе, мяч находится на ракетке ведущего
            {
                switch (leader)
                {
                    case 1: 
                        {
                            ball.Location = new Point(racket1.Location.X + racket1.Size.Width / 2 - ball.Size.Width / 2, racket1.Location.Y - ball.Size.Height);
                            break;
                        }
                    case 2: 
                        {
                            ball.Location = new Point(racket2.Location.X + racket2.Size.Width / 2 - ball.Size.Width / 2, racket2.Location.Y + ball.Size.Height+1);
                            break;
                        }
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
                                if (leader == 1) racket1.upgradeSize();
                                else racket2.upgradeSize();
                            }
                            break;
                    }
                    Controls.Remove(blocks[i]);
                    blocks.RemoveAt(i);

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
            if(ball.collision(racket1)) leader = 1;
            if (ball.collision(racket2)) leader = 2;

            // Столкновение с левым или правым краем игрового поля => отражение мяча по горизонтали
            if (ball.Location.X <= leftSideBar.Right || ball.Location.X + ball.Size.Width >= rightSideBar.Left)
                ball.flipX();

            // Столкновение с верхним краем игрового поля => вычитаем жизнь racket2
            if (ball.Location.Y <= 0)
            {
                Controls.Remove(ball);
                livesRacket2--;
                leader = ((leader==1) ? 2 : 1);
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

            // Выводим результаты
            scoreLabel.Text = "SCORE\n\nPLAYER1\n" + scoreRacket1 + "\nPLAYER2\n" + scoreRacket2;
            livesLabel.Text = "LIVES\n\nPLAYER1\n" + livesRacket1 + "\nPLAYER2\n" + livesRacket2;
        }

        private void scoreTimer_Tick(object sender, EventArgs e)
        {
            // Вычитаем 1 балл в секунду
            if (scoreRacket1 > 0) scoreRacket1 -= 1;
            if (scoreRacket2 > 0) scoreRacket2 -= 1;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) // запуск мяча
                ball.launched = true;

            if (e.KeyCode == Keys.Escape)   // Выход из игры
                Environment.Exit(0);

            /*     //перемещение ракетки racket1 с помощью стрелок
                 if (e.KeyCode == Keys.Left) racket1.isMovingLeft = true;
                 if (e.KeyCode == Keys.Right) racket1.isMovingRight = true;

                 //перемещение ракетки racket2 с помощью 'А' и 'D'
                 if (e.KeyCode == Keys.A) racket2.isMovingLeft = true;
                 if (e.KeyCode == Keys.D) racket2.isMovingRight = true;*/
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e) // при отпускании клавиши происходит прекращение движений ракетки
        {
       /*     if (e.KeyCode == Keys.Left) racket1.isMovingLeft = false;
            if (e.KeyCode == Keys.Right) racket1.isMovingRight = false;

            if (e.KeyCode == Keys.A) racket2.isMovingLeft = false;
            if (e.KeyCode == Keys.D) racket2.isMovingRight = false;*/
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Restart_Click(object sender, EventArgs e) // загрузка новой игры
        {
            WinLabel.Hide();
            Restart.Hide();

            Controls.Remove(ball);
            Controls.Remove(racket1);
            Controls.Remove(racket2);
            foreach (Block b in blocks) Controls.Remove(b);
            blocks.Clear();
            livesLabel.Show();

            startGame();
        }

        private void startLabel_Click(object sender, EventArgs e) // старт игры
        {
            startLabel.Hide();
            gameName.Hide();
            startPic.Hide();

            startGame();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            gameName.Left = this.Width / 2 - gameName.Width / 2;
            startLabel.Left = this.Width / 2 - startLabel.Width / 2;
            rightSideBar.Left = this.Width - 175;
            leftSideBar.Height = this.Height;
            rightSideBar.Height = this.Width;
            scoreLabel.Left = rightSideBar.Left + 5;
            livesLabel.Left = rightSideBar.Left + 5;
            label3.Location = new Point(leftSideBar.Right + 5, this.Height - label3.Height-50);
            WinLabel.Left = this.Width / 2 - WinLabel.Width / 2 - 50;
            Restart.Left = this.Width / 2 - Restart.Width / 2;
        }

    }
}

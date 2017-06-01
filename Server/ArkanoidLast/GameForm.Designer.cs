namespace ArkanoidLast
{
    partial class GameForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.movementTimer = new System.Windows.Forms.Timer(this.components);
            this.scoreTimer = new System.Windows.Forms.Timer(this.components);
            this.leftSideBar = new System.Windows.Forms.PictureBox();
            this.rightSideBar = new System.Windows.Forms.PictureBox();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.livesLabel = new System.Windows.Forms.Label();
            this.Restart = new System.Windows.Forms.Label();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.WinLabel = new System.Windows.Forms.Label();
            this.gameName = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.leftSideBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightSideBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPic)).BeginInit();
            this.SuspendLayout();
            // 
            // movementTimer
            // 
            this.movementTimer.Interval = 20;
            this.movementTimer.Tick += new System.EventHandler(this.movementTimer_Tick);
            // 
            // scoreTimer
            // 
            this.scoreTimer.Interval = 1000;
            this.scoreTimer.Tick += new System.EventHandler(this.scoreTimer_Tick);
            // 
            // leftSideBar
            // 
            this.leftSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftSideBar.Location = new System.Drawing.Point(0, 0);
            this.leftSideBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.leftSideBar.Name = "leftSideBar";
            this.leftSideBar.Size = new System.Drawing.Size(227, 911);
            this.leftSideBar.TabIndex = 0;
            this.leftSideBar.TabStop = false;
            // 
            // rightSideBar
            // 
            this.rightSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rightSideBar.Location = new System.Drawing.Point(1336, 0);
            this.rightSideBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rightSideBar.Name = "rightSideBar";
            this.rightSideBar.Size = new System.Drawing.Size(227, 911);
            this.rightSideBar.TabIndex = 1;
            this.rightSideBar.TabStop = false;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scoreLabel.Font = new System.Drawing.Font("Impact", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.Yellow;
            this.scoreLabel.Location = new System.Drawing.Point(1347, 11);
            this.scoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(156, 63);
            this.scoreLabel.TabIndex = 2;
            this.scoreLabel.Text = "SCORE";
            // 
            // livesLabel
            // 
            this.livesLabel.AutoSize = true;
            this.livesLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.livesLabel.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.livesLabel.ForeColor = System.Drawing.Color.White;
            this.livesLabel.Location = new System.Drawing.Point(1352, 418);
            this.livesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.livesLabel.Name = "livesLabel";
            this.livesLabel.Size = new System.Drawing.Size(63, 29);
            this.livesLabel.TabIndex = 3;
            this.livesLabel.Text = "LIVES";
            // 
            // Restart
            // 
            this.Restart.AutoSize = true;
            this.Restart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Restart.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Restart.ForeColor = System.Drawing.Color.Yellow;
            this.Restart.Location = new System.Drawing.Point(733, 492);
            this.Restart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(158, 48);
            this.Restart.TabIndex = 5;
            this.Restart.Text = "RESTART";
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.AutoSize = true;
            this.instructionsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.instructionsLabel.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.instructionsLabel.ForeColor = System.Drawing.Color.White;
            this.instructionsLabel.Location = new System.Drawing.Point(7, 91);
            this.instructionsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(191, 116);
            this.instructionsLabel.TabIndex = 6;
            this.instructionsLabel.Text = "Каждый блок:\r\n+50\r\nКаждая секунда:\r\n-1";
            // 
            // WinLabel
            // 
            this.WinLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.WinLabel.AutoSize = true;
            this.WinLabel.BackColor = System.Drawing.Color.Transparent;
            this.WinLabel.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WinLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.WinLabel.Location = new System.Drawing.Point(533, 369);
            this.WinLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WinLabel.Name = "WinLabel";
            this.WinLabel.Size = new System.Drawing.Size(241, 75);
            this.WinLabel.TabIndex = 7;
            this.WinLabel.Text = "WINNER: ";
            // 
            // gameName
            // 
            this.gameName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gameName.AutoSize = true;
            this.gameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 71.99999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameName.ForeColor = System.Drawing.Color.DarkCyan;
            this.gameName.Location = new System.Drawing.Point(253, 283);
            this.gameName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameName.Name = "gameName";
            this.gameName.Size = new System.Drawing.Size(684, 135);
            this.gameName.TabIndex = 11;
            this.gameName.Text = "ARKANOID";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.BackColor = System.Drawing.Color.Orange;
            this.startLabel.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startLabel.ForeColor = System.Drawing.Color.Maroon;
            this.startLabel.Location = new System.Drawing.Point(645, 584);
            this.startLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(323, 75);
            this.startLabel.TabIndex = 12;
            this.startLabel.Text = "START GAME";
            this.startLabel.Click += new System.EventHandler(this.startLabel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(233, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 29);
            this.label2.TabIndex = 13;
            this.label2.Text = "PLAYER2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(233, 794);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "PLAYER1";
            // 
            // startPic
            // 
            this.startPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPic.InitialImage = null;
            this.startPic.Location = new System.Drawing.Point(0, 0);
            this.startPic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startPic.Name = "startPic";
            this.startPic.Size = new System.Drawing.Size(1563, 838);
            this.startPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.startPic.TabIndex = 13;
            this.startPic.TabStop = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1563, 838);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.gameName);
            this.Controls.Add(this.startPic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WinLabel);
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.livesLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.rightSideBar);
            this.Controls.Add(this.leftSideBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.Text = "Arkanoid";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.leftSideBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightSideBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer movementTimer;
        private System.Windows.Forms.Timer scoreTimer;
        private System.Windows.Forms.PictureBox leftSideBar;
        private System.Windows.Forms.PictureBox rightSideBar;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label livesLabel;
        private System.Windows.Forms.Label Restart;
        private System.Windows.Forms.Label instructionsLabel;
        private System.Windows.Forms.Label WinLabel;
        private System.Windows.Forms.Label gameName;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox startPic;
        
    }
}


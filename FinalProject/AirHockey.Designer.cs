namespace AirHockey
{
    partial class AirHockey
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AirHockey));
            picRightPaddle = new PictureBox();
            picPuck = new PictureBox();
            picLeftPaddle = new PictureBox();
            gameTimer = new System.Windows.Forms.Timer(components);
            btnBack = new Button();
            lblRightScore = new Label();
            lblLeftScore = new Label();
            btnStart = new Button();
            ((System.ComponentModel.ISupportInitialize)picRightPaddle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPuck).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLeftPaddle).BeginInit();
            SuspendLayout();
            // 
            // picRightPaddle
            // 
            picRightPaddle.BackColor = Color.Transparent;
            picRightPaddle.Image = (Image)resources.GetObject("picRightPaddle.Image");
            picRightPaddle.Location = new Point(680, 246);
            picRightPaddle.Name = "picRightPaddle";
            picRightPaddle.Size = new Size(58, 50);
            picRightPaddle.SizeMode = PictureBoxSizeMode.CenterImage;
            picRightPaddle.TabIndex = 8;
            picRightPaddle.TabStop = false;
            // 
            // picPuck
            // 
            picPuck.BackColor = SystemColors.ActiveCaptionText;
            picPuck.Location = new Point(479, 272);
            picPuck.Name = "picPuck";
            picPuck.Size = new Size(28, 24);
            picPuck.TabIndex = 7;
            picPuck.TabStop = false;
            // 
            // picLeftPaddle
            // 
            picLeftPaddle.Image = (Image)resources.GetObject("picLeftPaddle.Image");
            picLeftPaddle.Location = new Point(207, 246);
            picLeftPaddle.Name = "picLeftPaddle";
            picLeftPaddle.Size = new Size(58, 50);
            picLeftPaddle.SizeMode = PictureBoxSizeMode.CenterImage;
            picLeftPaddle.TabIndex = 6;
            picLeftPaddle.TabStop = false;
            // 
            // gameTimer
            // 
            gameTimer.Tick += gameTimer_Tick;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(459, 41);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 12;
            btnBack.TabStop = false;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblRightScore
            // 
            lblRightScore.AutoSize = true;
            lblRightScore.BackColor = Color.Transparent;
            lblRightScore.Font = new Font("Segoe UI", 20F);
            lblRightScore.Location = new Point(606, 9);
            lblRightScore.Name = "lblRightScore";
            lblRightScore.Size = new Size(90, 37);
            lblRightScore.TabIndex = 10;
            lblRightScore.Text = "label2";
            // 
            // lblLeftScore
            // 
            lblLeftScore.AutoSize = true;
            lblLeftScore.BackColor = Color.Transparent;
            lblLeftScore.Font = new Font("Segoe UI", 20F);
            lblLeftScore.Location = new Point(299, 9);
            lblLeftScore.Name = "lblLeftScore";
            lblLeftScore.Size = new Size(90, 37);
            lblLeftScore.TabIndex = 9;
            lblLeftScore.Text = "label1";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(459, 9);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 11;
            btnStart.TabStop = false;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // AirHockey
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(984, 561);
            Controls.Add(lblLeftScore);
            Controls.Add(btnStart);
            Controls.Add(lblRightScore);
            Controls.Add(btnBack);
            Controls.Add(picRightPaddle);
            Controls.Add(picPuck);
            Controls.Add(picLeftPaddle);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AirHockey";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AirHockey";
            Load += AirHockey_Load;
            KeyDown += AirHockey_KeyDown;
            KeyUp += AirHockey_KeyUp;
            ((System.ComponentModel.ISupportInitialize)picRightPaddle).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPuck).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLeftPaddle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox picRightPaddle;
        private PictureBox picPuck;
        private PictureBox picLeftPaddle;
        private System.Windows.Forms.Timer gameTimer;
        private Button btnBack;
        private Label lblRightScore;
        private Label lblLeftScore;
        private Button btnStart;
    }
}
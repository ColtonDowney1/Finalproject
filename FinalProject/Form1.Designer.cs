namespace AirHockey
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnInstructions = new Button();
            btnStartGame = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 30F);
            lblTitle.Location = new Point(239, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(265, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "AIR HOCKEY";
            lblTitle.Click += lblTitle_Click;
            // 
            // btnInstructions
            // 
            btnInstructions.FlatStyle = FlatStyle.Flat;
            btnInstructions.Font = new Font("Microsoft Sans Serif", 11F);
            btnInstructions.Location = new Point(294, 97);
            btnInstructions.Name = "btnInstructions";
            btnInstructions.Size = new Size(150, 45);
            btnInstructions.TabIndex = 1;
            btnInstructions.Text = "Instructions";
            btnInstructions.UseVisualStyleBackColor = true;
            btnInstructions.Click += btnInstructions_Click;
            // 
            // btnStartGame
            // 
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.Font = new Font("Microsoft Sans Serif", 11F);
            btnStartGame.Location = new Point(294, 174);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(150, 45);
            btnStartGame.TabIndex = 2;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // btnExit
            // 
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Microsoft Sans Serif", 11F);
            btnExit.Location = new Point(294, 243);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 56);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnExit);
            Controls.Add(btnStartGame);
            Controls.Add(btnInstructions);
            Controls.Add(lblTitle);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnInstructions;
        private Button btnStartGame;
        private Button btnExit;
    }
}

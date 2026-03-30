using AirHockey.Properties;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblTitle = new Label();
            btnInstructions = new Button();
            btnStartGame = new Button();
            btnExit = new Button();
            label1 = new Label();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Perpetua Titling MT", 35.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Navy;
            lblTitle.Location = new Point(236, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(349, 56);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "AIR HOCKEY";
            lblTitle.Click += lblTitle_Click;
            // 
            // btnInstructions
            // 
            btnInstructions.BackColor = Color.Navy;
            btnInstructions.FlatAppearance.BorderSize = 0;
            btnInstructions.FlatStyle = FlatStyle.Flat;
            btnInstructions.Font = new Font("Perpetua Titling MT", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnInstructions.ForeColor = Color.White;
            btnInstructions.Location = new Point(0, 67);
            btnInstructions.Name = "btnInstructions";
            btnInstructions.Size = new Size(180, 50);
            btnInstructions.TabIndex = 1;
            btnInstructions.Text = "Instructions";
            btnInstructions.UseVisualStyleBackColor = false;
            btnInstructions.Click += btnInstructions_Click;
            // 
            // btnStartGame
            // 
            btnStartGame.BackColor = Color.Navy;
            btnStartGame.BackgroundImageLayout = ImageLayout.Stretch;
            btnStartGame.FlatAppearance.BorderSize = 0;
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.Font = new Font("Perpetua Titling MT", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnStartGame.ForeColor = Color.White;
            btnStartGame.Location = new Point(0, 0);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(180, 50);
            btnStartGame.TabIndex = 2;
            btnStartGame.Text = "Game Start";
            btnStartGame.UseVisualStyleBackColor = false;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Navy;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Perpetua Titling MT", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(0, 133);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(180, 50);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Perpetua Titling MT", 15F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.SlateGray;
            label1.Location = new Point(282, 77);
            label1.Name = "label1";
            label1.Size = new Size(258, 24);
            label1.TabIndex = 4;
            label1.Text = "2 Player Air Hockey";
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSkyBlue;
            panel1.Controls.Add(btnStartGame);
            panel1.Controls.Add(btnInstructions);
            panel1.Controls.Add(btnExit);
            panel1.ForeColor = SystemColors.ActiveCaption;
            panel1.Location = new Point(309, 131);
            panel1.Name = "panel1";
            panel1.Size = new Size(181, 184);
            panel1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(lblTitle);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnInstructions;
        private Button btnStartGame;
        private Button btnExit;
        private Label label1;
        private Panel panel1;
    }
}

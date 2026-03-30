namespace AirHockey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Player 1 (BLUE) use W,A,S,D" + Environment.NewLine + "Player 2 (RED) use I,J,K,L.");
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            AirHockey gameForm = new AirHockey();
            gameForm.Show();
            this.Hide();
            btnStartGame.Text = "START GAME";
            btnStartGame.ForeColor = Color.White;
            btnStartGame.Font = new Font("Arial", 12, FontStyle.Bold);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}

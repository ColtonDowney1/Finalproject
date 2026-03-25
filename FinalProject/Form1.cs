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
            MessageBox.Show("Player 1 (BLUE) use W and S," + Environment.NewLine + "Player 2 (RED) use UP and DOWN keys.");
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            AirHockey gameForm = new AirHockey();
            gameForm.Show();
            this.Hide();
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

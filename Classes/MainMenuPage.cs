using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GolfGame.Classes
{
    public partial class MainMenuPage : UserControl
    {
        private Label titleLabel;
        private Button startButton;

        public MainMenuPage()
        {
            InitializeComponent();
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            // Criar e configurar a Label que tem o titulo do Jogo
            titleLabel = new Label
            {
                Text = "Golf Game",
                Font = new Font("Arial", 38, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 100
            };

            // Criar e configurar botao de Começo
            startButton = new Button
            {
                Text = "Start",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Bottom,
                Height = 40
            };

            

            // Add controls to the form
            this.Controls.Add(titleLabel);
            this.Controls.Add(startButton);

            // Attach click event handlers
            startButton.Click += StartButton_Click;
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            //Logica do Start
            GameManager.Instance.SaveOptions();
            GameForm gameForm = new GameForm(this.ParentForm);
            gameForm.Show();
            this.ParentForm.Hide();
            
        }

        
    }
}

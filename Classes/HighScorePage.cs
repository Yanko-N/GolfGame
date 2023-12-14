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
    public partial class HighScorePage : UserControl
    {
        public Label highScoreLabel;
        private int score;
        public HighScorePage()
        {
            InitializeComponent();
            this.BackColor = Color.Wheat;
            this.BorderStyle = BorderStyle.Fixed3D;
            
            InitializeComponents();
        }

        public void changeScore(int _score)
        {
            score = _score;
            highScoreLabel.Text = $"O melhor jogo foi com {score} tacadas";
        }
        private void InitializeComponents()
        {
            this.score = GameManager.Instance.optionsValues.highScore;

            //Criar Label para o HighScore
            highScoreLabel = new Label
            {
                Text = $"O melhor jogo foi com {score} tacadas",
                Font = new Font("Arial", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,


            };

            if (score >= 999999) highScoreLabel.Text = "Bem Vindo este trabalho foi realizado por Paulo Novo";
           

            Controls.Add(highScoreLabel);
        }
    }
}

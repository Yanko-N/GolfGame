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
        private Label highScoreLabel;
        private int score;
        public HighScorePage()
        {
            InitializeComponent();
            this.Load += HighScorePage_OnLoad;
            InitializeComponents();
        }

        private void HighScorePage_OnLoad(object sender,EventArgs e)
        {
           GameManager manager = GameManager.GetManager();
            this.score = (int)manager.optionsValues.highScore;
        }
        private void InitializeComponents()
        {
            //Criar Label para o HighScore

            highScoreLabel = new Label
            {
                Text = String.Join(" ","The Highest Score is : ", score.ToString()), 
                Font = new Font("Arial", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            highScoreLabel.BackColor = Color.LightYellow;
            highScoreLabel.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(highScoreLabel);
        }
    }
}

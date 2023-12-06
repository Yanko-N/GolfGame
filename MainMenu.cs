using GolfGame.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GolfGame
{
    public partial class MainMenu : Form
    {
        private Label titleLabel;
        private Button startButton;
        private Button optionsButton;

        public MainMenu()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {

            this.MinimumSize = new Size(300, 200); // Set the minimum size

            // Create and configure the label
            titleLabel = new Label
            {
                Text = "Game Menu",
                Font = new Font("Arial", 38, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 100 // Set a fixed height for the title label
            };

            // Create and configure the Start button
            startButton = new Button
            {
                Text = "Start",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Bottom, // Make the button span the entire width
                Height = 40 // Set a fixed height for the buttons
            };

            // Create and configure the Options button
            optionsButton = new Button
            {
                Text = "Options",
                Font = new Font("Arial", 12),
                Dock = DockStyle.Bottom, // Make the button span the entire width
                Height = 40 // Set a fixed height for the buttons
            };

            // Add controls to the form
            this.Controls.Add(titleLabel);
            this.Controls.Add(startButton);
            this.Controls.Add(optionsButton);

            // Attach click event handlers
            startButton.Click += StartButton_Click;
            optionsButton.Click += OptionsButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Start button clicked!");
            // Add your logic for the "Start" button click event
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Options button clicked!");
            // Add your logic for the "Options" button click event
        }


    }
}

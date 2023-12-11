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
    public partial class MenuForm : Form
    {

        MainMenuPage menuPart = new MainMenuPage();
        HighScorePage highScorePart = new HighScorePage();
        OptionsPage optionsPart = new OptionsPage();
        public MenuForm()
        {
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(500, 400);

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Criação de um TableLayoutPanel para organizar os controlos numa grelha
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill
            };

            
            menuPart.Dock = DockStyle.Fill;
            highScorePart.Dock = DockStyle.Fill;
            optionsPart.Dock = DockStyle.Fill;

            // Definir colunas e Linhas na tabela
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            // Adicionar os UserControls a tabela
            tableLayoutPanel.Controls.Add(menuPart, 0, 0);
            tableLayoutPanel.Controls.Add(highScorePart, 1, 0);
            tableLayoutPanel.Controls.Add(optionsPart, 1, 1);

            // Adicionar a tabela ao form
            Controls.Add(tableLayoutPanel);
        }
    }
}
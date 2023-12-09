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
    public partial class OptionsPage : UserControl
    {
        private TrackBar hitPowerSlider;
        private TrackBar frictionValueSlider;
        private Label hitPowerLabel;
        private Label frictionValueLabel;
        int hitPower;
        int friction;
        GameManager manager;
        public OptionsPage()
        {

            InitializeComponent();
            this.Load += OptionsPage_Load;

        }


        private void OptionsPage_Load(object sender, EventArgs e)
        {
            manager = GameManager.GetManager();
            this.hitPower = (int)manager.optionsValues.hitPower;
            this.friction = (int)manager.optionsValues.frictionValue;

            InitializeComponents();
        }

        private void InitializeComponents()
        {



            // Criar e configurar o slider para a potência do golpe (hit power)
            hitPowerSlider = new TrackBar
            {
                Minimum = 1,
                Maximum = 100,
                TickFrequency = 10,
                LargeChange = 10,
                SmallChange = 1,
                Value = hitPower * 10,
                Dock = DockStyle.Top
            };

            // Criar e configurar o slider para o valor de fricção
            frictionValueSlider = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                TickFrequency = 10,
                LargeChange = 10,
                SmallChange = 1,
                Value = friction * 10,
                Dock = DockStyle.Top
            };

            // Criar e configurar etiquetas para exibir os valores atuais
            hitPowerLabel = new Label
            {
                Text = String.Join(":", "Força Maxima da tacada", hitPower), // Valor inicial
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };

            frictionValueLabel = new Label
            {
                Text = String.Join(":", "Valor do Atrito do chao", friction), //valor inicial
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };

            // Adicionar controlos ao formulário
            this.Controls.Add(hitPowerLabel);
            this.Controls.Add(hitPowerSlider);
            this.Controls.Add(frictionValueLabel);
            this.Controls.Add(frictionValueSlider);

            // Anexar manipuladores de eventos
            hitPowerSlider.Scroll += HitPowerSlider_Scroll;
            frictionValueSlider.Scroll += FrictionValueSlider_Scroll;
        }

        private void HitPowerSlider_Scroll(object sender, EventArgs e)
        {
            double hitPowerValue = hitPowerSlider.Value / 10.0;
            hitPowerLabel.Text = $"Potência do Golpe: {hitPowerValue:F1}";
            GameManager.Instance.optionsValues.hitPower = (float)hitPowerValue;
            
        }

        private void FrictionValueSlider_Scroll(object sender, EventArgs e)
        {
            double frictionValue = frictionValueSlider.Value / 10.0;
            frictionValueLabel.Text = $"Valor de Fricção: {frictionValue:F1}";
            GameManager.Instance.optionsValues.frictionValue = (float)friction;


        }
    }
}

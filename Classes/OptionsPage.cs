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

        private TrackBar maxSpeedValueSlider;
        private Label maxSpeedLabel;

        int hitPower;
        int friction;
        int maxSpeed;


        GameManager manager;
        public OptionsPage()
        {

            InitializeComponent();
            this.Load += OptionsPage_Load;
            this.BackColor = Color.WhiteSmoke;
            

        }


        private void OptionsPage_Load(object sender, EventArgs e)
        {
            manager = GameManager.GetManager();
            this.hitPower = (int)(manager.optionsValues.hitPower * 10);
            this.friction = (int)(manager.optionsValues.frictionValue * 10);
            this.maxSpeed = (int)(manager.optionsValues.maxSpeed * 10);

            InitializeComponents();
        }

        private void InitializeComponents()
        {

            // Criar e configurar o slider para a potência do golpe (hit power)
            maxSpeedValueSlider = new TrackBar
            {
                Minimum = 1,
                Maximum = 100,
                TickFrequency = 10,
                LargeChange = 10,
                SmallChange = 1,
                Value = maxSpeed,
                Dock = DockStyle.Top
            };

            // Criar e configurar o slider para a potência do golpe (hit power)
            hitPowerSlider = new TrackBar
            {
                Minimum = 1,
                Maximum = 100,
                TickFrequency = 10,
                LargeChange = 10,
                SmallChange = 1,
                Value = hitPower,
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
                Value = friction,
                Dock = DockStyle.Top
            };

            //Criar e configurar a label para os valroes atuais
            maxSpeedLabel = new Label
            {
                Text = $"Velocidade Maxima da Bola {maxSpeed /10f}", // Valor inicial
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };
            // Criar e configurar etiquetas para exibir os valores atuais
            hitPowerLabel = new Label
            {
                Text = $"Potencia da tacada {hitPower / 10f}", // Valor inicial
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };

            frictionValueLabel = new Label
            {
                Text = $"Valor do Atrito do Terreno {friction / 10f}", //valor inicial
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };

            // Adicionar controlos ao formulário
            this.Controls.Add(maxSpeedLabel);
            this.Controls.Add(maxSpeedValueSlider);
            this.Controls.Add(hitPowerLabel);
            this.Controls.Add(hitPowerSlider);
            this.Controls.Add(frictionValueLabel);
            this.Controls.Add(frictionValueSlider);

            // Anexar manipuladores de eventos
            maxSpeedValueSlider.Scroll += MaxSpeedValueSlider_Scroll;
            hitPowerSlider.Scroll += HitPowerSlider_Scroll;
            frictionValueSlider.Scroll += FrictionValueSlider_Scroll;
        }

        private void MaxSpeedValueSlider_Scroll(object? sender, EventArgs e)
        {

            float maxSpeedValue = maxSpeedValueSlider.Value / 10f;
            maxSpeedLabel.Text = $"Velocidade Maxima da Bola {maxSpeedValue:F1}";
            GameManager.Instance.optionsValues.maxSpeed = (float)maxSpeedValue;
        }

        private void HitPowerSlider_Scroll(object sender, EventArgs e)
        {
            float hitPowerValue = (hitPowerSlider.Value / 10f);
            hitPowerLabel.Text = $"Potencia da tacada {hitPowerValue:F1}";
            GameManager.Instance.optionsValues.hitPower = (float)hitPowerValue;

        }

        private void FrictionValueSlider_Scroll(object sender, EventArgs e)
        {
            float frictionValue = frictionValueSlider.Value / 10f;
            frictionValueLabel.Text = $"Valor do Atrito do Terreno {frictionValue:F1}";
            GameManager.Instance.optionsValues.frictionValue = (float)frictionValue;


        }
    }
}

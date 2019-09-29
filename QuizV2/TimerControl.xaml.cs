using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuizV2
{
    /// <summary>
    /// Interação lógica para TimerControl.xam
    /// </summary>
    public partial class TimerControl : UserControl
    {
        private DispatcherTimer dt;
        private int _increment;
        public TimerControl()
        {
            InitializeComponent();
        }

        public void StartTimer()
        {
            try
            {
                dt.Stop();
            }
            catch (Exception){}

            dt = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            TimerText.Text = "15";


            _increment = 15;
            dt.Tick += (_,__) => {
                _increment--;
                TimerText.Text = _increment.ToString();

                if (_increment <= 0)
                {
                    _increment = 1;
                }
            };


            dt.Start();

            var at = new DoubleAnimation
            {
                To = 0,
                Duration = new Duration(new TimeSpan(0, 0, 15)),
                From = 100
            };


            snad.BeginAnimation(ProgressBar.ValueProperty, at);
            snad1.BeginAnimation(ProgressBar.ValueProperty, at);
        }
    }
}

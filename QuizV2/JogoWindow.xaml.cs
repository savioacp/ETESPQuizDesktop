using QuizV2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuizV2
{
    /// <summary>
    /// Lógica interna para JogoWindow.xaml
    /// </summary>
    public partial class JogoWindow : Window
    {
        Quiz quiz;
        public JogoWindow()
        {
            InitializeComponent();
            quiz = new Quiz(Data.Cache.Perguntas, Data.Cache.Equipes);

            var cmdSair = new RoutedCommand {InputGestures = { new KeyGesture(Key.Escape) }};
            var cmdConfirmarSair = new RoutedCommand {InputGestures = { new KeyGesture(Key.Enter) }};
            var cmdNext = new RoutedCommand {InputGestures = {new KeyGesture(Key.F1)}};
            var cmdRanking = new RoutedCommand { InputGestures = { new KeyGesture(Key.F2) } };
            var cmdFinalizar = new RoutedCommand { InputGestures = { new KeyGesture(Key.F5) } };


            CommandBindings.Add(new CommandBinding(cmdSair, Sair));
            CommandBindings.Add(new CommandBinding(cmdConfirmarSair, ConfirmarSaída));
            CommandBindings.Add(new CommandBinding(cmdNext, NextPergunta));
            CommandBindings.Add(new CommandBinding(cmdRanking, SwitchRanking));
            LoadPergunta(quiz.NextPergunta());
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ;
        }

        private void Sair(object sender, RoutedEventArgs e)
        {
            dlgConfirmarFechar.IsOpen = !dlgConfirmarFechar.IsOpen;
        }

        private void ConfirmarSaída(object sender, RoutedEventArgs e)
        {
            if(dlgConfirmarFechar.IsOpen) Close();
        }
        private void NextPergunta(object sender, RoutedEventArgs e)
        {
            (Pergunta aCarregar, int idAtual) = quiz.NextPergunta();
            LoadPergunta(aCarregar, idAtual);

        }

        private void SwitchRanking(object sender, RoutedEventArgs e)
        {
            Transitioner.SelectedIndex = Transitioner.SelectedIndex == 0 ? 1 : 0;
        }

        private void LoadPergunta(Pergunta p, int i)
        {
            if (p.Dissertativa)
            {
                label.Text = $"{i}- {p.Texto}";
                txtRespostaA.Text = "";
                txtRespostaB.Text = "";
                txtRespostaC.Text = "";
                txtRespostaD.Text = "";
            }
            else
            {
                label.Text = $"{i}- {p.Texto}";
                txtRespostaA.Text = $"A) {p.Respostas[0]}";
                txtRespostaB.Text = $"B) {p.Respostas[1]}";
                txtRespostaC.Text = $"C) {p.Respostas[2]}";
                txtRespostaD.Text = $"D) {p.Respostas[3]}";
            }
            if(p.TemImagem)
            {
                img.Visibility = Visibility.Visible;
                img.Source = Serializa.GetImageSourceFromImage(p.Imagem);
                Grid.SetColumn(aaaaaa, 1);
            }
            else
            {
                img.Visibility = Visibility.Hidden;
                Grid.SetColumn(aaaaaa, 0);
            }
        }
    }
}

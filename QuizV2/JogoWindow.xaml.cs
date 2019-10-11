using QuizV2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuizV2
{
    /// <summary>
    /// Lógica interna para JogoWindow.xaml
    /// </summary>
    public partial class JogoWindow : Window
    {
        MediaPlayer mediaPlayer;
        bool TopQuiz;
        List<ListViewItemWithLittleImage> equipesOrdenadas;
        List<EquipeItemFinalizar> equipesFinalizar;
        Quiz quiz;
        Estado estado;
        public JogoWindow()
        {
            InitializeComponent();
            equipesOrdenadas = new List<ListViewItemWithLittleImage>();
            equipesFinalizar = new List<EquipeItemFinalizar>();
            quiz = new Quiz();
            TopQuiz = false;

            var cmdSair = new RoutedCommand {InputGestures = { new KeyGesture(Key.Escape) }};
            var cmdConfirmarSair = new RoutedCommand {InputGestures = { new KeyGesture(Key.Enter) }};
            var cmdNext = new RoutedCommand {InputGestures = {new KeyGesture(Key.F1)}};
            var cmdRanking = new RoutedCommand { InputGestures = { new KeyGesture(Key.F2) } };
            var cmdPodio = new RoutedCommand { InputGestures = { new KeyGesture(Key.F5) } };


            CommandBindings.Add(new CommandBinding(cmdSair, Sair));
            CommandBindings.Add(new CommandBinding(cmdConfirmarSair, ConfirmarSaída));
            CommandBindings.Add(new CommandBinding(cmdNext, NextState));
            CommandBindings.Add(new CommandBinding(cmdRanking, SwitchRanking));
            CommandBindings.Add(new CommandBinding(cmdPodio, Pódio));

            txtRespostaA.Text = "";
            txtRespostaB.Text = "";
            txtRespostaC.Text = "";
            txtRespostaD.Text = "";


            label.Text = "O Jogo está pronto para começar!";

            estado = Estado.Pergunta;
            img.Visibility = Visibility.Collapsed;


            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri($"{Environment.CurrentDirectory}\\Media\\timer-sound.mp3", UriKind.Absolute));
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
        private void NextState(object sender, RoutedEventArgs e)
        {
            switch (estado)
            {
                case Estado.Pergunta:
                    if (quiz.Equipes.Count(eq => !eq.Eliminada) == 3)
                    {
                        quiz.TopQuiz();
                    }

                    ValueTuple<Pergunta, int> a = quiz.NextPergunta();

                    foreach (var textBlock in stpRespostas.Children.OfType<TextBlock>())
                    {
                        textBlock.Foreground = Brushes.Black;
                    }
                    if (a.Item1 == null)
                    {
                        if (TopQuiz)
                            Pódio(null, null);
                        return;
                    }
                    LoadPergunta(a.Item1, a.Item2);
                    TimerControl.StartTimer();
                    mediaPlayer.Play();
                    estado = Estado.Resposta;
                    
                    break;
                case Estado.Resposta:
                    stpRespostas.Children.OfType<TextBlock>().First(txt => Regex.IsMatch(txt.Text, $"[ABCD]\\) {quiz.Pergunta.Correta}")).Foreground = Brushes.GreenYellow;
                    estado = Estado.Resultado;
                    mediaPlayer.Stop();
                    mediaPlayer.Position = TimeSpan.Zero;
                    break;
                case Estado.Resultado:
                    equipesFinalizar.Clear();
                    foreach (EquipeParticipando eq in quiz.Equipes)
                    {
                        equipesFinalizar.Add(new EquipeItemFinalizar
                        {
                            Equipe = eq,
                            IsRight = false
                        });
                    }

                    ItemsControlFinalizar.ItemsSource = equipesFinalizar.ToArray();
                    DialogHostFinalizar.IsOpen = true;

                    
                    estado = Estado.Pergunta;
                    break;
            }
        }

        private void Pódio(object sender, RoutedEventArgs e)
        {
            var Ordenada = quiz.Equipes.Where(eq => !eq.Eliminada).OrderByDescending(eq => eq.Pontos).ToList();
            var textPrimeiro = $"{Ordenada[0].Nome}";
            var textSegundo  = $"{Ordenada[1].Nome}";
            var textTerceiro = $"{Ordenada[2].Nome}";

            Transitioner.SelectedIndex = 1;
            textBlock.Text = textTerceiro;
            textBlock_Copy1.Text = textSegundo;
            textBlock_Copy.Text = textPrimeiro;

            var st = FindResource("animation") as Storyboard;
            st.Begin();
        }
        private void SwitchRanking(object sender, RoutedEventArgs e)
        {
            var rParcial = quiz.GetParcialRanking();
            ToggleButtonRepescagem.Visibility = TopQuiz ? Visibility.Collapsed : Visibility.Visible;
            equipesOrdenadas.Clear();
            equipesOrdenadas.Add(new ListViewItemWithLittleImage
            {
                Equipe = rParcial[0],
                Image = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/medalhaPrimeiro.png", UriKind.Absolute)),
                Index = "1º"
            });
            equipesOrdenadas.Add(new ListViewItemWithLittleImage
            {
                Equipe = rParcial[1],
                Image = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/medalhaSegundo.png", UriKind.Absolute)),
                Index = "2º"
            });
            equipesOrdenadas.Add(new ListViewItemWithLittleImage
            {
                Equipe = rParcial[2],
                Image = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/medalhaTerceiro.png", UriKind.Absolute)),
                Index = "3º"
            });
            if(!TopQuiz)
                for(int i = 3; i < rParcial.Length; i++)
                {
                    equipesOrdenadas.Add(new ListViewItemWithLittleImage
                    {
                        Equipe = rParcial[i],
                        Index = $"{i + 1}º"
                    });
                }

            lbxRanking.ItemsSource = equipesOrdenadas.ToArray();
            DrawerHost.IsRightDrawerOpen = !DrawerHost.IsRightDrawerOpen;
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
                Grid.SetColumn(stpRespostas, 1);
            }
            else
            {
                img.Visibility = Visibility.Collapsed;
                Grid.SetColumn(stpRespostas, 0);
            }
        }

        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonConfirmarFinalizar_OnClick(object sender, RoutedEventArgs e)
        {

            if(ToggleButtonRepescagem.IsChecked == true)
                quiz.Recuperação(equipesFinalizar.Where(eq => eq.IsRight).Select(eq => eq.Equipe).ToArray());
            else
                quiz.FinalizarPergunta(equipesFinalizar.Where(eq => eq.IsRight).Select(eq => eq.Equipe).ToArray());

            DialogHostFinalizar.IsOpen = false;

            ToggleButtonRepescagem.IsChecked = false;
        }

        private void ToggleButtonRepescagem_OnClick(object sender, RoutedEventArgs e)
        {
            ;
        }

        public void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        class ListViewItemWithLittleImage
        {
            public EquipeParticipando Equipe { get; set; }
            public BitmapImage Image { get; set; }
            public string Index { get; set; }
            public string Nome => Equipe.Nome;
            public string Cor => Equipe.Cor;
            public string Pontos => $"{Equipe.Pontos}pts";
            public string Foreground => Equipe.Eliminada ? "#ff1744" : "#000000";


            public Visibility ImageVisibility => Image == null ? Visibility.Collapsed : Visibility.Visible;
            public Visibility IndexVisibility => Image == null ? Visibility.Visible : Visibility.Collapsed;
        }

        class EquipeItemFinalizar
        {
            public EquipeParticipando Equipe { get; set; }
            public bool IsRight { get; set; }
            public string Nome => Equipe.Nome;
            public string Cor => Equipe.Cor;
            public string Pontos => $"{Equipe.Pontos}pts";
            public string Foreground => Equipe.Eliminada ? "#ff1744" : "#000000";

        }
        enum Estado
        {
            Pergunta, Resposta, Resultado
        }


        private void BtnVoltarSair_OnClick(object sender, RoutedEventArgs e)
        {
            dlgConfirmarFechar.IsOpen = false;
        }
    }
}

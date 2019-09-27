using QuizV2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        enum Estado
        {
            Pergunta, Resposta, Resultado
        }
        List<ListViewItemWithLittleImage> equipesOrdenadas;
        Quiz quiz;
        Estado estado;
        public JogoWindow()
        {
            InitializeComponent();
            equipesOrdenadas = new List<ListViewItemWithLittleImage>();
            quiz = new Quiz(Data.Cache.Perguntas, Data.Cache.Equipes);

            var cmdSair = new RoutedCommand {InputGestures = { new KeyGesture(Key.Escape) }};
            var cmdConfirmarSair = new RoutedCommand {InputGestures = { new KeyGesture(Key.Enter) }};
            var cmdNext = new RoutedCommand {InputGestures = {new KeyGesture(Key.F1)}};
            var cmdRanking = new RoutedCommand { InputGestures = { new KeyGesture(Key.F2) } };


            CommandBindings.Add(new CommandBinding(cmdSair, Sair));
            CommandBindings.Add(new CommandBinding(cmdConfirmarSair, ConfirmarSaída));
            CommandBindings.Add(new CommandBinding(cmdNext, NextState));
            CommandBindings.Add(new CommandBinding(cmdRanking, SwitchRanking));
            ValueTuple<Pergunta, int> a = quiz.NextPergunta();
            LoadPergunta(a.Item1, a.Item2);


            lbxRanking.ItemsSource = equipesOrdenadas;
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
                    ValueTuple<Pergunta, int> a = quiz.NextPergunta();
                    LoadPergunta(a.Item1, a.Item2);
                    estado = Estado.Resposta;
                    break;
                case Estado.Resposta:

                    estado = Estado.Resultado;
                    break;
                case Estado.Resultado:

                    estado = Estado.Pergunta;
                    break;
            }
        }

        private void SwitchRanking(object sender, RoutedEventArgs e)
        {
            var rParcial = quiz.GetParcialRanking();
            equipesOrdenadas.Clear();
            equipesOrdenadas.Add(new ListViewItemWithLittleImage()
            {
                Equipe = rParcial[0],
                Image = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/medalhaPrimeiro.png", UriKind.Absolute)),
                Index = 1
            });
            equipesOrdenadas.Add(new ListViewItemWithLittleImage()
            {
                Equipe = rParcial[1],
                Image = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/medalhaSegundo.png", UriKind.Absolute)),
                Index = 2
            });
            equipesOrdenadas.Add(new ListViewItemWithLittleImage()
            {
                Equipe = rParcial[2],
                Image = new BitmapImage(new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/Images/medalhaTerceiro.png", UriKind.Absolute)),
                Index = 3
            });
            for(int i = 3; i < rParcial.Length; i++)
            {
                equipesOrdenadas.Add(new ListViewItemWithLittleImage()
                {
                    Equipe = rParcial[i],
                    Index = i + 1
                });
            }

            lbxRanking.ItemsSource = equipesOrdenadas.ToArray();
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

        class ListViewItemWithLittleImage
        {
            public EquipeParticipando Equipe { get; set; }
            public BitmapImage Image { get; set; }
            public int Index { get; set; }
            public string Nome => Equipe.Nome;
            public string Cor => Equipe.Cor;
            public int Pontos => Equipe.Pontos;

            public Visibility ImageVisibility => Image == null ? Visibility.Collapsed : Visibility.Visible;
            public Visibility IndexVisibility => Image == null ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

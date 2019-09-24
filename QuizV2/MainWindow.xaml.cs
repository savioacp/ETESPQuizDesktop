using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Drawing;
using Microsoft.Win32;

namespace QuizV2
{
	/// <summary>
	/// Interação lógica para MainWindow.xaml
	/// </summary>
    
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			mnuBtnJogo.Click += (_, __) => contentTransitioner.SelectedIndex = 0;
			mnuBtnEquipes.Click += (_, __) => contentTransitioner.SelectedIndex = 1;
			mnuBtnPerguntas.Click += (_,__) => contentTransitioner.SelectedIndex = 2;
			mnuBtnSobre.Click += (_,__) => contentTransitioner.SelectedIndex = 3;
		}

		public void btnIniciarJogo_Click(object sender, RoutedEventArgs e)
		{
            (new JogoWindow()).Show();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            AtualizarEquipes();
            AtualizarPerguntas();
			//foreach(Equipe eq in Data.DataManager.GetEquipes())
			//	MessageBox.Show($"Nome: {eq.Nome}({eq.Id})\n\tCor: {eq.Cor}\n\t{eq.Integrantes[0]}\n\t{eq.Integrantes[1]}\n\t{eq.Integrantes[2]}\n\t{eq.Integrantes[3]}\n\t{eq.Integrantes[4]}");
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Não, isso não funciona...");
		}

        public static void AtualizarEquipes()
        {
            MainWindow currentWindow = Application.Current.MainWindow as MainWindow;
            Data.Cache.Equipes = Data.DataManager.GetEquipes();
            currentWindow.wrpEquipes.Children.Clear();
            foreach (Equipe eq in Data.Cache.Equipes)
            {
                currentWindow.wrpEquipes.Children.Add(new EquipeCard(eq));
            }
        }

        public static void AtualizarPerguntas()
        {
            MainWindow currentWindow = Application.Current.MainWindow as MainWindow;
            Data.Cache.Perguntas = Data.DataManager.GetPerguntas();
            currentWindow.stpPerguntas.Children.Clear();
            foreach (Pergunta pergunta in Data.Cache.Perguntas)
            {
                currentWindow.stpPerguntas.Children.Add(new PerguntaExpander(pergunta));
            }
        }
        public static void Notificar(string mensagem)
        {                                                                                                    
            MainWindow currentWindow = Application.Current.MainWindow as MainWindow;                         
            currentWindow.snackbarNotifications.MessageQueue.Enqueue(mensagem, "OK", () => {; });            
        }

        private void BtnAdicionarEquipe_Click(object sender, RoutedEventArgs e)
        {
            dlgAddEquipe.IsOpen = true;
        }

        private void btnConfirmarAdicionar_Click(object sender, RoutedEventArgs e)
        {

            Equipe toAdd = new Equipe()  
            { 
                Integrantes =  new[] {txtAddEquipeIntegrante1.Text, txtAddEquipeIntegrante2.Text, txtAddEquipeIntegrante3.Text, txtAddEquipeIntegrante4.Text, txtAddEquipeIntegrante5.Text },
                Nome = txtAddEquipeNome.Text,
                Cor = "#ffffff"
            };
            Data.DataManager.AddEquipe(toAdd);                                            
            dlgAddEquipe.IsOpen = false;                                                  
            Notificar($"Equipe \"{toAdd.Nome}\" adicionada com sucesso!");                
            AtualizarEquipes();
        }
        private void btnVoltarAdicionarEquipe_Click(object sender, RoutedEventArgs e)
        {
            dlgAddEquipe.IsOpen = false;
        }

        private void BtnAdicionarPergunta_Click(object sender, RoutedEventArgs e)
        {
            dlgAddPergunta.IsOpen = true;
        }
        
        private void CtcImagem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "Escolha uma imagem do computador.",
                Filter = "Arquivos de imagem (*.png; *.jpg; *.bmp) | *.png; *.jpg; *.bmp"
            };

            if(ofd.ShowDialog() == true)
            {
                img1.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void tgbDissertativa_Click(object sender, RoutedEventArgs e)
        {
            if (tgbDissertativa.IsChecked == true)
            {
                txtRespostaA.Visibility = Visibility.Hidden;
                txtRespostaB.Visibility = Visibility.Hidden;
                txtRespostaC.Visibility = Visibility.Hidden;
                txtRespostaD.Visibility = Visibility.Hidden;

                rdbRespostaA.Visibility = Visibility.Hidden;
                rdbRespostaB.Visibility = Visibility.Hidden;
                rdbRespostaC.Visibility = Visibility.Hidden;
                rdbRespostaD.Visibility = Visibility.Hidden;

                txtRespostaDissertativa.Visibility = Visibility.Visible;
            }
            else
            {
                txtRespostaA.Visibility = Visibility.Visible;
                txtRespostaB.Visibility = Visibility.Visible;
                txtRespostaC.Visibility = Visibility.Visible;
                txtRespostaD.Visibility = Visibility.Visible;

                rdbRespostaA.Visibility = Visibility.Visible;
                rdbRespostaB.Visibility = Visibility.Visible;
                rdbRespostaC.Visibility = Visibility.Visible;
                rdbRespostaD.Visibility = Visibility.Visible;

                txtRespostaDissertativa.Visibility = Visibility.Hidden;
            }
        }

        private void btnVoltarAdicionarPergunta_Click(object sender, RoutedEventArgs e)
        {
            txtRespostaA.Text = "";
            txtRespostaB.Text = "";
            txtRespostaC.Text = "";
            txtRespostaD.Text = "";
            dlgAddPergunta.IsOpen = false;
        }
        private void btnConfirmarAdicionarPergunta_Click(object sender, RoutedEventArgs e)
        {
            if (tgbDissertativa.IsChecked == true)
            {

                Pergunta pergunta = new Pergunta
                {
                    Texto = txtTextoPergunta.Text,
                    Imagem = Serializa.GetImageFromImageSource(img1.Source),
                    TopQuiz = tgbTopQuiz.IsChecked ?? false,
                    Correta = txtRespostaDissertativa.Text,
                    Respostas = new[] { txtRespostaDissertativa.Text }
                };

                Data.DataManager.AddPergunta(pergunta);
                dlgAddPergunta.IsOpen = false;
            }
            else
            { 
                string correta = "";
                if (rdbRespostaA.IsChecked ?? false) correta = txtRespostaA.Text;
                if (rdbRespostaB.IsChecked ?? false) correta = txtRespostaB.Text;
                if (rdbRespostaC.IsChecked ?? false) correta = txtRespostaC.Text;
                if (rdbRespostaD.IsChecked ?? false) correta = txtRespostaD.Text;

                var vazia = BitmapSource.Create(2, 2, 96, 96, PixelFormats.Indexed1, new BitmapPalette(new List<System.Windows.Media.Color> { Colors.Transparent }), new byte[] { 0, 0, 0, 0 }, 1);
                var bmpVazio = new Bitmap(Properties.Resources.transparente as System.Drawing.Image);


                Pergunta pergunta = new Pergunta
                {
                    Texto = txtTextoPergunta.Text,
                    Imagem = Serializa.GetImageFromImageSource(img1.Source),
                    TopQuiz = tgbTopQuiz.IsChecked ?? false,
                    Correta = correta,
                    Respostas = new [] { txtRespostaA.Text, txtRespostaB.Text, txtRespostaC.Text, txtRespostaD.Text },              
                };

                Data.DataManager.AddPergunta(pergunta);
                dlgAddPergunta.IsOpen = false;
            }
            Notificar("Pergunta adicionada com sucesso!");
            AtualizarPerguntas();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!dlgConfirmarFechar.IsOpen)
            {
                e.Cancel = true;
                dlgConfirmarFechar.IsOpen = true;
            }
        }

        private void BtnConfirmarFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnVoltarFechar_Click(object sender, RoutedEventArgs e)
        {
            dlgConfirmarFechar.IsOpen = false;
        }
    }
}

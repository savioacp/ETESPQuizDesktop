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

namespace QuizV2
{
	/// <summary>
	/// Interação lógica para MainWindow.xam
	/// </summary>
    /// CMB WAZ HERE
    
	public partial class MainWindow : Window
	{
        Equipe[] equipes;
		public MainWindow()
		{
			InitializeComponent();
			mnuBtnJogo.Click += new RoutedEventHandler((_, __) => contentTransitioner.SelectedIndex = 0);
			mnuBtnEquipes.Click += new RoutedEventHandler((_, __) => contentTransitioner.SelectedIndex = 1);
			mnuBtnPerguntas.Click += new RoutedEventHandler((_,__) => contentTransitioner.SelectedIndex = 2);
			mnuBtnSobre.Click += new RoutedEventHandler((_,__) => contentTransitioner.SelectedIndex = 3);
		}

		public void btnIniciarJogo_Click(object sender, RoutedEventArgs e)
		{
			;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            AtualizarEquipes();

			//foreach(Equipe eq in Data.DataManager.GetEquipes())
			//	MessageBox.Show($"Nome: {eq.Nome}({eq.Id})\n\tCor: {eq.Cor}\n\t{eq.Integrantes[0]}\n\t{eq.Integrantes[1]}\n\t{eq.Integrantes[2]}\n\t{eq.Integrantes[3]}\n\t{eq.Integrantes[4]}");
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Não, isso não funciona...");//será?
		}

        public static void AtualizarEquipes()
        {
            MainWindow currentWindow = Application.Current.MainWindow as MainWindow;
            currentWindow.equipes = Data.DataManager.GetEquipes();
            currentWindow.wrpEquipes.Children.Clear();
            foreach (Equipe eq in currentWindow.equipes)
            {
                currentWindow.wrpEquipes.Children.Add(new EquipeCard(eq));
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

            Equipe toAdd = new Equipe()  //
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
            MessageBox.Show("");
        }

        private void tgbDissertativa_Click(object sender, RoutedEventArgs e)
        {
            if (tgbDissertativa.IsChecked == true)
            {
                txtRespostaA.IsEnabled = false;
                txtRespostaB.IsEnabled = false;
                txtRespostaC.IsEnabled = false;
                txtRespostaD.IsEnabled = false;

                rdbRespostaA.IsEnabled = false;
                rdbRespostaB.IsEnabled = false;
                rdbRespostaC.IsEnabled = false;
                rdbRespostaD.IsEnabled = false;


            }
            else
            {
                txtRespostaA.IsEnabled = true;
                txtRespostaB.IsEnabled = true;
                txtRespostaC.IsEnabled = true;
                txtRespostaD.IsEnabled = true;

                rdbRespostaA.IsEnabled = true;
                rdbRespostaB.IsEnabled = true;
                rdbRespostaC.IsEnabled = true;
                rdbRespostaD.IsEnabled = true;
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
            string correta = "";
            if (rdbRespostaA.IsChecked ?? false) correta = txtRespostaA.Text;
            if (rdbRespostaB.IsChecked ?? false) correta = txtRespostaB.Text;
            if (rdbRespostaC.IsChecked ?? false) correta = txtRespostaC.Text;
            if (rdbRespostaD.IsChecked ?? false) correta = txtRespostaD.Text;
            Pergunta pergunta = new Pergunta
            {
                Texto = txtTextoPergunta.Text,
                Imagem = Serializa.GetImageFromImageSource(img1.Source),
                TopQuiz = tgbTopQuiz.IsChecked ?? false,
                Correta = correta,
                
            };
        }

    }
}

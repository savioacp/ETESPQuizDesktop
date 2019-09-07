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
			MessageBox.Show("Não, isso não funciona...");
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
        private void btnVoltarAdicionar_Click(object sender, RoutedEventArgs e)
        {
            dlgAddEquipe.IsOpen = false;
        }
    }
}

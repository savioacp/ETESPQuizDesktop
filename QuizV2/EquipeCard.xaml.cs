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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizV2
{
    /// <summary>
    /// Interação lógica para EquipeCard.xam
    /// </summary>
    public partial class EquipeCard : UserControl
    {
        private Equipe equipe;
        public EquipeCard(Equipe equipe)
        {
            InitializeComponent();
            this.equipe = equipe;
            txtNomeEquipe.Content = equipe.Nome;
            txtMembro1.Text = equipe.Integrantes[0];
            txtMembro2.Text = equipe.Integrantes[1];
            txtMembro3.Text = equipe.Integrantes[2];
            txtMembro4.Text = equipe.Integrantes[3];
            txtMembro5.Text = equipe.Integrantes[4];
            cardPrincipal.Background = new BrushConverter().ConvertFromString(equipe.Cor) as Brush;
        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            dlgDeletar.IsOpen = true;
        }

        private void txtNomeEquipe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnConfirmarDeletar_Click(object sender, RoutedEventArgs e)
        {
            dlgDeletar.IsOpen = false;
            Data.DataManager.DeleteEquipe(equipe.Id);
            MainWindow.AtualizarEquipes();
        }
        private void btnVoltarDeletar_Click(object sender, RoutedEventArgs e)
        {
            dlgDeletar.IsOpen = false;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
			Equipe newEquipe = new Equipe
			{
				Nome = txtNomeEquipe.Content.ToString(),
				Integrantes = new string[] { txtMembro1.Text, txtMembro2.Text, txtMembro3.Text, txtMembro4.Text, txtMembro5.Text },
				Cor = equipe.Cor,
				Id = equipe.Id,
				Pontuação = equipe.Pontuação
			};

			Data.DataManager.UpdateEquipe(equipe.Id, newEquipe);
			MainWindow.AtualizarEquipes();
        }
    }
}

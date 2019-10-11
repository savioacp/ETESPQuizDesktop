using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.Win32;

namespace QuizV2
{
    /// <summary>
    /// Interação lógica para PerguntaExpander.xam
    /// </summary>
    public partial class PerguntaExpander : UserControl
    {
        Pergunta pergunta;
        public PerguntaExpander(Pergunta pergunta)
        {
            InitializeComponent();
            this.pergunta = pergunta;
			txtTextoPergunta.Text = pergunta.Texto;
            if (pergunta.Dissertativa)
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

                txtRespostaDissertativa.Text = pergunta.Respostas[0];
            }
            else
            {
                txtRespostaA.Text = pergunta.Respostas[0];
                txtRespostaB.Text = pergunta.Respostas[1];
                txtRespostaC.Text = pergunta.Respostas[2];
                txtRespostaD.Text = pergunta.Respostas[3];

                rdbRespostaA.IsChecked = pergunta.Correta == pergunta.Respostas[0];
                rdbRespostaB.IsChecked = pergunta.Correta == pergunta.Respostas[1];
                rdbRespostaC.IsChecked = pergunta.Correta == pergunta.Respostas[2];
                rdbRespostaD.IsChecked = pergunta.Correta == pergunta.Respostas[3];
            }

            rdbRespostaA.GroupName = pergunta.Texto;
            rdbRespostaB.GroupName = pergunta.Texto;
            rdbRespostaC.GroupName = pergunta.Texto;
            rdbRespostaD.GroupName = pergunta.Texto;
            tgbDissertativa.IsChecked = pergunta.Dissertativa;
            tgbTopQuiz.IsChecked = pergunta.TopQuiz;

            tgbImagem.IsChecked = pergunta.TemImagem;

            if(pergunta.TemImagem) img1.Source = Serializa.GetImageSourceFromImage(pergunta.Imagem);
            
        }

		private void BtnDeletar_Click(object sender, RoutedEventArgs e)
		{
            Data.DataManager.DeletePergunta(pergunta);
            MainWindow.AtualizarPerguntas();
		}

        private void ExpPergunta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void tgbDissertativa_Click(object sender, RoutedEventArgs e)
        {
            if(tgbDissertativa.IsChecked == true)
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

                txtRespostaDissertativa.Text = pergunta.Respostas[0];

                txtRespostaA.Text = "";
                txtRespostaB.Text = "";
                txtRespostaC.Text = "";
                txtRespostaD.Text = "";
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

                txtRespostaA.Text = pergunta.Respostas[0];
                txtRespostaDissertativa.Text = "";
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string correta = "";
            if (rdbRespostaA.IsChecked == true)
                correta = txtRespostaA.Text;

            if (rdbRespostaB.IsChecked == true)
                correta = txtRespostaB.Text;

            if (rdbRespostaC.IsChecked == true)
                correta = txtRespostaC.Text;

            if (rdbRespostaD.IsChecked == true)
                correta = txtRespostaD.Text;

            if (tgbDissertativa.IsChecked == true)
                correta = txtRespostaDissertativa.Text;

            Pergunta newPergunta = new Pergunta
            {
                Imagem = tgbImagem.IsChecked == true ? Serializa.GetImageFromImageSource(img1.Source) : null,
                Texto = expPergunta.Header.ToString(),
                TopQuiz = tgbTopQuiz.IsChecked == true,
                Correta = correta,
                Respostas = tgbDissertativa.IsChecked == true ? new[] { txtRespostaDissertativa.Text } : new[] { txtRespostaA.Text, txtRespostaB.Text, txtRespostaC.Text, txtRespostaD.Text },
            };
            Data.DataManager.UpdatePergunta(pergunta.Id, newPergunta);
            MainWindow.Notificar("Pergunta editada com sucesso!");
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtTextoPergunta_GotFocus(object sender, RoutedEventArgs e)
        {
            expPergunta.IsExpanded = true;
        }

        private void CtcImagem_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tgbImagem.IsChecked == false) tgbImagem.IsChecked = true;
            var ofd = new OpenFileDialog()
            {
                Title = "Escolha uma imagem do computador.",
                Filter = "Arquivos de imagem (*.png; *.jpg; *.bmp) | *.png; *.jpg; *.bmp"
            };

            if (ofd.ShowDialog() == true)
            {
                img1.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void TgbImagem_OnClick(object sender, RoutedEventArgs e)
        {
            if (tgbImagem.IsChecked == true)
            {
                img1.IsEnabled = true;
                img1.Visibility = Visibility.Visible;
            }
            else
            {
                img1.IsEnabled = false;
                img1.Visibility = Visibility.Hidden;
            }
        }
    }
}

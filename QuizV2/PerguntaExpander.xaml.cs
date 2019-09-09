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

namespace QuizV2
{
    /// <summary>
    /// Interação lógica para PerguntaExpander.xam
    /// </summary>
    public partial class PerguntaExpander : UserControl
    {
        public PerguntaExpander(Pergunta pergunta)
        {
            InitializeComponent();

			expPergunta.Header = pergunta.Texto;
            txtRespostaA.Text = pergunta.Respostas[0];
            txtRespostaB.Text = pergunta.Respostas[1];
            txtRespostaC.Text = pergunta.Respostas[2];
            txtRespostaD.Text = pergunta.Respostas[3];

            rdbRespostaA.IsChecked = pergunta.Correta == pergunta.Respostas[0];
            rdbRespostaB.IsChecked = pergunta.Correta == pergunta.Respostas[1];
            rdbRespostaC.IsChecked = pergunta.Correta == pergunta.Respostas[2];
            rdbRespostaD.IsChecked = pergunta.Correta == pergunta.Respostas[3];

            tgbDissertativa.IsChecked = pergunta.Dissertativa;
            tgbTopQuiz.IsChecked = pergunta.TopQuiz;

            img1.Source = Serializa.GetImageSourceFromImage(pergunta.Imagem);
            
        }

		private void BtnDeletar_Click(object sender, RoutedEventArgs e)
		{

		}

        private void ExpPergunta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

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
    /// Interação lógica para PerguntaExpander.xam
    /// </summary>
    public partial class PerguntaExpander : UserControl
    {
        public PerguntaExpander(Pergunta pergunta)
        {
            InitializeComponent();

			expPergunta.Header = pergunta.Texto;
        }

		private void BtnDeletar_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}

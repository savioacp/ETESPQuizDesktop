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
    /// Interaction logic for QuestionControl.xaml
    /// </summary>
    public partial class QuestionControl : UserControl
    {
        public Pergunta question;
        public QuestionControl()
        {
            InitializeComponent();
        }
        public void SetQuestion(Pergunta question)
        {
            this.question = question;
            expPrincipal.Header = question.Texto;
            tgbDissertativa.IsChecked = question.Dissertativa;
            if(!question.Dissertativa)
            {
                txtA.Text = question.Respostas[0] ?? "Resposta A";
                txtB.Text = question.Respostas[1] ?? "Resposta B";
                txtC.Text = question.Respostas[2] ?? "Resposta C";
                txtD.Text = question.Respostas[3] ?? "Resposta D";

                rdbA.IsChecked = question.Correta == "A";
                rdbB.IsChecked = question.Correta == "B";
                rdbC.IsChecked = question.Correta == "C";
                rdbD.IsChecked = question.Correta == "D";
            }
            else
            {
                txtA.IsEnabled = false;
                txtB.IsEnabled = false;
                txtC.IsEnabled = false;
                txtD.IsEnabled = false;
            }
        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
	/// Interação lógica para JogoPage.xam
	/// </summary>
	public partial class JogoPage : UserControl
	{


		public event RoutedEventHandler Click;


		public JogoPage()
		{
			InitializeComponent();
			btn1.Click += new RoutedEventHandler((a, b) => Click?.Invoke(a,b));

		}
	}
}

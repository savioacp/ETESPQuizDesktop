using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuizV2.Util
{
	public static class GridHelper
	{
		public static void SetGrid(this UIElement element, int row, int col)
		{
			Grid.SetColumn(element, col);
			Grid.SetRow(element, row);
		}
	}
}

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

namespace TimeCalculator
{
	public partial class MainWindow : Window
	{
		int nextRow;

		public MainWindow()
		{
			InitializeComponent();

			timeGrid.ColumnDefinitions.Add(new ColumnDefinition());
			timeGrid.ColumnDefinitions.Add(new ColumnDefinition());
			timeGrid.RowDefinitions.Add(new RowDefinition());

			var text = new Label();
			text.Content = "Start";
			text.Background = new SolidColorBrush(Colors.Gray);
			Grid.SetRow(text, nextRow);
			Grid.SetColumn(text, 0);
			timeGrid.Children.Add(text);

			text = new Label();
			text.Content = "End";
			text.Background = new SolidColorBrush(Colors.Gray);
			Grid.SetRow(text, nextRow);
			Grid.SetColumn(text, 1);
			timeGrid.Children.Add(text);

			++nextRow;
		}

		private void Calculate_Click(object sender, RoutedEventArgs e)
		{
			TimeSpan totalTimeSpan = TimeSpan.Zero;
			for (int r = 1; r != timeGrid.RowDefinitions.Count; ++r)
			{
				string start = null, end = null;
				foreach (UIElement childElement in timeGrid.Children)
				{
					var child = childElement as TextBox;
					if (child == null) continue;

					if (Grid.GetRow(child) == r && Grid.GetColumn(child) == 0) start = child.Text;
					if (Grid.GetRow(child) == r && Grid.GetColumn(child) == 1) end = child.Text;
				}

				TimeSpan startTime, endTime;
				if (TimeSpan.TryParse(start, out startTime) && TimeSpan.TryParse(end, out endTime))
				{
					totalTimeSpan += (endTime - startTime);
				}
				else
				{
					totalTime.Content = "Total Time: ERROR on line: " + r;
					return;
				}
			}

			totalTime.Content = "Total Time: " + totalTimeSpan.ToString();
		}

		private void AddRow_Click(object sender, RoutedEventArgs e)
		{
			timeGrid.RowDefinitions.Add(new RowDefinition());

			var text = new TextBox();
			text.Text = "00:00";
			Grid.SetRow(text, nextRow);
			Grid.SetColumn(text, 0);
			timeGrid.Children.Add(text);

			text = new TextBox();
			text.Text = "00:00";
			Grid.SetRow(text, nextRow);
			Grid.SetColumn(text, 1);
			timeGrid.Children.Add(text);

			++nextRow;
		}

		private void RemoveRow_Click(object sender, RoutedEventArgs e)
		{
			if (timeGrid.RowDefinitions.Count <= 1) return;

			UIElement[] childeren = new UIElement[timeGrid.Children.Count];
			timeGrid.Children.CopyTo(childeren, 0);
			foreach (var child in childeren)
			{
				if (Grid.GetRow(child) == nextRow-1) timeGrid.Children.Remove(child);
			}

			timeGrid.RowDefinitions.RemoveAt(nextRow-1);
			--nextRow;
		}

		private void simpleConvert_Click(object sender, RoutedEventArgs e)
		{
			var values = simpleConvertTime.Text.Split(new char[]{'*'});
			string timeValue = (values.Length >= 1) ? values[0].Trim() : "00:00";
			decimal mulValue = 1;
			if (values.Length >= 2) decimal.TryParse(values[1].Trim(), out mulValue);

			TimeSpan time;
			if (TimeSpan.TryParse(timeValue, out time))
			{
				simpleConvertDecimal.Content = (time.Hours + (time.Minutes / 60m)) * mulValue;
			}
		}
	}
}

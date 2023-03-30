using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using FontFamily = System.Windows.Media.FontFamily;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace TextReader;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ReadFile();
    }

    private async void ReadFile()
    {
        Microsoft.Win32.OpenFileDialog dialog = new();
        dialog.ShowDialog();
        MainTextBlock.Text = await File.ReadAllTextAsync(dialog.FileName);
    }

    private void OnMouseLeave(object sender, MouseEventArgs e) => MainTextBlock.Visibility = Visibility.Hidden;

    private void OnMouseEnter(object sender, MouseEventArgs e) => MainTextBlock.Visibility = Visibility.Visible;

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

    private void OnMouseWheel(object sender, MouseWheelEventArgs e) => MainTextBlock.ScrollToVerticalOffset(MainTextBlock.VerticalOffset - e.Delta);

    private void OnSetColorClick(object sender, RoutedEventArgs e)
    {
        ColorDialog dialog = new()
        {
            FullOpen = true
        };
        dialog.ShowDialog();
        MainTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B));
    }

    private void OnSetFontClick(object sender, RoutedEventArgs e)
    {
        FontDialog dialog = new()
        {
            ShowEffects = false
        };
        dialog.ShowDialog();
        MainTextBlock.FontSize = dialog.Font.Size;
        if (dialog.Font.Style.HasFlag(System.Drawing.FontStyle.Regular))
        {
            MainTextBlock.FontStyle = FontStyles.Normal;
            MainTextBlock.FontWeight = FontWeights.Regular;
        }
        if (dialog.Font.Style.HasFlag(System.Drawing.FontStyle.Italic) || dialog.Font.Italic)
        {
            MainTextBlock.FontStyle = FontStyles.Italic;
            MainTextBlock.FontWeight = FontWeights.Regular;
        }
        if (dialog.Font.Style.HasFlag(System.Drawing.FontStyle.Bold) || dialog.Font.Bold)
        {
            MainTextBlock.FontStyle = FontStyles.Normal;
            MainTextBlock.FontWeight = FontWeights.Bold;
        }
        MainTextBlock.FontFamily = new FontFamily(dialog.Font.FontFamily.Name);
    }

    private void OnCloseClick(object sender, RoutedEventArgs e) => Close();

    private void OnLoadFileClick(object sender, RoutedEventArgs e) => ReadFile();
}
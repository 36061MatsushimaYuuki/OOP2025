using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercise1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e) {
        var fileDialog = new OpenFileDialog();
        Status_Label.Content = "";
        if(fileDialog.ShowDialog() == true) {
            Loaded_Block.Children.Clear();
            FileOpen_Button.IsEnabled = false;
            Close_Button.IsEnabled = false;
            int totalLines = File.ReadLines(fileDialog.FileName).Count();
            Loaded_ProgressBar.Visibility = Visibility.Visible;
            Loaded_ProgressBar.Minimum = 0;
            Loaded_ProgressBar.Maximum = totalLines;
            Loaded_ProgressBar.Value = 0;
            using (var stream = new StreamReader(fileDialog.FileName, Encoding.UTF8)) {
                string? line;
                int count = 1;
                while ((line = await stream.ReadLineAsync()) != null) {
                    TextBlock addTextBlock = new TextBlock {
                        TextWrapping = TextWrapping.Wrap,
                        Text = line,
                    };
                    Loaded_Block.Children.Add(addTextBlock);
                    await Dispatcher.InvokeAsync(() => {
                        Status_Label.Content = $"{count}/{totalLines}行読み込み中…";
                        Loaded_ProgressBar.Value = count;
                    });
                    count++;
                    await Task.Delay(1);
                }
                Status_Label.Content = "読み込み完了";
                Loaded_ProgressBar.Visibility = Visibility.Hidden;
                FileOpen_Button.IsEnabled = true;
                Close_Button.IsEnabled = true;
            }
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) {
        Status_Label.Content = "";
        Loaded_Block.Children.Clear();
    }
}
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebBrowser;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) {
        Check_BackForward();
    }

    private void Back_Button_Click(object sender, RoutedEventArgs e) {
        WebView.GoBack();
    }

    private void Forward_Button_Click(object sender, RoutedEventArgs e) {
        WebView.GoForward();
    }

    private void Go_Button_Click(object sender, RoutedEventArgs e) {
        var url = AddressBar.Text;
        if(url == "") {
            return;
        } 
        WebView.Source = new Uri(url);
    }

    private void Check_BackForward() {
        Back_Button.IsEnabled = WebView.CanGoBack;
        Forward_Button.IsEnabled = WebView.CanGoForward;
    }

    private async void WebView_ContentLoading(object sender, Microsoft.Web.WebView2.Core.CoreWebView2ContentLoadingEventArgs e) {
        await Task.Delay(20);
        AddressBar.Text = WebView.Source.AbsoluteUri;
        Check_BackForward();
    }
}
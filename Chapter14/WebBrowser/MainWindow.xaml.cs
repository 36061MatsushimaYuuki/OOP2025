using Microsoft.Web.WebView2.Core;
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
        InitializeAsync();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) {
        Check_BackForward();
    }

    private async void InitializeAsync() {
        await WebView.EnsureCoreWebView2Async();

        WebView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
        WebView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
    }

    //読み込み開始したらプログレスバー表示
    private void CoreWebView2_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e) {
        Loading_ProgressBar.Visibility = Visibility.Visible;
        Loading_ProgressBar.IsIndeterminate = true;
    }

    //読み込み完了したらプログレスバー非表示
    private void CoreWebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e) {
        Loading_ProgressBar.Visibility = Visibility.Collapsed;
        Loading_ProgressBar.IsIndeterminate = false;
    }

    private void Back_Button_Click(object sender, RoutedEventArgs e) {
        WebView.GoBack();
    }

    private void Forward_Button_Click(object sender, RoutedEventArgs e) {
        WebView.GoForward();
    }

    private void Go_Button_Click(object sender, RoutedEventArgs e) {
        var url = AddressBar.Text.Trim();
        if(string.IsNullOrWhiteSpace(url)) {
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
using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.IO;
using System.Net;
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

namespace CustomerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private List<Customer> _customers = new List<Customer>();
    private int _count = 0;

    public MainWindow() {
        InitializeComponent();
        Read_Database();
    }

    private void Read_Database() {
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            _customers = connection.Table<Customer>().ToList();
        }
        Customer_ListView.ItemsSource = _customers;
        _count = _customers.Count();
        List_Counter.Text = "全 " + _count + "/" + _count + " 件";
        Status_Text.Text = "";
    }

    private void Reset_InputItems() {
        Name_TextBox.Text = "";
        Phone_TextBox.Text = "";
        Address_TextBox.Text = "";
        Picture_Image.Source = null;
        Search_TextBox.Text = "";
    }

    private void OpenPicture_Button_Click(object sender, RoutedEventArgs e) {
        var fileDialog = new OpenFileDialog();
        fileDialog.Filter = "画像ファイル (*.jpg;.jpeg;*.png;*.webp)|*.jpg;.jpeg;*.png;*.webp";
        if (fileDialog.ShowDialog() == true) { // ?? false だとエラーが出るので廃止
            Uri imageUri = new Uri(fileDialog.FileName);
            Picture_Image.Source = new BitmapImage(imageUri);
        }
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e) {
        if(Name_TextBox.Text == "" || Phone_TextBox.Text == "" || Address_TextBox.Text == "") {
            Status_Text.Text = "入力項目が不十分です";
            return;
        }

        var customer = new Customer() {
            Name = Name_TextBox.Text,
            Phone = Phone_TextBox.Text,
            Address = Address_TextBox.Text,
            Picture = ImageToByteArray(Picture_Image.Source),
        };

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Insert(customer);
            Read_Database();
        }
        Reset_InputItems();
        Status_Text.Text = "データを登録しました";
    }

    private void Update_Button_Click(object sender, RoutedEventArgs e) {
        var selectedCustomer = Customer_ListView.SelectedItem as Customer;
        if (selectedCustomer == null) {
            //未選択時なし
            Status_Text.Text = "データが選択されていません";
            return;
        }

        if (MessageBox.Show("本当にデータを更新しますか？", "顧客管理アプリケーション", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.No) {
            return;
        }

        selectedCustomer.Name = Name_TextBox.Text;
        selectedCustomer.Phone = Phone_TextBox.Text;
        selectedCustomer.Address = Address_TextBox.Text;
        selectedCustomer.Picture = ImageToByteArray(Picture_Image.Source);

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Update(selectedCustomer);
            Read_Database();
        }
        Reset_InputItems();
        Status_Text.Text = "データを更新しました";
    }

    private void Delete_Button_Click(object sender, RoutedEventArgs e) {
        var selectedCustomer = Customer_ListView.SelectedItem as Customer;
        if (selectedCustomer == null) {
            //未選択時なし
            Status_Text.Text = "データが選択されていません";
            return;
        }

        if(MessageBox.Show("本当にデータを削除しますか？", "顧客管理アプリケーション", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.No) {
            return;
        }

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Delete(selectedCustomer);
            Read_Database();
        }
        Reset_InputItems();
        Status_Text.Text = "データを削除しました";
    }

    private void Customer_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selectedCustomer = Customer_ListView.SelectedItem as Customer;
        if (selectedCustomer == null) {
            //未選択時なし
            Update_Button.IsEnabled = false;
            Delete_Button.IsEnabled = false;
            return;
        }

        Name_TextBox.Text = selectedCustomer.Name;
        Phone_TextBox.Text = selectedCustomer.Phone;
        Address_TextBox.Text = selectedCustomer.Address;
        Picture_Image.Source = ByteArrayToImage(selectedCustomer.Picture);
        Status_Text.Text = "";
        Update_Button.IsEnabled = true;
        Delete_Button.IsEnabled = true;
    }

    private void Search_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
        var filterList = _customers.Where(x => x.Name.Contains(Search_TextBox.Text));
        if(filterList.Count() <= 0) {
            filterList = _customers.Where(x => x.Phone.Contains(Search_TextBox.Text));
        }
        if (filterList.Count() <= 0) {
            filterList = _customers.Where(x => x.Address.Contains(Search_TextBox.Text));
        }

        Customer_ListView.ItemsSource = filterList;
        List_Counter.Text = "全 " + filterList.Count() + "/" + _count + " 件";
        Status_Text.Text = "";
    }

    private void Input_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
        Status_Text.Text = "";
        if (Name_TextBox.Text == "" || Phone_TextBox.Text == "" || Address_TextBox.Text == "") {
            Save_Button.IsEnabled = false;
            return;
        }
        Save_Button.IsEnabled = true;
    }

    public byte[]? ImageToByteArray(ImageSource? image) {
        if (image == null) {
            return null;
        }

        using (var ms = new MemoryStream()) {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
            encoder.Save(ms);
            return ms.ToArray();
        }
    }

    public ImageSource? ByteArrayToImage(byte[]? bytes) {
        if (bytes == null) {
            return null;
        }

        using (var ms = new MemoryStream(bytes)) {
            var bi = new BitmapImage();

            // MemoryStreamを書き込むために準備する
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.CreateOptions = BitmapCreateOptions.None;

            // MemoryStreamを書き込む
            bi.StreamSource = ms;
            bi.EndInit();
            bi.Freeze();

            return bi;
        }
    }
}
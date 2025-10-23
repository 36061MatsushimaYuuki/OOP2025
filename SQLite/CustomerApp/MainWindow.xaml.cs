﻿using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        PostCode_TextBox.Text = "";
        Address_TextBox.Text = "";
        Picture_Image.Source = null;
        Search_TextBox.Text = "";
    }

    private void OpenPicture_Button_Click(object sender, RoutedEventArgs e) {
        var fileDialog = new OpenFileDialog();
        fileDialog.Filter = "画像ファイル (*.jpg;*.jpeg;*.png;*.webp)|*.jpg;*.jpeg;*.png;*.webp|すべてのファイル (*.*)|*.*";
        if (fileDialog.ShowDialog() == true) { // ?? false だとエラーが出るので廃止
            Uri imageUri = new Uri(fileDialog.FileName);
            Picture_Image.Source = new BitmapImage(imageUri);
        }
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e) {
        if(Name_TextBox.Text == "" || Phone_TextBox.Text == "" || PostCode_TextBox.Text == "" || Address_TextBox.Text == "") {
            Status_Text.Text = "入力項目が不十分です";
            return;
        }

        if (!PostCode_TextBox.Text.Contains("-") && PostCode_TextBox.Text.Length == 7 && int.TryParse(PostCode_TextBox.Text, out _)) {
            string formatted = $"{PostCode_TextBox.Text.Substring(0, 3)}-{PostCode_TextBox.Text.Substring(3, 4)}";
            PostCode_TextBox.Text = formatted;
        }

        var customer = new Customer() {
            Name = Name_TextBox.Text,
            Phone = Phone_TextBox.Text,
            PostCode = PostCode_TextBox.Text,
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

        if (!PostCode_TextBox.Text.Contains("-") && PostCode_TextBox.Text.Length == 7 && int.TryParse(PostCode_TextBox.Text, out _)) {
            string formatted = $"{PostCode_TextBox.Text.Substring(0, 3)}-{PostCode_TextBox.Text.Substring(3, 4)}";
            PostCode_TextBox.Text = formatted;
        }

        selectedCustomer.Name = Name_TextBox.Text;
        selectedCustomer.Phone = Phone_TextBox.Text;
        selectedCustomer.PostCode = PostCode_TextBox.Text;
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
        PostCode_TextBox.Text = selectedCustomer.PostCode;
        Address_TextBox.Text = selectedCustomer.Address;
        Picture_Image.Source = ByteArrayToImage(selectedCustomer.Picture);
        Status_Text.Text = "";
        if(!PostCode_Search_Button.IsEnabled) {
            Update_Button.IsEnabled = false;
        } else {
            Update_Button.IsEnabled = true;
        }
        Delete_Button.IsEnabled = true;
    }

    private void Search_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
        var nameFilter = _customers.Where(x => x.Name.Contains(Search_TextBox.Text));
        var postCodeFilter = _customers.Where(x => x.PostCode.Contains(Search_TextBox.Text));
        var addressFilter = _customers.Where(x => x.Address.Contains(Search_TextBox.Text));
        var phoneFilter = _customers.Where(x => x.Phone.Contains(Search_TextBox.Text));

        var filterList = nameFilter.Union(postCodeFilter).Union(addressFilter).Union(phoneFilter);

        Customer_ListView.ItemsSource = filterList;
        List_Counter.Text = "全 " + filterList.Count() + "/" + _count + " 件";
        Status_Text.Text = "";
    }

    private void Input_TextBox_TextChanged(object sender, TextChangedEventArgs e) {
        Status_Text.Text = "";
        if(PostCode_TextBox.Text.Length < 8) {
            if(PostCode_TextBox.Text.Length == 7 && !PostCode_TextBox.Text.Contains("-") && int.TryParse(PostCode_TextBox.Text, out _)) {
                PostCode_Search_Button.IsEnabled = true;
            } else {
                PostCode_Search_Button.IsEnabled = false;
            }
        } else {
            if(PostCode_TextBox.Text.Length == 8 && PostCode_TextBox.Text[3] == '-') {
                PostCode_Search_Button.IsEnabled = true;
            } else {
                PostCode_Search_Button.IsEnabled = false;
            }
        }
        if (Name_TextBox.Text == "" || Phone_TextBox.Text == "" || PostCode_TextBox.Text == "" || Address_TextBox.Text == "" || !PostCode_Search_Button.IsEnabled) {
            Save_Button.IsEnabled = false;
            Update_Button.IsEnabled = false;
            return;
        }
        Save_Button.IsEnabled = true;
        var selectedCustomer = Customer_ListView.SelectedItem as Customer;
        if (selectedCustomer != null) {
            Update_Button.IsEnabled = true;
            return;
        }
    }

    private async void PostCode_Search_Button_Click(object sender, RoutedEventArgs e) {
        Status_Text.Text = "";
        var postCode = PostCode_TextBox.Text.Replace("-", "");
        var url = "https://jp-postal-code-api.ttskch.com/api/v1/" + postCode + ".json";

        using(HttpClient client = new HttpClient()) {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                // 404 の場合の処理（例：エラーメッセージを表示）
                Status_Text.Text = "住所が見つかりませんでした";
                return;
            }

            if (!response.IsSuccessStatusCode) {
                // その他のエラー（例：500など）に対する処理
                Status_Text.Text = $"エラーが発生しました: {response.StatusCode}";
                return;
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<PostCode>(responseBody);
            if (data == null || data.addresses == null || data.addresses.Count == 0) {
                Status_Text.Text = "住所情報が取得できませんでした";
                return;
            }

            var ja = data.addresses[0].ja;
            Address_TextBox.Text = $"{ja.prefecture}{ja.address1}{ja.address2}";
            Status_Text.Text = "住所情報を取得しました";
        }
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
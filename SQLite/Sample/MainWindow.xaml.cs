using Sample.Data;
using SQLite;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private List<Person> _persons = new List<Person>();

    public MainWindow() {
        InitializeComponent();
        ReadDatabase();
    }

    private void ReadDatabase() {
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Person>();
            _persons = connection.Table<Person>().ToList();
        }
        PersonListView.ItemsSource = _persons;
    }

    private void ResetInputTextBox() {
        NameTextBox.Text = "";
        PhoneTextBox.Text = "";
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        if(NameTextBox.Text == "" || PhoneTextBox.Text == "") {
            //空欄だったら登録不可
            return;
        }

        var person = new Person() {
            Name = NameTextBox.Text,
            Phone = PhoneTextBox.Text,
        };

        using(var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Person>();
            connection.Insert(person);
            ReadDatabase();
        }
        //登録後空欄
        ResetInputTextBox();
    }

    private void ReadButton_Click(object sender, RoutedEventArgs e) {
        //正直不要
        ReadDatabase();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        var item = PersonListView.SelectedItem as Person;
        if(item == null) {
            //未選択時削除不可
            return;
        }

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Person>();
            connection.Delete(item);
            ReadDatabase();
        }
    }

    private void PersonListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selectedPerson = PersonListView.SelectedItem as Person;
        if (selectedPerson == null) {
            //未選択時なし
            return;
        }

        NameTextBox.Text = selectedPerson.Name;
        PhoneTextBox.Text = selectedPerson.Phone;
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e) {
        var item = PersonListView.SelectedItem as Person;
        if (item == null) {
            //未選択時なし
            return;
        }

        item.Name = NameTextBox.Text;
        item.Phone = PhoneTextBox.Text;

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Person>();
            connection.Update(item);
            ReadDatabase();
        }
        ResetInputTextBox();
    }

    //リストビューのフィルタリング
    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
        var filterList = _persons.Where(x => x.Name.Contains(SearchTextBox.Text));

        PersonListView.ItemsSource = filterList;
    }
}
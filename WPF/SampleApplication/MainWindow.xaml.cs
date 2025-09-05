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

namespace SampleApplication
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void checkBox_IsChecked(object sender, RoutedEventArgs e) {
            checkBoxTextBlock.Text = (bool)((CheckBox)sender).IsChecked ?
                "チェック済み" /* true */ : "未チェック" /* false */;
        }

        private void colorRadioButton_Checked(object sender, RoutedEventArgs e) {
            colorTextBlock.Text = (string)((RadioButton)sender).Content;
        }

        private void seasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            seasonTextBlock.Text = (string)((ComboBoxItem)(seasonComboBox.SelectedItem)).Content;
        }
    }
}

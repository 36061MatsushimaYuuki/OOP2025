using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace ColorChecker
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = GetColorList();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            colorArea.Background = new SolidColorBrush(
                Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value)
            );
        }

        private void Stock_Button_Click(object sender, RoutedEventArgs e) {
            var r = (byte)RedSlider.Value;
            var g = (byte)GreenSlider.Value;
            var b = (byte)BlueSlider.Value;
            var thisColor = Color.FromRgb(r, g, b);
            //comboBoxが選択されていない状態でも調べる
            var equalItem = ColorSelect_ComboBox.Items.Cast<MyColor>().FirstOrDefault(c => c.Color == thisColor);
            string colorName = null;
            if(equalItem != null) {
                colorName = equalItem.Name;
            }
            Stock_List.Items.Add(new MyColor {
                Color = thisColor,
                Name = colorName ?? "R: " + r + " G: " + g + " B: " + b
            });
        }

        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        private void ColorSelect_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(ColorSelect_ComboBox.SelectedItem == null) {
                return;
            }
            var selectedItem = (MyColor)ColorSelect_ComboBox.SelectedItem;
            colorArea.Background = new SolidColorBrush(
                selectedItem.Color
            );
            Set_SliderValue(selectedItem.Color);
        }

        private void Stock_List_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(Stock_List.SelectedItem == null) {
                return;
            }
            var selectedItem = (MyColor)Stock_List.SelectedItem;
            colorArea.Background = new SolidColorBrush(
                selectedItem.Color
            );
            Set_SliderValue(selectedItem.Color);
            //セレクト状態をリセット
            Stock_List.SelectedIndex = -1;
        }

        private void Set_SliderValue(Color color) {
            RedSlider.Value = color.R;
            GreenSlider.Value = color.G;
            BlueSlider.Value = color.B;
        }
    }
}

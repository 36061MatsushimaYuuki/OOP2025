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

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            DataContext = GetColorList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            ColorSelect_ComboBox.SelectedIndex = 7;
        }

        Color _saveColor = Color.FromRgb(0, 0, 0);
        double _saveLightValue = 0.0;
        bool isLightMode = false;

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            var r = (byte)RedSlider.Value;
            var g = (byte)GreenSlider.Value;
            var b = (byte)BlueSlider.Value;
            var thisColor = Color.FromRgb(r, g, b);
            colorArea.Background = new SolidColorBrush(
                thisColor
            );
            var equalItem = ColorSelect_ComboBox.Items.Cast<MyColor>().FirstOrDefault(c => c.Color == thisColor);
            var index = ColorSelect_ComboBox.Items.IndexOf(equalItem);
            ColorSelect_ComboBox.SelectedIndex = index;
            ColorCode_TextBox.Text = String.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
            RGBCode_TextBox.Text = r + ", " + g + ", " + b;
            if(!isLightMode || (!isLightMode && ColorSelect_ComboBox.SelectedIndex < 0)) {
                SetLightAndSaveColor(thisColor);
            }
        }

        private void Slider_GotFocus(object sender, RoutedEventArgs e) {
            isLightMode = false;
        }

        private void LightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if(!isLightMode || _saveLightValue <= 0) {
                return;
            }
            RedSlider.Value = Math.Round(_saveColor.R * (LightSlider.Value / _saveLightValue));
            GreenSlider.Value = Math.Round(_saveColor.G * (LightSlider.Value / _saveLightValue));
            BlueSlider.Value = Math.Round(_saveColor.B * (LightSlider.Value / _saveLightValue));
        }

        private void LightSlider_GotFocus(object sender, RoutedEventArgs e) {
            isLightMode = true;
        }

        private void SetLightAndSaveColor(Color color) {
            var r = color.R;
            var g = color.G;
            var b = color.B;
            _saveColor = color;
            LightSlider.Value = Math.Max(Math.Max(r, g), b);
            _saveLightValue = LightSlider.Value;
        }

        private void Stock_Button_Click(object sender, RoutedEventArgs e) {
            var r = (byte)RedSlider.Value;
            var g = (byte)GreenSlider.Value;
            var b = (byte)BlueSlider.Value;
            var thisColor = Color.FromRgb(r, g, b);
            //comboBoxが選択されていない状態でも調べる
            var equalItem = ColorSelect_ComboBox.Items.Cast<MyColor>().FirstOrDefault(c => c.Color == thisColor);
            string colorName = null;
            if (equalItem != null) {
                colorName = equalItem.Name;
            }
            var matched = Stock_List.Items.Cast<MyColor>().FirstOrDefault(c => c.Color == thisColor);
            if(matched != null) {
                MessageBox.Show("既にこの色は登録されています", "ColorChecker", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
            if (ColorSelect_ComboBox.SelectedItem == null) {
                return;
            }
            var selectedItem = (MyColor)ColorSelect_ComboBox.SelectedItem;
            colorArea.Background = new SolidColorBrush(
                selectedItem.Color
            );
            if (!isLightMode) {
                Set_SliderValue(selectedItem.Color, true);
            }
        }

        private void ColorSelect_ComboBox_GotFocus(object sender, RoutedEventArgs e) {
            isLightMode = false;
        }

        private void Stock_List_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //Stock_ToColorLabel_Button_Click に移動
        }

        private void Set_SliderValue(Color color, bool isSetLight) {
            var r = color.R;
            var g = color.G;
            var b = color.B;
            RedSlider.Value = r;
            GreenSlider.Value = g;
            BlueSlider.Value = b;
            if(isSetLight) {
                SetLightAndSaveColor(color);
            }
        }

        private void Stock_Delete_Button_Click(object sender, RoutedEventArgs e) {
            if(Stock_List.SelectedItem == null) {
                return;
            }
            var selectedItem = (MyColor)Stock_List.SelectedItem;
            Stock_List.Items.Remove(selectedItem);
        }

        private void Stock_ToColorLabel_Button_Click(object sender, RoutedEventArgs e) {
            if (Stock_List.SelectedItem == null) {
                return;
            }
            var selectedItem = (MyColor)Stock_List.SelectedItem;
            colorArea.Background = new SolidColorBrush(
                selectedItem.Color
            );
            isLightMode = false;
            Set_SliderValue(selectedItem.Color, false);
        }

        /* Stockの位置移動機能を実装 */
        private void Stock_MoveUp_Button_Click(object sender, RoutedEventArgs e) {
            if (Stock_List.SelectedItem == null || Stock_List.SelectedIndex <= 0) {
                return;
            }
            var selectedItem = Stock_List.SelectedItem;
            var index = Stock_List.SelectedIndex;
            Stock_List.Items[index] = Stock_List.Items[index - 1];
            Stock_List.Items[index - 1] = selectedItem;
            //移動先indexを選択しておくことでスムーズに
            Stock_List.SelectedIndex = index - 1;
        }

        private void Stock_MoveDown_Button_Click(object sender, RoutedEventArgs e) {
            if (Stock_List.SelectedItem == null || Stock_List.SelectedIndex >= Stock_List.Items.Count - 1) {
                return;
            }
            var selectedItem = Stock_List.SelectedItem;
            var index = Stock_List.SelectedIndex;
            Stock_List.Items[index] = Stock_List.Items[index + 1];
            Stock_List.Items[index + 1] = selectedItem;
            //移動先indexを選択しておくことでスムーズに
            Stock_List.SelectedIndex = index + 1;
        }

        private void Overwrite_Button_Click(object sender, RoutedEventArgs e) {
            if (Stock_List.SelectedItem == null) {
                MessageBox.Show("項目が選択されていません", "ColorChecker", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var index = Stock_List.SelectedIndex;
            var r = (byte)RedSlider.Value;
            var g = (byte)GreenSlider.Value;
            var b = (byte)BlueSlider.Value;
            var thisColor = Color.FromRgb(r, g, b);
            //comboBoxが選択されていない状態でも調べる
            var equalItem = ColorSelect_ComboBox.Items.Cast<MyColor>().FirstOrDefault(c => c.Color == thisColor);
            string colorName = null;
            if (equalItem != null) {
                colorName = equalItem.Name;
            }
            var matched = Stock_List.Items.Cast<MyColor>().FirstOrDefault(c => c.Color == thisColor);
            if (matched != null) {
                MessageBox.Show("既にこの色は登録されています", "ColorChecker", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Stock_List.Items[index] = new MyColor {
                Color = thisColor,
                Name = colorName ?? "R: " + r + " G: " + g + " B: " + b
            };
            Stock_List.SelectedIndex = index;
        }
    }
}

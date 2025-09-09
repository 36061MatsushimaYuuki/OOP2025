﻿using System;
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
            Set_SliderValue(selectedItem.Color);
        }

        private void Stock_List_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //Stock_ToColorLabel_Button_Click に移動
        }

        private void Set_SliderValue(Color color) {
            RedSlider.Value = color.R;
            GreenSlider.Value = color.G;
            BlueSlider.Value = color.B;
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
            Set_SliderValue(selectedItem.Color);
        }
    }
}

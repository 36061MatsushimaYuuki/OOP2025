using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace TenkiApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public double Lat = 35;
        public double Lng = 139;
        public string Country = "";
        public string State = "";
        public string City = "";

        public MainWindow() {
            InitializeComponent();
            InitWebView();
        }

        private async void InitWebView() {
            await MapViewer.EnsureCoreWebView2Async();

            string html = @"
            <!DOCTYPE html>
            <html>
            <head>
              <meta charset='utf-8'>
              <title>Leaflet Map Click</title>
              <link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css'/>
              <script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>
            </head>
            <body style='margin:0'>
              <div id='map' style='width:100%;height:100vh;'></div>
              <script>
                var map = L.map('map').setView([35.6895, 139.6917], 8);
                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                  attribution: '© OpenStreetMap contributors'
                }).addTo(map);

                var currentMarker = null;

                // 左クリックでピンを追加
                map.on('click', async function(e) {
                  var lat = e.latlng.lat;
                  var lng = e.latlng.lng;

                  // 既存のピンを消す
                  if (currentMarker) {
                    map.removeLayer(currentMarker);
                  }

                  // 新しいピンを追加
                  currentMarker = L.marker([lat, lng]).addTo(map);

                  // 右クリックでピンを削除
                  currentMarker.on('contextmenu', function() {
                    map.removeLayer(currentMarker);
                    currentMarker = null;
                  });

                  // Nominatim APIで逆ジオコーディング
                  let url = `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lng}&format=json&accept-language=ja`;
                  let response = await fetch(url);
                  let data = await response.json();
                  let a = data.address || {};

                  // 国
                  let country = a.country || '';

                  // 都道府県候補をフォールバック
                  let state = a.state || a.province || a.region || a.county || '';

                  // 市区町村候補をフォールバック
                  let city = a.city || a.town || a.village || a.municipality || a.suburb || '';

                  // C# 側にJSONで送信（lat/lngは数値のまま）
                  window.chrome.webview.postMessage(JSON.stringify({ lat, lng, country, state, city }));
                });
              </script>
            </body>
            </html>";

            MapViewer.NavigateToString(html);

            // C# 側で受け取る
            MapViewer.CoreWebView2.WebMessageReceived += (s, e) => {
                LocationBlock.Text = "場所名: ";

                WeatherPanel.Children.Clear();

                var json = e.TryGetWebMessageAsString();
                var msg = System.Text.Json.JsonSerializer.Deserialize<Location>(json);

                Lat = msg.lat;
                Lng = msg.lng;
                Country = msg.country ?? "";
                State = msg.state ?? "";
                City = msg.city ?? "";

                WeatherMethod();
            };
        }

        private async void WeatherMethod() {
            await GetLocationWeather();
        }

        private async Task GetLocationWeather() {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={Lat}&longitude={Lng}&hourly=temperature_2m,wind_speed_10m,weather_code,relative_humidity_2m&models=jma_seamless&timezone=Asia%2FTokyo";

            using var http = new HttpClient();

            try {
                // JSON デシリアライズ
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = await http.GetFromJsonAsync<WeatherList>(url, options);

                if (data?.hourly != null) {
                    for (int i = 0; i < data.hourly.time.Count(); i++) {
                        var border = new Border {
                            BorderThickness = new Thickness(1),
                            BorderBrush = System.Windows.Media.Brushes.Gray,
                            Margin = new Thickness(5),
                            Padding = new Thickness(5)
                        };

                        var stack = new StackPanel();
                        stack.Children.Add(new TextBlock { Text = data.hourly.time[i] });
                        stack.Children.Add(new TextBlock { Text = $"{data.hourly.temperature_2m[i]} ℃" });

                        border.Child = stack;
                        WeatherPanel.Children.Add(border);
                    }
                    LocationBlock.Text = $"場所名: {Country} {State} {City}";
                } else {
                    Console.WriteLine("データが取得できませんでした。");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"エラー：{ex.Message}");
            }
        }
    }
}
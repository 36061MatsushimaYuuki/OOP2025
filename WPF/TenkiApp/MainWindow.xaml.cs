using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
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
using System.Windows.Threading;
using static System.Net.WebRequestMethods;

namespace TenkiApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public double Lat;
        public double Lng;
        public string Country = "";
        public string State = "";
        public string City = "";
        private DispatcherTimer timer;
        private bool isFetchingWeather = false;

        public MainWindow() {
            InitializeComponent();
            InitTimer();
            Loaded += async (_, __) => {
                await InitLocationByIpAsync();  // 次にIP→住所を取得して、JSからpostMessageを発火
            };
        }

        private void InitTimer() {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            // 現在時刻を表示
            TimeBox.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void HandleLocationMessage(Location msg) {
            Lat = msg.lat;
            Lng = msg.lng;
            Country = msg.country ?? "";
            State = msg.state ?? "";
            City = msg.city ?? "";

            LocationBlock.Text = $"📍 場所名: {Country} {State} {City}";

            WeatherMethod();
        }

        private async Task InitWebView(double lat = 35.6895, double lng = 139.6917) {
            await MapViewer.EnsureCoreWebView2Async();

            string html = $@"
            <!DOCTYPE html>
            <html>
            <head>
              <meta charset='utf-8'>
              <title>Leaflet Map</title>
              <link rel='stylesheet' href='https://unpkg.com/leaflet/dist/leaflet.css'/>
              <script src='https://unpkg.com/leaflet/dist/leaflet.js'></script>
            </head>
            <body style='margin:0'>
              <div id='map' style='width:100%;height:100vh;'></div>
              <script>
                var map = L.map('map').setView([{lat}, {lng}], 10);
                L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
                  attribution: '© OpenStreetMap contributors'
                }}).addTo(map);

                var currentMarker = null;

                // ピンを置く共通関数（ピンが変わったときに必ず呼ばれる）
                async function setMarker(lat, lng) {{
                  if (currentMarker) {{
                    map.removeLayer(currentMarker);
                  }}
                  currentMarker = L.marker([lat, lng]).addTo(map);

                  currentMarker.on('contextmenu', function() {{
                    map.removeLayer(currentMarker);
                    currentMarker = null;
                  }});

                  // 逆ジオコーディング
                  let url = `https://nominatim.openstreetmap.org/reverse?lat=${{lat}}&lon=${{lng}}&format=json&accept-language=ja`;
                  let response = await fetch(url);
                  let data = await response.json();
                  let a = data.address || {{}};

                  let country = a.country || '';
                  let state = a.state || a.province || a.region || a.county || '';
                  let city = a.city || a.town || a.village || a.municipality || a.suburb || '';

                  // ピンが移動したときに必ず通知
                  window.chrome.webview.postMessage(JSON.stringify({{ lat, lng, country, state, city }}));
                }}

                // 初期表示時にもピンを置く（＝移動判定が走る）
                setMarker({lat}, {lng});

                // クリック時にもピンを置く（＝移動判定が走る）
                map.on('click', function(e) {{
                  setMarker(e.latlng.lat, e.latlng.lng);
                }});
              </script>
            </body>
            </html>";

            MapViewer.NavigateToString(html);

            MapViewer.CoreWebView2.WebMessageReceived += (s, e) => {
                var json = e.TryGetWebMessageAsString();
                var msg = System.Text.Json.JsonSerializer.Deserialize<Location>(json);
                HandleLocationMessage(msg);
            };
        }

        private async Task InitLocationByIpAsync() {
            using var client = new HttpClient();
            var json = await client.GetStringAsync("https://ipinfo.io/json");
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var loc = root.GetProperty("loc").GetString(); // "lat,lon" 形式
            var parts = loc.Split(',');
            Lat = double.Parse(parts[0]);
            Lng = double.Parse(parts[1]);
            // 地図を現在地に移動
            await InitWebView(Lat, Lng);
        }

        private async void WeatherMethod() {
            // 既にデータ取得中であれば、処理を中断
            if (isFetchingWeather) {
                return;
            }

            isFetchingWeather = true; // 処理開始フラグを立てる

            try {
                WeatherPanel.Children.Clear();
                WeatherScroll.ScrollToHorizontalOffset(0);
                LoadingOverlay.Visibility = Visibility.Visible;
                await GetLocationWeather();
            }
            catch (Exception ex) {
                Console.WriteLine($"WeatherMethodエラー：{ex.Message}");
            }
            finally {
                isFetchingWeather = false; // 処理終了後、フラグを解除
                LoadingOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async Task GetLocationWeather() {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={Lat}&longitude={Lng}&hourly=temperature_2m,wind_speed_10m,weather_code,relative_humidity_2m&models=jma_seamless&timezone=Asia%2FTokyo";

            using var http = new HttpClient();

            //1時間ごとのデータを日別に分けてグラフとして表示する機構に変更する
            try {
                // JSON デシリアライズ
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = await http.GetFromJsonAsync<WeatherList>(url, options);

                if (data?.hourly != null) {
                    var hourlyTimes = data.hourly.time.Select(t => DateTime.Parse(t)).ToList();
                    var temps = data.hourly.temperature_2m;
                    var codes = data.hourly.weather_code;

                    // 日ごとにグループ化
                    var grouped = hourlyTimes
                        .Select((dt, idx) => new { dt, temp = temps[idx], code = codes[idx] })
                        .GroupBy(x => x.dt.Date);

                    bool isFirstDay = true; // 最初の日のフラグ

                    foreach (var dayGroup in grouped) {
                        var plotModel = new PlotModel();

                        // ラベルを3時間ごとに、それ以外は空文字にする
                        var labels = dayGroup
                            .Select(x => x.dt.Hour % 3 == 0 ? x.dt.ToString("H:00") : "")
                            .ToList();

                        double maxTemp = dayGroup.Max(x => x.temp);
                        double minTemp = dayGroup.Min(x => x.temp);

                        var axis = new CategoryAxis {
                            Position = AxisPosition.Bottom,
                            Angle = 45,
                            TextColor = OxyColors.White, // ダークテーマに合わせて白に
                            TicklineColor = OxyColor.FromAColor(100, OxyColors.White), // 淡い白
                            MajorGridlineStyle = LineStyle.Solid,
                            MajorGridlineColor = OxyColor.FromAColor(50, OxyColors.White), // 薄い白のグリッド線
                            MinorTickSize = 0,
                            Maximum = dayGroup.Count() + 1.0
                        };
                        axis.Labels.AddRange(labels);
                        plotModel.Axes.Add(axis);

                        plotModel.Axes.Add(new LinearAxis {
                            Position = AxisPosition.Left,
                            TextColor = OxyColors.White, // ダークテーマに合わせて白に
                            TicklineColor = OxyColor.FromAColor(100, OxyColors.White),
                            MajorGridlineStyle = LineStyle.Solid,
                            MajorGridlineColor = OxyColor.FromAColor(50, OxyColors.White),
                            MinorGridlineStyle = LineStyle.None,
                            Title = "気温 (°C)", // タイトルに単位を追記
                            TitleColor = OxyColors.White,
                            Minimum = minTemp - 2,
                            Maximum = maxTemp + 2
                        });

                        // グラフの背景を完全に透明にし、コンテナの色（#304060）が透けるようにする
                        plotModel.Background = OxyColors.Transparent;
                        plotModel.PlotAreaBackground = OxyColors.Transparent;
                        plotModel.TextColor = OxyColors.DimGray;
                        plotModel.PlotAreaBorderColor = OxyColors.Transparent;

                        // 線の色を視認性の高い明るい色に変更
                        var seriesColor = OxyColor.FromRgb(255, 190, 0); // 明るいオレンジイエロー
                        var series = new LineSeries {
                            Color = seriesColor,
                            StrokeThickness = 3.0, // 線を太く
                            MarkerType = MarkerType.Circle,
                            MarkerSize = 5,
                            MarkerFill = seriesColor,
                            // トラッカーを無効化（マウス操作がないため不要）
                            TrackerFormatString = ""
                        };

                        // 現在時刻のデータポイントを特定するための変数
                        double targetX = -1;
                        double targetY = -1;

                        foreach (var item in dayGroup.Select((x, idx) => new { x, idx })) {
                            bool rainFlag = (item.x.code >= 51 && item.x.code <= 67) ||
                                            (item.x.code >= 80 && item.x.code <= 82) ||
                                            (item.x.code >= 95 && item.x.code <= 99);

                            bool snowFlag = (item.x.code >= 71 && item.x.code <= 77) ||
                                            (item.x.code == 85 || item.x.code == 86);

                            bool cloudyFlag = (item.x.code == 2 || item.x.code == 3);

                            if (rainFlag) {
                                plotModel.Annotations.Add(new RectangleAnnotation {
                                    MinimumX = item.idx + 0.5,
                                    MaximumX = item.idx + 1.5,
                                    Fill = OxyColor.FromAColor(100, OxyColors.Blue), // 半透明の青
                                    Layer = AnnotationLayer.BelowSeries
                                });
                            } else if (snowFlag) {
                                plotModel.Annotations.Add(new RectangleAnnotation {
                                    MinimumX = item.idx + 0.5,
                                    MaximumX = item.idx + 1.5,
                                    Fill = OxyColor.FromAColor(100, OxyColors.LightBlue), // 雪 → 半透明の白
                                    Layer = AnnotationLayer.BelowSeries
                                });
                            } else if (cloudyFlag) {
                                plotModel.Annotations.Add(new RectangleAnnotation {
                                    MinimumX = item.idx + 0.5,
                                    MaximumX = item.idx + 1.5,
                                    Fill = OxyColor.FromAColor(100, OxyColors.Gray), // 曇り → 半透明の薄いグレー
                                    Layer = AnnotationLayer.BelowSeries
                                });
                            } else {
                                plotModel.Annotations.Add(new RectangleAnnotation {
                                    MinimumX = item.idx + 0.5,
                                    MaximumX = item.idx + 1.5,
                                    Fill = OxyColor.FromAColor(100, OxyColors.Orange), // 半透明の橙
                                    Layer = AnnotationLayer.BelowSeries
                                });
                            }
                            series.Points.Add(new DataPoint(item.idx + 0.5, item.x.temp));

                            // 最初の日の場合のみ、現在時刻に最も近い点を特定
                            if (isFirstDay) {
                                // 現在時刻の1時間切り捨てを取得
                                DateTime nowHour = DateTime.Now.Date.AddHours(DateTime.Now.Hour);

                                // データポイントの時刻が nowHour と一致する場合
                                if (item.x.dt == nowHour) {
                                    targetX = item.idx;
                                    targetY = item.x.temp;
                                    isFirstDay = false; // 最初の日の処理で一度見つけたらフラグを解除
                                }
                            }
                        }
                        plotModel.Series.Add(series);

                        // 特定の点が見つかった場合、PointAnnotationを追加
                        if (targetX != -1) {
                            plotModel.Annotations.Add(new PointAnnotation {
                                X = targetX + 0.5,
                                Y = targetY,
                                Size = 6, // マーカーより少し大きく
                                Fill = OxyColors.Blue,
                                Shape = MarkerType.Diamond,
                                Layer = AnnotationLayer.AboveSeries
                            });
                        }

                        isFirstDay = false; // 最初のグラフの処理が完了したので、次のループでは適用しない

                        var controller = new PlotController();
                        controller.UnbindMouseWheel();
                        controller.UnbindMouseDown(OxyMouseButton.Left); // 左クリックでのトラッカーも無効化
                        controller.UnbindMouseDown(OxyMouseButton.Right); // 右クリックでのトラッカーも無効化

                        var view = new OxyPlot.Wpf.PlotView {
                            Model = plotModel,
                            Controller = controller,
                            Width = 563,
                            Height = 300,
                            Margin = new Thickness(0, 0, 0, 0),
                            Background = Brushes.Transparent
                        };

                        // その日の天気コード一覧
                        var codesOfDay = dayGroup.Select(x => x.code).Distinct().ToList();

                        // 晴れ系と雨系を判定
                        var sunnyCodes = codesOfDay.Where(c => c == 0 || c == 1 || c == 2 || c == 3).ToList();
                        var rainCodes = codesOfDay.Where(c =>
                            (c >= 51 && c <= 67) ||
                            (c >= 80 && c <= 82) ||
                            (c >= 95 && c <= 99)
                        ).ToList();

                        string weatherText;
                        if (sunnyCodes.Any() && rainCodes.Any()) {
                            // 両方ある場合 → 代表的なコードを選んで2つ並べる
                            string sunnyDesc = WeatherList.WeatherDescription(sunnyCodes.First());
                            string rainDesc = WeatherList.WeatherDescription(rainCodes.First());
                            weatherText = $"{sunnyDesc}/{rainDesc}";
                        } else if (sunnyCodes.Any()) {
                            weatherText = WeatherList.WeatherDescription(sunnyCodes.First());
                        } else if (rainCodes.Any()) {
                            weatherText = WeatherList.WeatherDescription(rainCodes.First());
                        } else {
                            // その他の天気（曇りや雪など）
                            weatherText = WeatherList.WeatherDescription(codes.First());
                        }

                        var infoGrid = new Grid { Margin = new Thickness(15, 0, 15, 0), Height = 60 }; // 高さを固定して安定化
                        infoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // 左側の列 (日付/気温)
                        infoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.3, GridUnitType.Star) }); // 右側の列 (天気)

                        // 1. 左側コンテナ (日付と最高/最低気温)
                        var leftPanel = new StackPanel { Orientation = Orientation.Vertical, VerticalAlignment = System.Windows.VerticalAlignment.Center };
                        Grid.SetColumn(leftPanel, 0);

                        // 日付
                        var dateBlock = new TextBlock {
                            Text = $"{dayGroup.Key:MM/dd} ({dayGroup.Key:ddd})",
                            Foreground = Brushes.White,
                            FontWeight = System.Windows.FontWeights.Bold,
                            FontSize = 22,
                            Margin = new Thickness(0, 0, 0, 3) // 気温との間に少し隙間
                        };
                        leftPanel.Children.Add(dateBlock);

                        // 最高・最低気温
                        var tempPanel = new StackPanel { Orientation = Orientation.Horizontal };
                        tempPanel.Children.Add(new TextBlock {
                            Text = $"{maxTemp:F1}°C", // 小数点以下を非表示
                            Foreground = Brushes.OrangeRed,
                            FontWeight = System.Windows.FontWeights.ExtraBold,
                            FontSize = 20,
                            Margin = new Thickness(0, 0, 10, 0)
                        });
                        tempPanel.Children.Add(new TextBlock {
                            Text = $"{minTemp:F1}°C", // 小数点以下を非表示
                            Foreground = Brushes.DeepSkyBlue,
                            FontWeight = System.Windows.FontWeights.ExtraBold,
                            FontSize = 18
                        });
                        leftPanel.Children.Add(tempPanel);

                        infoGrid.Children.Add(leftPanel);

                        // 2. 右側コンテナ (天気)
                        var weatherBlock = new TextBlock {
                            Text = $"{weatherText}",
                            Foreground = Brushes.LightGray,
                            FontSize = 38, // ✨ 天気をさらに大きく (32 -> 38)
                            FontWeight = System.Windows.FontWeights.SemiBold,
                            TextAlignment = System.Windows.TextAlignment.Right, // ✨ 右寄せ
                            VerticalAlignment = System.Windows.VerticalAlignment.Center
                        };
                        Grid.SetColumn(weatherBlock, 1);
                        infoGrid.Children.Add(weatherBlock);

                        // 最終コンテナ (グラフと情報をまとめる)
                        var container = new StackPanel { Orientation = Orientation.Vertical };

                        // ✨ 日付と天気、最高最低気温をまとめたinfoGridを最初に追加
                        container.Children.Add(infoGrid);
                        container.Children.Add(view); // グラフをその下に追加

                        var borderedContainer = new Border {
                            Background = Brushes.Transparent,
                            CornerRadius = new CornerRadius(10),
                            Margin = new Thickness(4),
                            Child = container
                        };

                        WeatherPanel.Children.Add(borderedContainer);
                    }
                    LocationBlock.Text = $"📍 場所名: {Country} {State} {City}";
                } else {
                    Console.WriteLine("データが取得できませんでした。");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"エラー：{ex.Message}");
            }
        }

        private void WeatherScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null) return;

            // 子要素を横並びにしている StackPanel
            var panel = WeatherPanel;
            if (panel.Children.Count == 0) return;

            // 現在のスクロール位置
            double currentOffset = scrollViewer.HorizontalOffset;

            // 子要素の位置をリスト化
            var offsets = new List<double>();
            double pos = 0;
            foreach (FrameworkElement child in panel.Children) {
                offsets.Add(pos);
                pos += child.ActualWidth + 8; // 各要素の幅分ずつ進める
            }

            // スクロール可能な最大オフセットを正確に計算
            // WeatherPanelの全幅 - ScrollViewerの表示領域幅
            double maxScrollableOffset = scrollViewer.ScrollableWidth;

            // 次に移動する位置を決定
            double targetOffset = currentOffset;
            if (e.Delta < 0) {
                // ホイール下 → 次の要素へ (右方向)

                // 次のグラフの開始位置を検索
                targetOffset = offsets.FirstOrDefault(x => x > currentOffset + 1);

                // 修正: 次の目標位置が最大スクロール可能範囲を超えていたら、最大値に制限する
                if (targetOffset == 0 && currentOffset > maxScrollableOffset - 1) {
                    // FirstOrDefaultが0を返し、かつ既に右端にいる場合は、現在の位置を維持 (または最大値に)
                    targetOffset = maxScrollableOffset;
                } else if (targetOffset > maxScrollableOffset) {
                    // 計算された次のターゲット位置が最大値を超えたら、最大値に制限
                    targetOffset = maxScrollableOffset;
                }

            } else if (e.Delta > 0) {
                // ホイール上 → 前の要素へ
                targetOffset = offsets.LastOrDefault(x => x < currentOffset - 1);
            }

            // 移動
            scrollViewer.ScrollToHorizontalOffset(targetOffset);
            e.Handled = true;
        }

        private void WeatherScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e) {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer == null) return;

            var panel = WeatherPanel;
            if (panel.Children.Count == 0) return;

            // 現在のスクロール位置
            double currentOffset = scrollViewer.HorizontalOffset;

            // 子要素の位置をリスト化
            var offsets = new List<double>();
            double pos = 0;
            foreach (FrameworkElement child in panel.Children) {
                offsets.Add(pos);
                pos += child.ActualWidth + 8; // 各要素の幅分ずつ進める
            }

            // 最も近い位置にスナップ
            double targetOffset = offsets.OrderBy(x => Math.Abs(x - currentOffset)).FirstOrDefault();

            // スナップ移動
            scrollViewer.ScrollToHorizontalOffset(targetOffset);
        }
    }
}
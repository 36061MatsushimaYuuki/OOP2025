using System.Net;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            using (var hc = new HttpClient()) {
                try {
                    var responce = await hc.GetStreamAsync(tbUrl.Text);
                    XDocument xdoc = XDocument.Load(responce);   //RSSの取得

                    //XDocument xdoc2 = XDocument.Parse(await hc.GetStringAsync(tbUrl.Text)); 

                    //var url = hc.OpenRead(tbUrl.Text);
                    //XDocument xdoc = XDocument.Load(url);

                    //RSSを解析して必要な要素を取得
                    items = xdoc.Root.Descendants("item")
                        .Select(x => new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                    //リストボックスへタイトルを表示
                    lbTitles.Items.Clear();
                    items.ForEach(item => lbTitles.Items.Add(item.Title ?? "不明なデータ"));
                }
                catch (Exception ex) {
                    MessageBox.Show($"RSS取得中にエラーが発生しました: {ex.Message}", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //タイトルを選択（クリック）したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            var selectedIndex = lbTitles.SelectedIndex;
            if (items is not null && items[selectedIndex].Link is not null) {
                try {
                    webView21.Source = new Uri(items[selectedIndex].Link);
                }
                catch (Exception ex) {
                    MessageBox.Show($"URL情報取得中にエラーが発生しました: {ex.Message}", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

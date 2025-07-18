using System.Net;
using System.Security.Cryptography.Xml;
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
                    var url = cbUrl.SelectedIndex < 0 ? cbUrl.Text : ((FavoriteItem)cbUrl.SelectedItem).Value;
                    var responce = await hc.GetStreamAsync(url);
                    XDocument xdoc = XDocument.Load(responce);   //RSSの取得

                    //XDocument xdoc2 = XDocument.Parse(await hc.GetStringAsync(cbUrl.Text)); 

                    //var url = hc.OpenRead(cbUrl.Text);
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
            if (items is not null && items[selectedIndex] is not null) {
                try {
                    wvRssLink.Source = new Uri(items[selectedIndex].Link);
                }
                catch (Exception ex) {
                    MessageBox.Show($"URL情報取得中にエラーが発生しました: {ex.Message}", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //進むボタン
        private void btForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }

        //戻るボタン
        private void btBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }

        //進む 戻るボタンの有効化判定処理
        private void checkBackForward() {
            btBack.Enabled = true;
            btForward.Enabled = true;
            if (!wvRssLink.CanGoBack)
                btBack.Enabled = false;
            if (!wvRssLink.CanGoForward)
                btForward.Enabled = false;
        }

        private void btFavoriteAdd_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(cbUrl.Text) && !string.IsNullOrWhiteSpace(tbFavorite.Text)) {
                int cnt;
                for(cnt = 0; cnt < cbUrl.Items.Count; cnt++) {
                    if (cbUrl.Items[cnt].ToString() == tbFavorite.Text) {
                        break;
                    }
                }
                if (cnt == cbUrl.Items.Count) {
                    cbUrl.Items.Add(new FavoriteItem {
                        DisplayName = tbFavorite.Text,
                        Value = cbUrl.Text
                    });
                    cbUrl.Text = "";
                    tbFavorite.Text = "";
                } else {
                    MessageBox.Show("同じお気に入り名称が既に存在します", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("URLまたはお気に入り名称が空です", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void wvRssLink_ContentLoading(object sender, Microsoft.Web.WebView2.Core.CoreWebView2ContentLoadingEventArgs e) {
            checkBackForward();
        }
    }
}

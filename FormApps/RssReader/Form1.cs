using System.Net;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            lbTitles.DrawItem += lbTitles_DrawItem;

            ActiveControl = cbUrl;
            checkBackForward();
            btReload.Enabled = false;
            addFavoriteItem("★群馬テレビ", "https://news.yahoo.co.jp/rss/media/gtv/all.xml", false);
            addFavoriteItem("★グルメ", "https://news.yahoo.co.jp/rss/media/impgrw/all.xml", false);
            addFavoriteItem("★クロワッサンオンライン", "https://news.yahoo.co.jp/rss/media/crssntv/all.xml", false);
            addFavoriteItem("★ITメディア", "https://news.yahoo.co.jp/rss/media/zdn_n/all.xml");
            addFavoriteItem("★科学", "https://news.yahoo.co.jp/rss/topics/science.xml", false);
            addFavoriteItem("★経済", "https://news.yahoo.co.jp/rss/topics/business.xml", false);
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
            if (items is not null && lbTitles.Items.Count > 0 && items[selectedIndex] is not null) {
                try {
                    var index = items.FindIndex(i => i.Title == lbTitles.Text);
                    wvRssLink.Source = new Uri(items[index].Link);
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
            btReload.Enabled = true;
            btBack.Enabled = wvRssLink.CanGoBack;
            btForward.Enabled = wvRssLink.CanGoForward;
        }

        private void btFavoriteAdd_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(cbUrl.Text) && !string.IsNullOrWhiteSpace(tbFavorite.Text)) {
                if(!Uri.IsWellFormedUriString(cbUrl.Text, UriKind.Absolute)) {
                    MessageBox.Show("入力されたURLが正しくありません", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int cnt;
                for (cnt = 0; cnt < cbUrl.Items.Count; cnt++) {
                    if (cbUrl.Items[cnt].ToString() == tbFavorite.Text) {
                        break;
                    }
                }
                if (cnt == cbUrl.Items.Count) {
                    addFavoriteItem(tbFavorite.Text, cbUrl.Text);
                    MessageBox.Show("お気に入り登録が完了しました", "RSSリーダー", MessageBoxButtons.OK);
                    cbUrl.Text = "";
                    tbFavorite.Text = "";
                } else {
                    MessageBox.Show("同じお気に入り名称が既に存在します", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("URLまたはお気に入り名称が空です", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addFavoriteItem(string name, string url, bool canDelete = true) {
            cbUrl.Items.Add(new FavoriteItem {
                DisplayName = name,
                Value = url,
                CanDelete = canDelete
            });
        }

        private void wvRssLink_ContentLoading(object sender, Microsoft.Web.WebView2.Core.CoreWebView2ContentLoadingEventArgs e) {
            checkBackForward();
            tbSiteUrlText.Text = wvRssLink.Source.AbsoluteUri;
        }

        //サイトを再読み込み
        private void btReload_Click(object sender, EventArgs e) {
            try {
                wvRssLink.Reload();
            }
            catch (Exception ex) {

            }
        }

        private void btFavoriteDelete_Click(object sender, EventArgs e) {
            var selectedIndex = cbUrl.SelectedIndex;
            if (selectedIndex > -1) {
                if (MessageBox.Show("選択したお気に入り名称を本当に削除しますか？", "RSSリーダー", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    if (((FavoriteItem)cbUrl.SelectedItem).CanDelete) {
                        cbUrl.Items.RemoveAt(selectedIndex);
                        cbUrl.Text = "";
                        return;
                    }
                    MessageBox.Show("このお気に入り名称は削除することができません", "RSSリーダー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {
            if (e.Index < 0) return;

            // 奇数行と偶数行で異なる背景色を設定
            Color backColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? SystemColors.Highlight :
                              (e.Index % 2 == 0) ? Color.White : Color.WhiteSmoke;

            using (SolidBrush brush = new SolidBrush(backColor)) {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // テキストを描画
            TextRenderer.DrawText(e.Graphics, lbTitles.Items[e.Index].ToString(),
            lbTitles.Font, e.Bounds, lbTitles.ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            // 選択状態の描画
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) {
                e.DrawFocusRectangle();
            }
        }

        private void btSearch_Click(object sender, EventArgs e) {
            lbTitles.Items.Clear();
            if (string.IsNullOrWhiteSpace(tbSearch.Text)) {
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "不明なデータ"));
                return;
            }
            var indexlist = new List<int>();
            int index = items.FindIndex(i => Regex.IsMatch(i.Title, ".*" + tbSearch.Text + ".*"));
            while(index > -1) {
                indexlist.Add(index);
                index++;
                index = items.FindIndex(index, i => Regex.IsMatch(i.Title, ".*" + tbSearch.Text + ".*"));
            }
            foreach(var selected in indexlist) {
                lbTitles.Items.Add(items[selected].Title);
            }
        }
    }
}

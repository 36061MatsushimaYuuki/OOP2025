using System.Net;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
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
            if (!File.Exists("FavoriteList.json")) {
                addFavoriteItem("���Q�n�e���r", "https://news.yahoo.co.jp/rss/media/gtv/all.xml", false);
                addFavoriteItem("���O����", "https://news.yahoo.co.jp/rss/media/impgrw/all.xml", false);
                addFavoriteItem("���N�����b�T���I�����C��", "https://news.yahoo.co.jp/rss/media/crssntv/all.xml", false);
                addFavoriteItem("��IT���f�B�A", "https://news.yahoo.co.jp/rss/media/zdn_n/all.xml");
                addFavoriteItem("���Ȋw", "https://news.yahoo.co.jp/rss/topics/science.xml", false);
                addFavoriteItem("���o��", "https://news.yahoo.co.jp/rss/topics/business.xml", false);
            } else {
                try {
                    var jsonString = File.ReadAllText("FavoriteList.json");
                    var items = JsonSerializer.Deserialize<FavoriteItem[]>(jsonString);
                    foreach (var item in items) {
                        addFavoriteItem(item.DisplayName, item.Value, item.CanDelete);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"���C�ɓ��薼�̃��X�g�̓ǂݍ��݂Ɏ��s���܂���: {ex.Message}", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            using (var hc = new HttpClient()) {
                try {
                    var url = cbUrl.SelectedIndex < 0 ? cbUrl.Text : ((FavoriteItem)cbUrl.SelectedItem).Value;
                    var responce = await hc.GetStreamAsync(url);
                    XDocument xdoc = XDocument.Load(responce);   //RSS�̎擾

                    //XDocument xdoc2 = XDocument.Parse(await hc.GetStringAsync(cbUrl.Text)); 

                    //var url = hc.OpenRead(cbUrl.Text);
                    //XDocument xdoc = XDocument.Load(url);

                    //RSS����͂��ĕK�v�ȗv�f���擾
                    items = xdoc.Root.Descendants("item")
                        .Select(x => new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                    //���X�g�{�b�N�X�փ^�C�g����\��
                    lbTitles.Items.Clear();
                    items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�s���ȃf�[�^"));
                }
                catch (Exception ex) {
                    MessageBox.Show($"RSS�擾���ɃG���[���������܂���: {ex.Message}", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //�^�C�g����I���i�N���b�N�j�����Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            var selectedIndex = lbTitles.SelectedIndex;
            if (items is not null && items[selectedIndex] is not null) {
                try {
                    var index = items.FindIndex(i => i.Title == lbTitles.Text);
                    wvRssLink.Source = new Uri(items[index].Link);
                }
                catch (Exception ex) {
                    
                }
            }
        }

        //�i�ރ{�^��
        private void btForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }

        //�߂�{�^��
        private void btBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }

        //�i�� �߂�{�^���̗L�������菈��
        private void checkBackForward() {
            btReload.Enabled = true;
            btBack.Enabled = wvRssLink.CanGoBack;
            btForward.Enabled = wvRssLink.CanGoForward;
        }

        private void btFavoriteAdd_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(cbUrl.Text) && !string.IsNullOrWhiteSpace(tbFavorite.Text)) {
                if (!Uri.IsWellFormedUriString(cbUrl.Text, UriKind.Absolute)) {
                    MessageBox.Show("���͂��ꂽURL������������܂���", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("���C�ɓ���o�^���������܂���", "RSS���[�_�[", MessageBoxButtons.OK);
                    cbUrl.Text = "";
                    tbFavorite.Text = "";
                } else {
                    MessageBox.Show("�������C�ɓ��薼�̂����ɑ��݂��܂�", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("URL�܂��͂��C�ɓ��薼�̂���ł�", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //�T�C�g���ēǂݍ���
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
                if (MessageBox.Show("�I���������C�ɓ��薼�̂�{���ɍ폜���܂����H", "RSS���[�_�[", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    if (((FavoriteItem)cbUrl.SelectedItem).CanDelete) {
                        cbUrl.Items.RemoveAt(selectedIndex);
                        cbUrl.Text = "";
                        return;
                    }
                    MessageBox.Show("���̂��C�ɓ��薼�͍̂폜���邱�Ƃ��ł��܂���", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {
            if (e.Index < 0) return;

            int maxWidth = 0;
            foreach (object item in lbTitles.SelectedItems) {
                int itemWidth = TextRenderer.MeasureText(item.ToString(), lbTitles.Font, lbTitles.Size).Width;
                if (itemWidth > maxWidth) {
                    maxWidth = itemWidth;
                }
            }

            // ��s�Ƌ����s�ňقȂ�w�i�F��ݒ�
            Color backColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? SystemColors.Highlight :
                              (e.Index % 2 == 0) ? Color.White : Color.WhiteSmoke;

            using (SolidBrush brush = new SolidBrush(backColor)) {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            // �e�L�X�g��`��
            TextRenderer.DrawText(e.Graphics, lbTitles.Items[e.Index].ToString(),
            lbTitles.Font, e.Bounds, lbTitles.ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            // �I����Ԃ̕`��
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) {
                e.DrawFocusRectangle();
            }

            if (maxWidth > lbTitles.HorizontalExtent) {
                lbTitles.HorizontalExtent = maxWidth;
            }
        }

        private void btSearch_Click(object sender, EventArgs e) {
            lbTitles.Items.Clear();
            if (string.IsNullOrWhiteSpace(tbSearch.Text)) {
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�s���ȃf�[�^"));
                return;
            }
            var indexlist = new List<int>();
            int index = items.FindIndex(i => Regex.IsMatch(i.Title, ".*" + tbSearch.Text + ".*"));
            while (index > -1) {
                indexlist.Add(index);
                index++;
                index = items.FindIndex(index, i => Regex.IsMatch(i.Title, ".*" + tbSearch.Text + ".*"));
            }
            foreach (var selected in indexlist) {
                lbTitles.Items.Add(items[selected].Title);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(tbSearch.Text)) {
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�s���ȃf�[�^"));
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            var options = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(cbUrl.Items, options);
            try {
                File.WriteAllText("FavoriteList.json", jsonString);
            }
            catch (Exception ex) {

            }
        }
    }
}

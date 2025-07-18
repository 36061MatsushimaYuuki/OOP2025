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
                    wvRssLink.Source = new Uri(items[selectedIndex].Link);
                }
                catch (Exception ex) {
                    MessageBox.Show($"URL���擾���ɃG���[���������܂���: {ex.Message}", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("�������C�ɓ��薼�̂����ɑ��݂��܂�", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("URL�܂��͂��C�ɓ��薼�̂���ł�", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void wvRssLink_ContentLoading(object sender, Microsoft.Web.WebView2.Core.CoreWebView2ContentLoadingEventArgs e) {
            checkBackForward();
        }
    }
}

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
                    XDocument xdoc = XDocument.Load(responce);   //RSS�̎擾

                    //XDocument xdoc2 = XDocument.Parse(await hc.GetStringAsync(tbUrl.Text)); 

                    //var url = hc.OpenRead(tbUrl.Text);
                    //XDocument xdoc = XDocument.Load(url);

                    //RSS����͂��ĕK�v�ȗv�f���擾
                    items = xdoc.Root.Descendants("item")
                        .Select(x => new ItemData {
                            Title = (string)x.Element("title"),
                            Link = (string)x.Element("link"),
                        }).ToList();

                    //���X�g�{�b�N�X�փ^�C�g����\��
                    lbTitles.Items.Clear();
                    items.ForEach(item => lbTitles.Items.Add(item.Title));
                }
                catch (Exception ex) {
                    MessageBox.Show($"RSS�擾���ɃG���[���������܂���: {ex.Message}", "RSS���[�_�[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

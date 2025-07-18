using System.Net;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private void btRssGet_Click(object sender, EventArgs e) {
            using (var wc = new WebClient()) {
                var url = wc.OpenRead(tbUrl.Text);
                XDocument xdoc = XDocument.Load(url);   //RSS‚ÌŽæ“¾

                //RSS‚ð‰ðÍ‚µ‚Ä•K—v‚È—v‘f‚ðŽæ“¾
                items = xdoc.Root.Descendants("item")
                    .Select(x => new ItemData { Title = (string)x.Element("title")}).ToList();

                lbTitles.Items.Clear();
                foreach (var item in items) {
                    lbTitles.Items.Add(item.Title);
                }
            }
        }
    }
}

using System.ComponentModel;
using System.Net.Http.Headers;
using System.Windows.Forms;
using static CarReportSystem.CarReport;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //カーレポート管理用リスト
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        public Form1() {
            InitializeComponent();
            dgvRecord.DataSource = listCarReports;
        }

        //コンストラクタの後に呼ばれる
        private void Form1_Load(object sender, EventArgs e) {
            InputItemsAllClear();
        }

        private void btPicOpen_Click(object sender, EventArgs e) {
            ofdPicFileOpen.Filter = "画像ファイル (*.jpg;.jpeg;*.png;*.webp)|*.jpg;.jpeg;*.png;*.webp";
            if (ofdPicFileOpen.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        private void btPicDelete_Click(object sender, EventArgs e) {
            if (pbPicture.Image != null && MessageBox.Show("本当に画像を削除しますか？", "試乗レポート管理システム", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                pbPicture.Image = null;
            }
        }

        //追加ボタンのイベントハンドラ
        private void btRecordAdd_Click(object sender, EventArgs e) {
            var carReport = new CarReport {
                Date = dtpDate.Value,
                Author = cbAuthor.Text,
                Maker = GetRadioButtonMaker(),
                CarName = cbCarName.Text,
                Report = tbReport.Text,
                Picture = pbPicture.Image,
            };
            listCarReports.Add(carReport);
            setCbAuthor(cbAuthor.Text);
            setCbCarName(cbCarName.Text);
            InputItemsAllClear();
        }

        //入力項目をすべてクリア
        private void InputItemsAllClear() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = string.Empty;
            rbOther.Checked = true;
            cbCarName.Text = string.Empty;
            tbReport.Text = string.Empty;
            pbPicture.Image = null;
        }

        //記録者の履歴をコンボボックスへ登録（重複なし）
        private void setCbAuthor(string author) {
            //値が空でないか、既に登録済みか確認
            if (author != "" && cbAuthor.Items.IndexOf(author) == -1) { //!cbCarName.Items.Contains(author)
                cbAuthor.Items.Add(author);
            }
        }

        //車名の履歴をコンボボックスへ登録（重複なし）
        private void setCbCarName(string carName) {
            //値が空でないか、既に登録済みか確認
            if (carName != "" && cbCarName.Items.IndexOf(carName) == -1) {
                cbCarName.Items.Add(carName);
            }
        }

        private CarReport.MakerGroup GetRadioButtonMaker() {
            RadioButton? checkedRadioButton = null;

            foreach (Control control in groupBox1.Controls) {
                if (/*control is RadioButton && */((RadioButton)control).Checked) {
                    checkedRadioButton = (RadioButton)control;
                    return (MakerGroup)Enum.Parse(typeof(MakerGroup), checkedRadioButton.Text);
                }
            }
            return MakerGroup.その他;

            /*
            if (rbToyota.Checked)
                return MakerGroup.トヨタ;
            else if (rbNissan.Checked)
                return MakerGroup.トヨタ;
            else if (rbHonda.Checked)
                return MakerGroup.ホンダ;
            else if (rbSubaru.Checked)
                return MakerGroup.スバル;
            else if (rbImport.Checked)
                return MakerGroup.輸入車;
            return MakerGroup.その他;
            */
        }

        private void dgvRecord_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow != null && dgvRecord.Rows.Count > 0) {
                dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
                cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
                setRadioButtonMaker((MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
                cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
                tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
                pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
            }
        }

        //指定したメーカーのラジオボタンをセット
        private void setRadioButtonMaker(MakerGroup targetMaker) {
            switch (targetMaker) {
                case MakerGroup.なし:
                    break;
                case MakerGroup.トヨタ:
                    rbToyota.Checked = true;
                    break;
                case MakerGroup.日産:
                    rbNissan.Checked = true;
                    break;
                case MakerGroup.ホンダ:
                    rbHonda.Checked = true;
                    break;
                case MakerGroup.スバル:
                    rbSubaru.Checked = true;
                    break;
                case MakerGroup.輸入車:
                    rbImport.Checked = true;
                    break;
                case MakerGroup.その他:
                    rbOther.Checked = true;
                    break;
                default:
                    break;
            }
        }

        //新規入力のイベントハンドラ
        private void btNewRecord_Click(object sender, EventArgs e) {
            InputItemsAllClear();
        }

        //修正ボタンのイベントハンドラ
        private void btRecordModify_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow != null && dgvRecord.Rows.Count > 0) {
                if (MessageBox.Show("現在のレコードを修正しますか？", "試乗レポート管理システム", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                    /*
                    dgvRecord.CurrentRow.Cells["Date"].Value = dtpDate.Value;
                    dgvRecord.CurrentRow.Cells["Author"].Value = cbAuthor.Text;
                    dgvRecord.CurrentRow.Cells["Maker"].Value = GetRadioButtonMaker();
                    dgvRecord.CurrentRow.Cells["CarName"].Value = cbCarName.Text;
                    dgvRecord.CurrentRow.Cells["Report"].Value = tbReport.Text;
                    dgvRecord.CurrentRow.Cells["Picture"].Value = pbPicture.Image;
                    */
                    var carReport = new CarReport {
                        Date = dtpDate.Value,
                        Author = cbAuthor.Text,
                        Maker = GetRadioButtonMaker(),
                        CarName = cbCarName.Text,
                        Report = tbReport.Text,
                        Picture = pbPicture.Image,
                    };
                    listCarReports[dgvRecord.CurrentRow.Index] = carReport;
                    InputItemsAllClear();
                }
            }
        }

        //削除ボタンのイベントハンドラ
        private void btRecordDelete_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow != null && dgvRecord.Rows.Count > 0) {
                if (MessageBox.Show("本当に現在のレコードを削除しますか？", "試乗レポート管理システム", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                    listCarReports.RemoveAt(dgvRecord.CurrentRow.Index);
                    InputItemsAllClear();
                }
            }
        }
    }
}

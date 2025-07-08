using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static CarReportSystem.CarReport;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //カーレポート管理用リスト
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        //設定クラスのインスタンスを生成
        Settings settings = new Settings();

        public Form1() {
            InitializeComponent();
            dgvRecord.DataSource = listCarReports;
        }

        //コンストラクタの後に呼ばれる
        private void Form1_Load(object sender, EventArgs e) {
            InputItemsAllClear();
            //交互に色を設定（データグリッドビュー）
            dgvRecord.DefaultCellStyle.BackColor = Color.LightGreen;
            dgvRecord.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            ////データグリッドビューの列名に別名を設定
            //dgvRecord.Columns[0].HeaderText = "日付";
            //dgvRecord.Columns[1].HeaderText = "記録者";

            //設定ファイルを読み込み背景色を設定する（逆シリアル化）
            try {
                using (var reader = XmlReader.Create("settings.xml")) {
                    var serializer = new XmlSerializer(typeof(Settings));
                    settings = (Settings)serializer.Deserialize(reader);
                    BackColor = Color.FromArgb(settings.MainFormBackColor);
                }
            }
            catch (Exception) {

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            //設定ファイルへ色情報を保存する処理（シリアル化）
            try {
                using (var writer = XmlWriter.Create("settings.xml")) {
                    var serializer = new XmlSerializer(settings.GetType());
                    serializer.Serialize(writer, settings);
                }
            }
            catch (Exception) {

            }
        }

        private void btPicOpen_Click(object sender, EventArgs e) {
            ofdPicFileOpen.Filter = "画像ファイル (*.jpg;.jpeg;*.png;*.webp)|*.jpg;.jpeg;*.png;*.webp";
            if (ofdPicFileOpen.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        private void btPicDelete_Click(object sender, EventArgs e) {
            if (pbPicture.Image is not null && MessageBox.Show("本当に画像を削除しますか？", "試乗レポート管理システム", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                pbPicture.Image = null;
            }
        }

        //追加ボタンのイベントハンドラ
        private void btRecordAdd_Click(object sender, EventArgs e) {

            tsslbMessage.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(cbAuthor.Text) || string.IsNullOrWhiteSpace(cbCarName.Text)) {
                tsslbMessage.Text = "記録者、または車名が未入力です";
                //MessageBox.Show("記録者、または車名が未入力です", "試乗レポート管理システム", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var carReport = new CarReport {
                Date = dtpDate.Value,
                Author = cbAuthor.Text,
                Maker = getRadioButtonMaker(),
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
            if (string.IsNullOrWhiteSpace(author) || cbAuthor.Items.IndexOf(author) != -1) { //!cbCarName.Items.Contains(author)
                return;
            }
            cbAuthor.Items.Add(author);
        }

        //車名の履歴をコンボボックスへ登録（重複なし）
        private void setCbCarName(string carName) {
            //値が空でないか、既に登録済みか確認
            if (string.IsNullOrWhiteSpace(carName) || cbCarName.Items.IndexOf(carName) != -1) {
                return;
            }
            cbCarName.Items.Add(carName);
        }

        private CarReport.MakerGroup getRadioButtonMaker() {
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
            if (dgvRecord.CurrentRow is null || dgvRecord.Rows.Count <= 0) {
                return;
            }
            dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
            cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
            setRadioButtonMaker((MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
            cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
            tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
            pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
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
            if (dgvRecord.CurrentRow is null || dgvRecord.Rows.Count <= 0) {
                return;
            }
            if (MessageBox.Show("現在のレコードを修正しますか？", "試乗レポート管理システム", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                /* リストの中身が変わらないので廃止
                dgvRecord.CurrentRow.Cells["Date"].Value = dtpDate.Value;
                dgvRecord.CurrentRow.Cells["Author"].Value = cbAuthor.Text;
                dgvRecord.CurrentRow.Cells["Maker"].Value = getRadioButtonMaker();
                dgvRecord.CurrentRow.Cells["CarName"].Value = cbCarName.Text;
                dgvRecord.CurrentRow.Cells["Report"].Value = tbReport.Text;
                dgvRecord.CurrentRow.Cells["Picture"].Value = pbPicture.Image;
                */

                //var carReport = new CarReport {
                //    Date = dtpDate.Value,
                //    Author = cbAuthor.Text,
                //    Maker = getRadioButtonMaker(),
                //    CarName = cbCarName.Text,
                //    Report = tbReport.Text,
                //    Picture = pbPicture.Image,
                //};
                ////選択されたインデックスのリストの中身を置き換える
                //listCarReports[dgvRecord.CurrentRow.Index] = carReport;

                var index = dgvRecord.CurrentRow.Index;
                listCarReports[index].Date = dtpDate.Value;
                listCarReports[index].Author = cbAuthor.Text;
                listCarReports[index].Maker = getRadioButtonMaker();
                listCarReports[index].CarName = cbCarName.Text;
                listCarReports[index].Report = tbReport.Text;
                listCarReports[index].Picture = pbPicture.Image;

                InputItemsAllClear();
                dgvRecord.Refresh();
            }
        }

        //削除ボタンのイベントハンドラ
        private void btRecordDelete_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null || dgvRecord.Rows.Count <= 0) {
                return;
            }
            if (MessageBox.Show("本当に現在のレコードを削除しますか？", "試乗レポート管理システム", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                listCarReports.RemoveAt(dgvRecord.CurrentRow.Index);
                InputItemsAllClear();
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void tsmiAbout_Click(object sender, EventArgs e) {
            var versionForm = new fmVersion();
            versionForm.ShowDialog();
        }

        private void 色設定ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cdColor.ShowDialog() == DialogResult.OK) {
                BackColor = cdColor.Color;
                //設定ファイルへ保存
                settings.MainFormBackColor = cdColor.Color.ToArgb(); //背景色を設定インスタンスへ設定
            }
        }

        //ファイルオープン処理
        private void reportOpenFile() {
            if (ofdReportFileOpen.ShowDialog() == DialogResult.OK) {
                try {
                    //逆シリアル化でバイナリ形式を取り込む
#pragma warning disable SYSLIB0011 // 型またはメンバーが旧型式です
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // 型またはメンバーが旧型式です

                    using (FileStream fs = File.Open(
                        ofdReportFileOpen.FileName, FileMode.Open, FileAccess.Read)) {

                        listCarReports = (BindingList<CarReport>)bf.Deserialize(fs);
                        dgvRecord.DataSource = listCarReports;
                        //元のコンボボックスデータをクリア
                        cbAuthor.Items.Clear();
                        cbCarName.Items.Clear();
                        //コンボボックスに登録
                        foreach (var item in listCarReports) { //1つずつ取り出す
                            setCbAuthor(item.Author);
                            setCbCarName(item.CarName);
                        }
                    }

                }
                catch (Exception ex) {
                    tsslbMessage.Text = "ファイル形式が違います";
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //ファイルセーブ処理
        private void reportSaveFile() {
            if (sfdReportFileSave.ShowDialog() == DialogResult.OK) {
                try {
                    //バイナリ形式でシリアル化
#pragma warning disable SYSLIB0011
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011

                    using (FileStream fs = File.Open(
                        sfdReportFileSave.FileName, FileMode.Create)) {

                        bf.Serialize(fs, listCarReports);
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "ファイル書き出しエラー";
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e) {
            reportSaveFile();
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e) {
            reportOpenFile();
        }
    }
}

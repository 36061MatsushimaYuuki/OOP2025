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
        //�J�[���|�[�g�Ǘ��p���X�g
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        //�ݒ�N���X�̃C���X�^���X�𐶐�
        Settings settings = new Settings();

        public Form1() {
            InitializeComponent();
            dgvRecord.DataSource = listCarReports;
        }

        //�R���X�g���N�^�̌�ɌĂ΂��
        private void Form1_Load(object sender, EventArgs e) {
            InputItemsAllClear();
            //���݂ɐF��ݒ�i�f�[�^�O���b�h�r���[�j
            dgvRecord.DefaultCellStyle.BackColor = Color.LightGreen;
            dgvRecord.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            ////�f�[�^�O���b�h�r���[�̗񖼂ɕʖ���ݒ�
            //dgvRecord.Columns[0].HeaderText = "���t";
            //dgvRecord.Columns[1].HeaderText = "�L�^��";

            //�ݒ�t�@�C����ǂݍ��ݔw�i�F��ݒ肷��i�t�V���A�����j
            try {
                using (var reader = XmlReader.Create("settings.xml")) {
                    var serializer = new XmlSerializer(typeof(Settings));
                    settings = (Settings)serializer.Deserialize(reader);
                    BackColor = Color.FromArgb(settings.MainFormBackColor);
                }
            }
            catch (Exception) {

            }
            dateTimer.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            //�ݒ�t�@�C���֐F����ۑ����鏈���i�V���A�����j
            try {
                using (var writer = XmlWriter.Create("settings.xml")) {
                    var serializer = new XmlSerializer(settings.GetType());
                    serializer.Serialize(writer, settings);
                }
            }
            catch (Exception) {

            }
            dateTimer.Stop();
        }

        private void btPicOpen_Click(object sender, EventArgs e) {
            ofdPicFileOpen.Filter = "�摜�t�@�C�� (*.jpg;.jpeg;*.png;*.webp)|*.jpg;.jpeg;*.png;*.webp";
            if (ofdPicFileOpen.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        private void btPicDelete_Click(object sender, EventArgs e) {
            if (pbPicture.Image is not null && MessageBox.Show("�{���ɉ摜���폜���܂����H", "���惌�|�[�g�Ǘ��V�X�e��", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                pbPicture.Image = null;
            }
        }

        //�ǉ��{�^���̃C�x���g�n���h��
        private void btRecordAdd_Click(object sender, EventArgs e) {

            tsslbMessage.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(cbAuthor.Text) || string.IsNullOrWhiteSpace(cbCarName.Text)) {
                tsslbMessage.Text = "�L�^�ҁA�܂��͎Ԗ��������͂ł�";
                //MessageBox.Show("�L�^�ҁA�܂��͎Ԗ��������͂ł�", "���惌�|�[�g�Ǘ��V�X�e��", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //���͍��ڂ����ׂăN���A
        private void InputItemsAllClear() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = string.Empty;
            rbOther.Checked = true;
            cbCarName.Text = string.Empty;
            tbReport.Text = string.Empty;
            pbPicture.Image = null;
        }

        //�L�^�҂̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbAuthor(string author) {
            //�l����łȂ����A���ɓo�^�ς݂��m�F
            if (string.IsNullOrWhiteSpace(author) || cbAuthor.Items.IndexOf(author) != -1) { //!cbCarName.Items.Contains(author)
                return;
            }
            cbAuthor.Items.Add(author);
        }

        //�Ԗ��̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbCarName(string carName) {
            //�l����łȂ����A���ɓo�^�ς݂��m�F
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
            return MakerGroup.���̑�;

            /*
            if (rbToyota.Checked)
                return MakerGroup.�g���^;
            else if (rbNissan.Checked)
                return MakerGroup.�g���^;
            else if (rbHonda.Checked)
                return MakerGroup.�z���_;
            else if (rbSubaru.Checked)
                return MakerGroup.�X�o��;
            else if (rbImport.Checked)
                return MakerGroup.�A����;
            return MakerGroup.���̑�;
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

        //�w�肵�����[�J�[�̃��W�I�{�^�����Z�b�g
        private void setRadioButtonMaker(MakerGroup targetMaker) {
            switch (targetMaker) {
                case MakerGroup.�Ȃ�:
                    break;
                case MakerGroup.�g���^:
                    rbToyota.Checked = true;
                    break;
                case MakerGroup.���Y:
                    rbNissan.Checked = true;
                    break;
                case MakerGroup.�z���_:
                    rbHonda.Checked = true;
                    break;
                case MakerGroup.�X�o��:
                    rbSubaru.Checked = true;
                    break;
                case MakerGroup.�A����:
                    rbImport.Checked = true;
                    break;
                case MakerGroup.���̑�:
                    rbOther.Checked = true;
                    break;
                default:
                    break;
            }
        }

        //�V�K���͂̃C�x���g�n���h��
        private void btNewRecord_Click(object sender, EventArgs e) {
            InputItemsAllClear();
        }

        //�C���{�^���̃C�x���g�n���h��
        private void btRecordModify_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null || dgvRecord.Rows.Count <= 0) {
                return;
            }
            if (MessageBox.Show("���݂̃��R�[�h���C�����܂����H", "���惌�|�[�g�Ǘ��V�X�e��", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                /* ���X�g�̒��g���ς��Ȃ��̂Ŕp�~
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
                ////�I�����ꂽ�C���f�b�N�X�̃��X�g�̒��g��u��������
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

        //�폜�{�^���̃C�x���g�n���h��
        private void btRecordDelete_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null || dgvRecord.Rows.Count <= 0) {
                return;
            }
            if (MessageBox.Show("�{���Ɍ��݂̃��R�[�h���폜���܂����H", "���惌�|�[�g�Ǘ��V�X�e��", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
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

        private void �F�ݒ�ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cdColor.ShowDialog() == DialogResult.OK) {
                BackColor = cdColor.Color;
                //�ݒ�t�@�C���֕ۑ�
                settings.MainFormBackColor = cdColor.Color.ToArgb(); //�w�i�F��ݒ�C���X�^���X�֐ݒ�
            }
        }

        //�t�@�C���I�[�v������
        private void reportOpenFile() {
            //�g���q�̐ݒ�
            ofdReportFileOpen.Filter = "Report�t�@�C��(*.report)|*.report|���ׂẴt�@�C�� (*.*)|*.*";

            if (ofdReportFileOpen.ShowDialog() == DialogResult.OK) {
                try {
                    //�t�V���A�����Ńo�C�i���`������荞��
#pragma warning disable SYSLIB0011 // �^�܂��̓����o�[�����^���ł�
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // �^�܂��̓����o�[�����^���ł�

                    using (FileStream fs = File.Open(
                        ofdReportFileOpen.FileName, FileMode.Open, FileAccess.Read)) {

                        listCarReports = (BindingList<CarReport>)bf.Deserialize(fs);
                        dgvRecord.DataSource = listCarReports;
                        //���̃R���{�{�b�N�X�f�[�^���N���A
                        cbAuthor.Items.Clear();
                        cbCarName.Items.Clear();
                        //�R���{�{�b�N�X�ɓo�^
                        foreach (var item in listCarReports) { //1�����o��
                            setCbAuthor(item.Author);
                            setCbCarName(item.CarName);
                        }
                    }

                }
                catch (Exception ex) {
                    tsslbMessage.Text = "�t�@�C���`�����Ⴂ�܂�";
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //�t�@�C���Z�[�u����
        private void reportSaveFile() {
            //�g���q�������I�ɂ���
            sfdReportFileSave.AddExtension = true;

            //�g���q�̐ݒ�
            sfdReportFileSave.Filter = "Report�t�@�C��(*.report)|*.report|���ׂẴt�@�C�� (*.*)|*.*";

            if (sfdReportFileSave.ShowDialog() == DialogResult.OK) {
                try {
                    //�o�C�i���`���ŃV���A����
#pragma warning disable SYSLIB0011
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011

                    using (FileStream fs = File.Open(
                        sfdReportFileSave.FileName, FileMode.Create)) {

                        bf.Serialize(fs, listCarReports);
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "�t�@�C�������o���G���[";
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void �ۑ�ToolStripMenuItem_Click(object sender, EventArgs e) {
            reportSaveFile();
        }

        private void �J��ToolStripMenuItem_Click(object sender, EventArgs e) {
            reportOpenFile();
        }

        private void dateTimer_Tick(object sender, EventArgs e) {
            var now = DateTime.Now;
            tsslDateTime.Text = now.ToString("yyyy/MM/dd hh:mm:ss");
        }
    }
}

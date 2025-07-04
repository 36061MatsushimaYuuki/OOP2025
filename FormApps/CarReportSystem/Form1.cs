using System.ComponentModel;
using System.Net.Http.Headers;
using System.Windows.Forms;
using static CarReportSystem.CarReport;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //�J�[���|�[�g�Ǘ��p���X�g
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        public Form1() {
            InitializeComponent();
            dgvRecord.DataSource = listCarReports;
        }

        //�R���X�g���N�^�̌�ɌĂ΂��
        private void Form1_Load(object sender, EventArgs e) {
            InputItemsAllClear();
        }

        private void btPicOpen_Click(object sender, EventArgs e) {
            ofdPicFileOpen.Filter = "�摜�t�@�C�� (*.jpg;.jpeg;*.png;*.webp)|*.jpg;.jpeg;*.png;*.webp";
            if (ofdPicFileOpen.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        private void btPicDelete_Click(object sender, EventArgs e) {
            if (pbPicture.Image != null && MessageBox.Show("�{���ɉ摜���폜���܂����H", "���惌�|�[�g�Ǘ��V�X�e��", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                pbPicture.Image = null;
            }
        }

        //�ǉ��{�^���̃C�x���g�n���h��
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
            if (author != "" && cbAuthor.Items.IndexOf(author) == -1) { //!cbCarName.Items.Contains(author)
                cbAuthor.Items.Add(author);
            }
        }

        //�Ԗ��̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbCarName(string carName) {
            //�l����łȂ����A���ɓo�^�ς݂��m�F
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
            if (dgvRecord.CurrentRow != null && dgvRecord.Rows.Count > 0) {
                dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
                cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
                setRadioButtonMaker((MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
                cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
                tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
                pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
            }
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
            if (dgvRecord.CurrentRow != null && dgvRecord.Rows.Count > 0) {
                if (MessageBox.Show("���݂̃��R�[�h���C�����܂����H", "���惌�|�[�g�Ǘ��V�X�e��", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
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

        //�폜�{�^���̃C�x���g�n���h��
        private void btRecordDelete_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow != null && dgvRecord.Rows.Count > 0) {
                if (MessageBox.Show("�{���Ɍ��݂̃��R�[�h���폜���܂����H", "���惌�|�[�g�Ǘ��V�X�e��", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) {
                    listCarReports.RemoveAt(dgvRecord.CurrentRow.Index);
                    InputItemsAllClear();
                }
            }
        }
    }
}

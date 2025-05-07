using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitConverter
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btChange_Click(object sender, EventArgs e) {
            int num1;
            if (int.TryParse(tbNum1.Text, out num1)) {
                double num2 = num1 * 0.3048;
                tbNum2.Text = num2.ToString();
            } else {
                DialogResult result = MessageBox.Show("値が入力されていないか、形式が正しくありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btCalc_Click(object sender, EventArgs e) {
            if(nudNum2.Value != 0) {
                nudAnswer.Value = Math.Floor(nudNum1.Value / nudNum2.Value);
                nudAmari.Value = nudNum1.Value % nudNum2.Value;
            } else {
                DialogResult result = MessageBox.Show("0で除算はできません。", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}

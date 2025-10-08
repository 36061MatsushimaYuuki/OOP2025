using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrismObservesSample {
    class MainWindowViewModel : BindableBase {
        //コンストラクタ
        public MainWindowViewModel() {
            SumCommand = new DelegateCommand(ExecuteSum, CanExecuteSum)
                .ObservesProperty(() => Input1)
                .ObservesProperty(() => Input2); //プロパティチェック
        }

        //足し算の処理
        private void ExecuteSum() {
            Result = (int.Parse(Input1) + int.Parse(Input2)).ToString();
        }

        //判定の処理
        private bool CanExecuteSum() {
            return int.TryParse(Input1, out _) && int.TryParse(Input2, out _);
        }

        //プロパティ
        private string _input1 = "";
        public string Input1 {
            get => _input1;
            set => SetProperty(ref _input1, value);
        }

        private string _input2 = "";
        public string Input2 {
            get => _input2;
            set => SetProperty(ref _input2, value);
        }

        private string _result = "";
        public string Result {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public DelegateCommand SumCommand { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleUnitConverter {
    public class MainWindowViewModel : BindableBase {

        //フィールド
        private double metricValue;
        private double imperialValue;

        //▲で呼ばれるコマンド
        public ICommand ImperialUnitToMetric { get; private set; }

        //▼で呼ばれるコマンド
        public ICommand MetricToImperialUnit { get; private set; }

        //上のComboBoxで選択されている値
        public MetricUnit CurrentMetricUnit { get; set; }

        //下のComboBoxで選択されている値
        public ImperialUnit CurrentImperialUnit { get; set; }

        //プロパティ
        public double MetricValue {
            get => metricValue;
            set => SetProperty(ref metricValue, value);
        }

        public double ImperialValue {
            get => imperialValue;
            set => SetProperty(ref imperialValue, value);
        }

        //コンストラクタ
        public MainWindowViewModel() {
            CurrentMetricUnit = MetricUnit.Units.First();
            CurrentImperialUnit = ImperialUnit.Units.First();
            MetricToImperialUnit = new DelegateCommand(
                () => ImperialValue =
                    CurrentImperialUnit.FromMetricUnit(CurrentMetricUnit, MetricValue));
            ImperialUnitToMetric = new DelegateCommand(
                () => MetricValue = 
                    CurrentMetricUnit.FromImperialUnit(CurrentImperialUnit, ImperialValue));
        }
    }
}

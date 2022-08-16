using Microcharts;
using System.Windows.Input;

namespace UI_by_Vedernykov.Helpers
{
    public class ChartItem
    {
        public string Title { get; set; } = string.Empty;

        public Chart Chart { get; set; }

        public int index { get; set; }

        public ICommand RefreshChartCommand { get; set; }
    }
}

using Microcharts;
using Prism.Navigation;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UI_by_Vedernykov.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace UI_by_Vedernykov.ViewModels
{
    public class ChartsAndGraphsViewViewModel : BaseViewModel
    {
        private readonly List<KeyValuePair<string, SKColor>> pairMonthColor = new()
        {
            new("January", SKColor.Parse("#266489")),
            new("February", SKColor.Parse("#68B9C0")),
            new("March", SKColor.Parse("#90D585")),
            new("April", SKColor.Parse("#F3C151")),
            new("May", SKColor.Parse("#F37F64")),
            new("June", SKColor.Parse("#424856")),
            new("July", SKColor.Parse("#8F97A4")),
            new("August", SKColor.Parse("#DAC096")),
            new("September", SKColor.Parse("#76846E")),
            new("October", SKColor.Parse("#DABFAF")),
            new("November", SKColor.Parse("#A65B69")),
            new("December", SKColor.Parse("#97A69D")),
        };

        private ICommand _refreshChartCommand;

        public ChartsAndGraphsViewViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _refreshChartCommand = new AsyncCommand<int>(OnRefreshChartCommandAsync, allowsMultipleExecutions: false);

            InitCharts();
        }

        #region -- Public properties --

        private List<ChartItem> _charts;
        public List<ChartItem> Charts
        {
            get => _charts;
            set => SetProperty(ref _charts, value);
        }

        #endregion

        #region -- Private helpers --

        private Task OnRefreshChartCommandAsync(int index)
        {
            _charts[index].Chart.Entries = GenerateChartEntriesData();

            return Task.CompletedTask;
        }

        private List<ChartEntry> GenerateChartEntriesData()
        {
            int value;
            var random = new Random();

            var entries = new List<ChartEntry>();

            for (int i = 0; i < 12; i++)
            {
                entries.Add(new ChartEntry(value = random.Next(-300, 700))
                {
                    Label = pairMonthColor[i].Key,
                    ValueLabel = value.ToString(),
                    Color = pairMonthColor[i].Value,
                });
            }

            return entries;
        }

        private void InitCharts()
        {
            int i = 0;

            _charts = new()
            {
                new()
                {
                    Title = "BarChart",
                    Chart = new BarChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "DonutChart",
                    Chart = new DonutChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "LineChart, LineMode = None",
                    Chart = new LineChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                        LineSize = 8,
                        LineMode = LineMode.None,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "LineChart, LineMode = Spline",
                    Chart = new LineChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                        LineSize = 8,
                        LineMode = LineMode.Spline,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "LineChart, LineMode = Straight",
                    Chart = new LineChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                        LineSize = 8,
                        LineMode = LineMode.Straight,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "PieChart",
                    Chart = new PieChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "PointChart",
                    Chart = new PointChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "RadarChart",
                    Chart = new RadarChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
                new()
                {
                    Title = "RadialGaugeChart",
                    Chart = new RadialGaugeChart()
                    {
                        Entries = GenerateChartEntriesData(),
                        LabelTextSize = 42,
                    },
                    index = i++,
                    RefreshChartCommand = _refreshChartCommand,
                },
            };
        }

        #endregion
    }
}

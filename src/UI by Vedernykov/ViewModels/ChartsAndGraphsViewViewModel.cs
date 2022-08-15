using Microcharts;
using Prism.Navigation;
using SkiaSharp;
using System;
using System.Collections.Generic;

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

        public ChartsAndGraphsViewViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            InitCharts();
        }

        #region -- Public properties --

        private Chart _barChart;
        public Chart BarChart
        {
            get => _barChart;
            set => SetProperty(ref _barChart, value);
        }

        private Chart _donutChart;
        public Chart DonutChart
        {
            get => _donutChart;
            set => SetProperty(ref _donutChart, value);
        }

        private Chart _lineChart;
        public Chart LineChart
        {
            get => _lineChart;
            set => SetProperty(ref _lineChart, value);
        }

        private Chart _pieChart;
        public Chart PieChart
        {
            get => _pieChart;
            set => SetProperty(ref _pieChart, value);
        }

        private Chart _pointChart;
        public Chart PointChart
        {
            get => _pointChart;
            set => SetProperty(ref _pointChart, value);
        }

        private Chart _radarChart;
        public Chart RadarChart
        {
            get => _radarChart;
            set => SetProperty(ref _radarChart, value);
        }

        private Chart _radialGaugeChart;
        public Chart RadialGaugeChart
        {
            get => _radialGaugeChart;
            set => SetProperty(ref _radialGaugeChart, value);
        }

        #endregion

        #region -- Private helpers --

        private List<ChartEntry> GenerateChartEntriesData()
        {
            int value;
            var random = new Random();

            var entries = new List<ChartEntry>();

            for (int i = 0; i < 12; i++)
            {
                entries.Add(new ChartEntry(value = random.Next(100, 700))
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
            _barChart = new BarChart()
            {
                Entries = GenerateChartEntriesData(),
                LabelTextSize = 42,
            };

            _donutChart = new DonutChart()
            {
                Entries = GenerateChartEntriesData(),
                LabelTextSize = 42,
            };

            _lineChart = new LineChart()
            {
                Entries = GenerateChartEntriesData(),
                LabelTextSize = 42,
            };

            _pieChart = new PieChart()
            {
                Entries = GenerateChartEntriesData(),
                LabelTextSize = 42,
            };

            _pointChart = new PointChart()
            {
                Entries = GenerateChartEntriesData(),
                LabelTextSize = 42,
            };

            _radarChart = new RadarChart()
            {
                Entries = GenerateChartEntriesData(),
                LabelTextSize = 42,
            };

            _radialGaugeChart = new RadialGaugeChart()
            {
                Entries = GenerateChartEntriesData(),
                LabelTextSize = 42,
            };
        }

        #endregion
    }
}

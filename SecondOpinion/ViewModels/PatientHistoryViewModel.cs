using System;
using SecondOpinion.Models;
using System.Reactive.Linq;
using SecondOpinion.Services.Api;
using System.Collections.Generic;
using ReactiveUI;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Linq;

namespace SecondOpinion.ViewModels {

    public class HistoryItem {

        public string Title {
            get;
            private set;
        }

        public PlotModel Chart {
            get;
            private set;
        }

        public HistoryItem (PlotModel chart, string title) {
            Chart = chart;
            Title = title;
        }
    }

    public class Item {
        public DateTime X { get; set; }
        public double Y { get; set; }
    }

    public class PatientHistoryViewModel : BaseViewModel {

        private const string DATE_TIME_FORMAT = "";

        public string PatientName {
            get;
            set;
        }

        private ReactiveList<HistoryItem> _graphItems;
        public ReactiveList<HistoryItem> GraphItems {
            get => _graphItems;
            private set => this.RaiseAndSetIfChanged(ref _graphItems , value);
        }

        public PatientHistoryViewModel() {
            _graphItems = new ReactiveList<HistoryItem>(); // empty initial list
        }

        public async Task GetPatientData() {
            try {
                var patientData = await ApiCoordinator.GetPatientData(PatientName).ConfigureAwait(true);
                var list = new List<HistoryItem>();
                foreach (KeyValuePair<string , List<PatientData>> entry in patientData) {
                    var chart = new PlotModel {
                        Title = "" ,
                        PlotType = PlotType.XY ,
                        PlotAreaBorderThickness = new OxyThickness(1.5 , 0.0 , 0.0 , 1.5) ,
                        PlotAreaBorderColor = OxyColor.FromRgb(0xE1 , 0xE1 , 0xE1)
                    };
                    var dates = entry.Value.Select(i => DateTime.Parse(i.HappenedAt));
                    var min = dates.Min(); //DateTime.Parse("08/12/2019 09:00:00");
                    var max = dates.Max(); //DateTime.Parse("08/12/2019 19:00:00");
                    chart.Axes.Add(new LinearAxis {
                        Position = AxisPosition.Left ,
                        MajorGridlineStyle = LineStyle.Solid ,
                        MajorGridlineColor = OxyColor.FromRgb(0xE1 , 0xE1 , 0xE1) ,
                        MinorGridlineStyle = LineStyle.None,
                        AxislineThickness = 1 ,
                        TickStyle = TickStyle.Crossing ,
                        TicklineColor = OxyColor.FromRgb(0xE1 , 0xE1 , 0xE1)
                    });
                    chart.Axes.Add(new DateTimeAxis {
                        Position = AxisPosition.Bottom ,
                        Minimum = DateTimeAxis.ToDouble(min) ,
                        Maximum = DateTimeAxis.ToDouble(max) ,
                        StringFormat = "dd/MM" ,
                        IntervalType = DateTimeIntervalType.Auto,
                        MajorGridlineStyle = LineStyle.None ,
                        MinorGridlineStyle = LineStyle.None ,
                        TickStyle = TickStyle.Outside ,
                        TicklineColor = OxyColor.FromRgb(0xE1 , 0xE1 , 0xE1) ,
                        IsZoomEnabled = true ,
                        //MajorStep = 1.0 , // 1/24 = 1 hour, 1/24/2 = 30 minutes,
                    });
                    chart.Series.Add(CreateLineSeries(entry.Value , dates));
                    list.Add(new HistoryItem(chart , entry.Key));
                }
                GraphItems.AddRange(list);

            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        }

        private LineSeries CreateLineSeries (List<PatientData> data, IEnumerable<DateTime> dates) {
            var serie = new LineSeries {
                DataFieldX = "X",
                DataFieldY = "Y",
                TextColor = OxyColor.FromRgb(0x0 , 0x0 , 0x0),
                Color = OxyColor.FromRgb(0x5A, 0xC8, 0xFA),
                BrokenLineColor = OxyColors.Automatic,
                LineStyle = LineStyle.Solid,
                MarkerType =  MarkerType.Diamond,
                MarkerSize = 5,
                StrokeThickness = 3
            };
            
            List<Item> ii = new List<Item>();
            //var time = DateTime.Parse("08/12/2019 10:00:00");
            //ii.Add(new Item { X = time , Y = 101.0 });
            //ii.Add(new Item { X = time.AddHours(4) , Y = 125.0 });
            //ii.Add(new Item { X = time.AddHours(8) , Y = 110.0 });
            foreach (var item in data) {
                try {
                    ii.Add(new Item { X = DateTime.Parse(item.HappenedAt) , Y = Double.Parse(item.Data) });
                } catch (ArgumentNullException) {
                    continue;
                }
            }
            serie.ItemsSource = ii;
            return serie;
        }
    }
}

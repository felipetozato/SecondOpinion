using System;
using System.Reactive.Subjects;
using SecondOpinion.Models;
using System.Reactive.Linq;
using SecondOpinion.Services.Api;
using System.Collections.Generic;
using Microcharts;
using ReactiveUI;
using System.Globalization;
using SkiaSharp;
using System.Collections;
using System.Threading.Tasks;

namespace SecondOpinion.ViewModels {

    public class HistoryItem {

        public Chart Chart {
            get;
            private set;
        }

        public HistoryItem (Chart chart) {
            Chart = chart;
        }
    }

    public class PatientHistoryViewModel : BaseViewModel {

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
            var patientData = await ApiCoordinator.GetPatientData(PatientName).ConfigureAwait(true);
            var list = new List<HistoryItem>();
            foreach (KeyValuePair<string , List<PatientData>> entry in patientData) {
                var chart = new LineChart {
                    Entries = CreateEntries(entry.Value),
                    LineSize = 1.0f ,
                    LineMode = LineMode.Straight
                };
                list.Add(new HistoryItem(chart));
            }
            GraphItems.AddRange(list);
        }

        private Entry[] CreateEntries (List<PatientData> data) {
            var entries = new Entry[data.Count];
            var entryIndex = 0;
            data.ForEach(item => {
                var value = float.Parse(item.Data , CultureInfo.InvariantCulture.NumberFormat);
                entries[entryIndex] = new Entry(value) {
                    ValueLabel = item.Data ,
                    Color = SKColor.Parse("#4287f5") ,
                    TextColor = SKColor.Parse("#f0ac2e") ,
                    Label = "The Label"
                };
                entryIndex++;
            });
            return entries;
        }
    }
}

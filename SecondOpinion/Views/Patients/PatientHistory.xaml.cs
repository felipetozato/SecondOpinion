using SecondOpinion.ViewModels;

namespace SecondOpinion.Views.Patients {
    public partial class PatientHistory : ContentPageBase<PatientHistoryViewModel> {

        private string patientName;

        public PatientHistory(string name) {
            InitializeComponent ();
            patientName = name;
            ViewModel.PatientName = patientName;
            GraphList.ItemsSource = ViewModel.GraphItems;
        }

        protected override void OnAppearing () {
            base.OnAppearing();
            Title = $"Historico: {patientName}";
            ViewModel.GetPatientData();
        }
    }
}

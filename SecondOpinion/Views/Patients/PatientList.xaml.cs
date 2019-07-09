using System;
using System.Collections.Generic;
using SecondOpinion.Models;
using SecondOpinion.ViewModels;
using Xamarin.Forms;

namespace SecondOpinion.Views.Patients {
    public partial class PatientList : ContentPageBase<PatientListViewModel> {

        public PatientList () {
            InitializeComponent();
            PatientListView.BindingContext = ViewModel.PatientList;
            PatientListView.ItemsSource = ViewModel.PatientList;
            Init();
        }

        protected override void OnAppearing () {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine("OnAppearing");
            ViewModel.LoadPatients();
        }

        private void Init() {
            PatientListView.ItemTapped += (sender, e) => {
                System.Diagnostics.Debug.WriteLine(e.Item);
                var patient = e.Item as Patient;
                var detailPage = new PatientHistory(patient.Name);
                Navigation.PushAsync(detailPage , true);
            };
        }
    }
}

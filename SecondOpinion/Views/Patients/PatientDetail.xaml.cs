using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SecondOpinion.Models;

namespace SecondOpinion.Views.Patients {
    public partial class PatientDetail : ContentPage {

        private Patient patient;

        public PatientDetail () {
            InitializeComponent();
        }

        public PatientDetail(Patient patient) {
            InitializeComponent();
            this.patient = patient;
            Init();
        }

        void Init() {
            Title = patient.Name;
            PatientInfoLabel.Text = patient.RawInfo.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecondOpinion.Models;
using SecondOpinion.Services.Api;
using System.Linq;
using ReactiveUI;

namespace SecondOpinion.ViewModels {
    public class PatientListViewModel : BaseViewModel {

        private ReactiveList<Patient> _patientList;
        public ReactiveList<Patient> PatientList {
            get => _patientList;
            private set => this.RaiseAndSetIfChanged(ref _patientList, value);
        }

        public PatientListViewModel () {
            PatientList = new ReactiveList<Patient>();
        }

        public async Task LoadPatients () {
            List<Patient> patients = await ApiCoordinator.GetAllPatients();
            PatientList.Clear();
            PatientList.AddRange(patients);
        }
    }
}

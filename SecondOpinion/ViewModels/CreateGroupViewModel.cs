using System;
using System.Reactive;
using System.Collections.Generic;
using SecondOpinion.Models;
using ReactiveUI;
using System.Threading.Tasks;
using SecondOpinion.Services.Api;
using System.Reactive.Concurrency;

namespace SecondOpinion.ViewModels {
    public class CreateGroupViewModel : BaseViewModel {

        public IList<UserContact> SelectedUsers {
            get;
            set;
        }

        public string GroupName {
            get;
            set;
        }

        public ReactiveCommand<Unit, Unit> CreateGroup {
            get;
            private set;
        }

        public delegate void DialogEvent (Dialog dialog);

        public event DialogEvent GoToDialogScreen;

        public CreateGroupViewModel (IScheduler scheduler = null) : base() {
            CreateGroup = ReactiveCommand.CreateFromTask(HandleCreateGroup, null, scheduler);
        }

        protected virtual void OnGoToDialogScreen(Dialog dialog) {
            GoToDialogScreen?.Invoke(dialog);
        }

        private async Task HandleCreateGroup() {
            //Validate group name
            if (string.IsNullOrWhiteSpace(GroupName)) {
                return;
            }
            var newDialog = await ApiCoordinator.CreateMessageGroup(GroupName , SelectedUsers);
            //Save in the future

            OnGoToDialogScreen(newDialog);
        } 
    }
}

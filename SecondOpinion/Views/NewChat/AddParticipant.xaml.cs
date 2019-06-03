using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using SecondOpinion.ViewModels;
using Xamarin.Forms;
using SecondOpinion.Views.Chat;
using Plugin.Media;

namespace SecondOpinion.Views.NewChat {
    public partial class AddParticipant : ContentPageBase<UserListViewModel> {

        private INavigation parent;

        private readonly CreateGroupViewModel groupViewModel;

        public AddParticipant (INavigation parentNavigation) : base() {
            parent = parentNavigation;
            groupViewModel = new CreateGroupViewModel(null);
            InitializeComponent();
            Init();
            SubscribeToViewModel();
            ViewModel.Populate();
            ContactList.ItemsSource = ViewModel.ContactList;
            ContactList.BindingContext = ViewModel.ContactList;
        }

        private void Init () {
            ContactList.ItemTapped += ContactList_ItemSelected;
            CreateButton.Clicked += Next_Clicked;
            groupViewModel.GoToDialogScreen += async (dialog) => {
                await parent.PopModalAsync(false);
                var page = new ChatPage(dialog);
                await parent.PushAsync(page, true);
            };
            GroupNameField.TextChanged += (object sender , TextChangedEventArgs e) => {
                groupViewModel.GroupName = e.NewTextValue;
            };
            AddPhoto.Clicked += AddPhoto_Clicked;
        }

        void Next_Clicked (object sender , EventArgs e) {
            groupViewModel.CreateGroup.Execute(ViewModel.GetSelectedUsers()).Subscribe();
        }


        void ContactList_ItemSelected (object sender , ItemTappedEventArgs e) {
            var item = e.Item as UserListItem;
            if (item.Selected) {
                item.DeSelect();
            } else {
                item.Select();
            }
        }

        private void SubscribeToViewModel () {
            this.WhenActivated(disposables => {
                ContactList.ItemsSource = ViewModel.ContactList;
                //ViewModel.ContactList.Changed
                         //.Where(list => list != null && list.NewItems.Count > 0)
                         //.ObserveOn(RxApp.MainThreadScheduler)
                         //.Subscribe(chatList => {
                         //    //ContactList.ItemsSource = ViewModel.ContactList;
                         //    //ContactList.BindingContext = ViewModel.ContactList;
                         //});
            });
        }

        async void AddPhoto_Clicked (object sender , EventArgs e) {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported) {
                DisplayAlert("No Gallery" , ":( No gallery available." , "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CompressionQuality = 90
            });

            AddPhoto.Source = ImageSource.FromFile(file.Path);
        }

    }
}

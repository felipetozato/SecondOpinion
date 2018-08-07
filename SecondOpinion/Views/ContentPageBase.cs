using System;

using Xamarin.Forms;
using ReactiveUI.XamForms;
using SecondOpinion.ViewModels;
using System.Reactive.Disposables;

namespace SecondOpinion.Views {
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

        protected ContentPageBase() {
            ViewModel = (TViewModel) Activator.CreateInstance(typeof(TViewModel));
        }

        protected ContentPageBase(params object[] viewModelParameters) {
            ViewModel = (TViewModel) Activator.CreateInstance(typeof(TViewModel), viewModelParameters);
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            SubscriptionDisposables.Clear();
        }
    }
}


using System;

using Xamarin.Forms;

namespace SecondOpinion.Views {
    public class ContentPageBase : ContentPage {
        public ContentPageBase () {
            Content = new StackLayout {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}


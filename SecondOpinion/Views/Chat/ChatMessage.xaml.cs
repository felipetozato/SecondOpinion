using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SecondOpinion.Views.Chat {
    public partial class ChatMessage : ViewCell {
        
        public static readonly BindableProperty ContentProperty =
            BindableProperty.Create ("ContentLabel", typeof(string), typeof(ChatMessage), "");

        public string Content {
            get { return (string) GetValue (ContentProperty); }
            set { SetValue (ContentProperty, value); }
        }

        public ChatMessage () {
            InitializeComponent();
        }
    }
}

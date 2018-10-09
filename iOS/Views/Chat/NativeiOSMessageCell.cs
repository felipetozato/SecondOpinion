using System;
using System.Linq;

using Foundation;
using SecondOpinion.Views.Chat;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SecondOpinion.iOS.Views.Chat
{
    public partial class NativeiOSMessageCell : UITableViewCell, INativeElementView
    {
        public static readonly NSString Key = new NSString("NativeiOSMessageCell");
        public static readonly UINib Nib;

        public UILabel MessageContent => _MessageContent;

        public ChatMessage ChatCell {
            get;
            private set;
        }
        public Element Element => ChatCell;

        public static NativeiOSMessageCell Create(ChatMessage chatMessage, NSObject owner) {
            var cell = Nib.Instantiate(owner, null).First() as NativeiOSMessageCell;
            cell.ChatCell = chatMessage;
            return cell;
        }

        static NativeiOSMessageCell() {
            Nib = UINib.FromName("NativeiOSMessageCell", NSBundle.MainBundle);
        }

        protected NativeiOSMessageCell(IntPtr handle) : base(handle) {
            // Note: this .ctor should not contain any initialization logic.
        }

        internal void UpdateCell(ChatMessage nativeCell) {
            this.MessageContent.Text = nativeCell.Content;
            this.ContentView.BackgroundColor = nativeCell.MessageColor.ToUIColor();
            var margin = nativeCell.View.Margin;
        }
    }
}

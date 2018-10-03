using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using SecondOpinion.iOS.Views.Chat;
using SecondOpinion.Views.Chat;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ChatMessage), typeof(SecondOpinion.iOS.Renders.MessageiOSViewCellRender))]
namespace SecondOpinion.iOS.Renders
{
    
    class MessageiOSViewCellRender : ViewCellRenderer
    {
        NativeiOSMessageCell cell;

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv) {

            var nativeCell = (ChatMessage) item;

            cell = reusableCell as NativeiOSMessageCell;
            if (cell == null) {
                cell = NativeiOSMessageCell.Create(nativeCell, tv);
            } else {
                cell.ChatCell.PropertyChanged -= OnNativeCellPropertyChanged;
            }

            nativeCell.PropertyChanged += OnNativeCellPropertyChanged;
            cell.UpdateCell(nativeCell);
            return cell;
        }

        private void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e) {
            var nativeCell = (ChatMessage) sender;
            if (e.PropertyName == ChatMessage.ContentProperty.PropertyName) {
                cell.MessageContent.Text = nativeCell.Content;
            } else if (e.PropertyName == ChatMessage.MessageColorProperty.PropertyName) {
                cell.ContentView.BackgroundColor = nativeCell.MessageColor.ToUIColor();
            }
        }
    }
}
using UI_by_Vedernykov.Controls;
using UI_by_Vedernykov.iOS.Renderers.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomNoBorderEntry), typeof(CustomNoBorderEntryRenderer))]
namespace UI_by_Vedernykov.iOS.Renderers.Controls
{
    public class CustomNoBorderEntryRenderer : EntryRenderer
    {

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }

        protected override bool OnShouldReturn(UITextField view)
        {
            Control.ResignFirstResponder();
            ((IEntryController)Element).SendCompleted();

            if (Element != null && Element.ReturnType == ReturnType.Next)
            {
                var shouldSetFocusToNextElement = true;

                if (Element is CustomNoBorderEntry entry)
                {
                    shouldSetFocusToNextElement = entry.ShouldSetFocusToNextElement;
                }

                if (shouldSetFocusToNextElement)
                {
                    FocusSearch(true);
                }
            }

            return false;
        }

        #endregion
    }
}
using Android.Content;
using Android.Views.InputMethods;
using Android.Views;
using Android.Widget;
using UI_by_Vedernykov.Controls;
using UI_by_Vedernykov.Droid.Renderers.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Text;

[assembly: ExportRenderer(typeof(CustomNoBorderEntry), typeof(CustomNoBorderEntryRenderer))]
namespace UI_by_Vedernykov.Droid.Renderers.Controls
{
    public class CustomNoBorderEntryRenderer : EntryRenderer, TextView.IOnEditorActionListener
    {
        public CustomNoBorderEntryRenderer(Context context)
            : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(((Color)CustomNoBorderEntry.BackgroundColorProperty.DefaultValue).ToAndroid());
                Control.SetPadding(0, 0, 0, 0);
            }
        }

        bool TextView.IOnEditorActionListener.OnEditorAction(TextView v, ImeAction actionId, KeyEvent e)
        {
            var currentActionId = (ImeAction)System.Enum.Parse(typeof(ImeAction), Element.ReturnType.ToString());

            if (actionId == ImeAction.Done || actionId == currentActionId || (actionId == ImeAction.ImeNull && e.KeyCode == Keycode.Enter && e.Action == KeyEventActions.Up))
            {
                global::Android.Views.View nextFocus = null;

                if (currentActionId == ImeAction.Next)
                {
                    nextFocus = FocusSearch(v, FocusSearchDirection.Forward);
                }

                if (nextFocus != null)
                {
                    var shouldSetFocusToNextElement = true;

                    if (Element is CustomNoBorderEntry entry)
                    {
                        shouldSetFocusToNextElement = entry.ShouldSetFocusToNextElement;
                    }

                    if (shouldSetFocusToNextElement)
                    {
                        nextFocus.RequestFocus();

                        if (!nextFocus.OnCheckIsTextEditor())
                        {
                            Context.HideKeyboard(v);
                        }
                    }
                }
                else
                {
                    EditText.ClearFocus();
                    Context.HideKeyboard(v);
                }

                ((IEntryController)Element).SendCompleted();
            }

            return true;
        }

        #endregion
    }
}
using Xamarin.Forms;

namespace UI_by_Vedernykov.Controls.StateContainer
{
    [ContentProperty("Content")]
    public class StateCondition : View
    {
        public object? State { get; set; }

        public object? NotState { get; set; }

        public View Content { get; set; }
    }
}

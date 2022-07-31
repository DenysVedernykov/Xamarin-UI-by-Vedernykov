using System.Windows.Input;
using UI_by_Vedernykov.ENums;
using UI_by_Vedernykov.Interfaces;

namespace UI_by_Vedernykov.Helpers
{
    public class MenuItem : ITappable
    {
        public EPages State { get; set; }

        public string Title { get; set; }

        public ICommand TapCommand { get; set; }
    }
}

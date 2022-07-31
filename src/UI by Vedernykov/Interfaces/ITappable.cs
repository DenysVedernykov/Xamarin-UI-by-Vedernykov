using System.Windows.Input;

namespace UI_by_Vedernykov.Interfaces
{
    public interface ITappable
    {
        ICommand? TapCommand { get; set; }
    }
}

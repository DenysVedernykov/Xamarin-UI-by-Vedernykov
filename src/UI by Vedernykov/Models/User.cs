using UI_by_Vedernykov.Interfaces;

namespace UI_by_Vedernykov.Models
{
    public class User : IBaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}

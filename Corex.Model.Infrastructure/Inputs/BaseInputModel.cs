namespace Corex.Model.Infrastructure
{
    public abstract class BaseInputModel : IInputModel
    {
        public string SearchText { get; set; }
        public bool? IsActive { get; set; }
    }
}

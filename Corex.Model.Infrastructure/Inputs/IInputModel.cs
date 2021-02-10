namespace Corex.Model.Infrastructure
{
    public interface IInputModel
    {
        string SearchText { get; set; }
        bool? IsActive { get; set; }
    }
}

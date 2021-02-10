namespace Corex.Model.Infrastructure
{
    public interface IPagerInputModel : IInputModel
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string SortColumn { get; set; }
        bool SortDescending { get; set; }
    }
}

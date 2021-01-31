namespace Corex.Model.Infrastructure
{
    public interface IPagerInputModel
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string SearchText { get; set; }
        string SortColumn { get; set; }
        bool SortDescending { get; set; }
        bool? IsActive { get; set; }
    }

}

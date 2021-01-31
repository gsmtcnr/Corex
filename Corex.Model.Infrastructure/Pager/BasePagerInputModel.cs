namespace Corex.Model.Infrastructure
{
    public abstract class BasePagerInputModel : IPagerInputModel
    {
        public BasePagerInputModel()
        {
            PageSize = int.MaxValue;
            PageNumber = 1;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        private string p_SortColumn = null;
        public string SortColumn
        {
            get => p_SortColumn ?? "Position";
            set => p_SortColumn = value;
        }
        private bool? p_SortDescending = null;
        public bool SortDescending
        {
            get => p_SortDescending ?? true;
            set => p_SortDescending = value;
        }
        public bool? IsActive { get; set; }
    }
}

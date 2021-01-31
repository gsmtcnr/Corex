namespace Corex.Model.Derived.PageModel
{
    public abstract class BaseSelectListItem : ISelectListItem
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}

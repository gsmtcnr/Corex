namespace Corex.Model.Infrastructure
{
    public abstract class BaseSelectListItem : ISelectListItem
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}

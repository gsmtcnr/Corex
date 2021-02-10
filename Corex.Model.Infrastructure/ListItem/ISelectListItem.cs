namespace Corex.Model.Infrastructure
{
    public interface ISelectListItem<TKey>
    {
        public TKey Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}

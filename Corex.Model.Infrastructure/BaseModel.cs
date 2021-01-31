using System;

namespace Corex.Model.Infrastructure
{
    public abstract class BaseModel<TKey> : IModel<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool IsDeleted { get; set; }
        public int Position { get; set; }
    }
}

﻿namespace Corex.Model.Infrastructure
{
    public interface ISelectListItem
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
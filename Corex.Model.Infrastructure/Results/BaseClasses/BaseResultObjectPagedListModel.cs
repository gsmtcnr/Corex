using PagedList.Core;
using System.Collections.Generic;
using System.Linq;

namespace Corex.Model.Infrastructure
{
    public abstract class BaseResultObjectPagedListModel<TData> : BaseResultModel, IResultObjectPagedListModel<TData>
         where TData : class, new()
    {
        public BaseResultObjectPagedListModel()
        {
            IsSuccess = false;
            Data = new List<TData>();
        }
        public BaseResultObjectPagedListModel(IPagedList metaData, List<TData> data)
        {
            Data = data;
            TotalItemCount = metaData.TotalItemCount;
            PageCount = metaData.PageCount;
            HasNextPage = metaData.HasNextPage;
            HasPreviousPage = metaData.HasPreviousPage;
            PageNumber = metaData.PageNumber;
            IsFirstPage = metaData.IsFirstPage;
            IsLastPage = metaData.IsLastPage;
        }
        public BaseResultObjectPagedListModel(IResultPagedListModel pagedListModel, List<TData> data)
        {

            Data = data;
            TotalItemCount = pagedListModel.TotalItemCount;
            PageCount = pagedListModel.PageCount;
            HasNextPage = pagedListModel.HasNextPage;
            HasPreviousPage = pagedListModel.HasPreviousPage;
            PageNumber = pagedListModel.PageCount;

        }
        public override void SetResult()
        {
            if (Data == null)
            {
                IsSuccess = false;
                Message = "Data is null";
            }
            else if (Messages.Any())
            {
                IsSuccess = false;
                Message = "Messages for detail";
            }
            else if (Data.Count == 0)
            {
                IsSuccess = false;
                Message = "Data is empty";
            }
            else
                IsSuccess = true;
        }
        public List<TData> Data { get; set; }
        public int TotalItemCount { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}

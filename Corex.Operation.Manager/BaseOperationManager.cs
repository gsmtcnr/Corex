using Corex.ExceptionHandling.Derived.Validation;
using Corex.ExceptionHandling.Infrastructure.Models;
using Corex.Mapper.Infrastructure;
using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;
using Corex.Operation.Infrastructure;
using Corex.Operation.Manager.Results;
using Corex.Validation.Infrastucture;
using PagedList.Core;
using System.Collections.Generic;
using System.Linq;

namespace Corex.Operation.Manager
{
    public abstract class BaseOperationManager<TKey, TEntity, TModel> : IOperationManager<TKey, TEntity, TModel>
    where TEntity : class, IEntityModel<TKey>, new()
    where TModel : class, IModel<TKey>, new()
    {
        public string TransactionId { get; }
        public IDataOperation<TEntity, TKey> DataOperation { get; protected set; }
        public IMapping Mapper { get; protected set; }
        public abstract IDataOperation<TEntity, TKey> SetDataOperation();
        public abstract IMapping SetMapper();
        public BaseOperationManager(string tranactionId = null)
        {
            this.DataOperation = SetDataOperation();
            TransactionId = tranactionId;
            this.Mapper = SetMapper();
        }
        #region SaveChanges
        //TKey değerinin ne olacağını Concrete nesne bilir orada karar verilmeli..
        public abstract IResultObjectModel<TModel> SaveChanges(TModel dto);
        #endregion
        #region Insert 
        public abstract IValidationOperation<TModel> SetInsertValidationOperation(TModel dto);
        public virtual IResultObjectModel<TModel> Insert(TModel dto)
        {
            ResultObjectModel<TModel> resultObjectModel = new ResultObjectModel<TModel>();
            try
            {
                InsertValidationOperation(dto);
                TEntity entity = Mapper.Map<TModel, TEntity>(dto);
                entity = DataOperation.Insert(entity);
                resultObjectModel.Data = Mapper.Map<TEntity, TModel>(entity);
            }
            catch (System.Exception ex)
            {
                resultObjectModel.IsSuccess = false;
                ExceptionManager exceptionManager = new ExceptionManager(ex);

                resultObjectModel.Messages.AddRange(exceptionManager.GetMessages());
            }
            resultObjectModel.SetResult();
            return resultObjectModel;
        }

        private void InsertValidationOperation(TModel dto)
        {
            var validationOperation = SetInsertValidationOperation(dto);
            List<ValidationMessage> messages = validationOperation.GetValidationResults();
            if (messages.Any())
            {
                throw new ValidationOperationException(new ValidationExceptionModel()
                {
                    ModelName = nameof(TModel),
                    OriginalMessage = "Insert-Model not valid",
                    ValidationMessages = messages.Select(v =>
                    new ValidationExceptionMessage
                    {
                        Code = v.Code,
                        Message = v.Message
                    }).ToList()
                });
            }
        }
        #endregion
        #region Update
        public abstract IValidationOperation<TModel> SetUpdateValidationOperation(TModel dto);
        public virtual IResultObjectModel<TModel> Update(TModel dto)
        {
            ResultObjectModel<TModel> resultObjectModel = new ResultObjectModel<TModel>();
            try
            {
                UpdateValidationOperation(dto);
                TEntity entity = Mapper.Map<TModel, TEntity>(dto);
                entity = DataOperation.Update(entity);
                resultObjectModel.Data = Mapper.Map<TEntity, TModel>(entity);
            }
            catch (System.Exception ex)
            {
                resultObjectModel.IsSuccess = false;
                ExceptionManager exceptionManager = new ExceptionManager(ex);
                resultObjectModel.Messages.AddRange(exceptionManager.GetMessages());
            }
            resultObjectModel.SetResult();
            return resultObjectModel;
        }
        private void UpdateValidationOperation(TModel dto)
        {
            IValidationOperation<TModel> validationOperation = SetUpdateValidationOperation(dto);
            List<ValidationMessage> messages = validationOperation.GetValidationResults();
            if (messages.Any())
            {
                throw new ValidationOperationException(new ValidationExceptionModel()
                {
                    ModelName = nameof(TModel),
                    OriginalMessage = "Update-Model not valid",
                    ValidationMessages = messages.Select(v =>
                    new ValidationExceptionMessage
                    {
                        Code = v.Code,
                        Message = v.Message
                    }).ToList()
                });
            }
        }
        #endregion
        #region Get
        public virtual IResultObjectModel<TModel> Get(TKey id)
        {
            ResultObjectModel<TModel> resultObjectModel = new ResultObjectModel<TModel>();
            try
            {
                TEntity entity = DataOperation.Get(s => s.Id.Equals(id));
                TModel dto = Mapper.Map<TEntity, TModel>(entity);
                resultObjectModel = new ResultObjectModel<TModel>(dto);
                SetNullMessage(resultObjectModel, dto);
            }
            catch (System.Exception ex)
            {
                resultObjectModel.IsSuccess = false;
                ExceptionManager exceptionManager = new ExceptionManager(ex);
                resultObjectModel.Messages.AddRange(exceptionManager.GetMessages());
            }
            resultObjectModel.SetResult();
            return resultObjectModel;
        }

        private static void SetNullMessage(ResultObjectModel<TModel> resultObjectModel, TModel dto)
        {
            if (dto == null)
                resultObjectModel.Messages.Add(new MessageItem
                {
                    Code = "NotFound",
                    Message = "Data is null"
                });
        }
        public virtual IResultObjectPagedListModel<TModel> GetList(IPagerInputModel pagerInputModel)
        {
            ResultObjectPagedListModel<TModel> resultObjectList = new ResultObjectPagedListModel<TModel>();
            try
            {
                IPagedList<TEntity> pagedList = DataOperation.GetPagedList<IPagerInputModel>(pagerInputModel);
                List<TModel> dtoList = Mapper.Map<List<TEntity>, List<TModel>>(pagedList.ToList());
                resultObjectList = new ResultObjectPagedListModel<TModel>(pagedList, dtoList);
            }
            catch (System.Exception ex)
            {
                resultObjectList.IsSuccess = false;
                ExceptionManager exceptionManager = new ExceptionManager(ex);
                resultObjectList.Messages.AddRange(exceptionManager.GetMessages());
            }
            resultObjectList.SetResult();
            return resultObjectList;
        }
        #endregion
        #region Delete
        public abstract IValidationOperation<TModel> SetDeleteValidationOperation(TModel dto);
        public virtual IResultModel Delete(TKey id)
        {
            IResultModel resultModel = new ResultModel();
            IResultObjectModel<TModel> getResult = Get(id);
            if (getResult.IsSuccess)
                resultModel = Delete(getResult.Data);
            else
                resultModel.Messages.AddRange(getResult.Messages);

            resultModel.SetResult();
            return resultModel;
        }
        public virtual IResultModel Delete(TModel dto)
        {
            ResultModel resultModel = new ResultModel();
            try
            {
                DeleteValidationOperation(dto);
                TEntity entity = Mapper.Map<TModel, TEntity>(dto);
                resultModel.IsSuccess = DataOperation.Delete(entity);
            }
            catch (System.Exception ex)
            {
                resultModel.IsSuccess = false;
                ExceptionManager exceptionManager = new ExceptionManager(ex);
                resultModel.Messages.AddRange(exceptionManager.GetMessages());
            }
            return resultModel;
        }
        private void DeleteValidationOperation(TModel dto)
        {
            IValidationOperation<TModel> validationOperation = SetDeleteValidationOperation(dto);
            if (validationOperation != null)
            {
                List<ValidationMessage> messages = validationOperation.GetValidationResults();
                if (messages.Any())
                {
                    throw new ValidationOperationException(new ValidationExceptionModel()
                    {
                        ModelName = nameof(TModel),
                        OriginalMessage = "Delete-Model not valid",
                        ValidationMessages = messages.Select(v =>
                        new ValidationExceptionMessage
                        {
                            Code = v.Code,
                            Message = v.Message
                        }).ToList()
                    });
                }
            }
        }
        #endregion
    }
}

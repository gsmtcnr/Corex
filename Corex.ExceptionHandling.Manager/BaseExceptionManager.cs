using Corex.ExceptionHandling.Infrastructure;
using Corex.ExceptionHandling.Manager.MessageCreators;
using Corex.ExceptionHandling.Manager.Models;
using Corex.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Corex.ExceptionHandling.Manager
{
    public abstract class BaseExceptionManager
    {
        // Resource işlemi varsa, burada yapılmalı. 
        readonly Exception _ex;
        public BaseExceptionManager(Exception ex)
        {
            _ex = ex;
        }
        public IList<IResultMessage> Messages { get; set; }
        private void AddMesage(IResultMessage resultMessage)
        {
            Messages.Add(resultMessage);
        }
        public void SetMessages()
        {
            ExceptionMessageModel messageModel = GetUFMessage(_ex);
            if (messageModel.Messages.Any())
            {
                foreach (ExceptionMessage item in messageModel.Messages)
                {
                    AddMesage((IResultMessage)item);
                }
            }
        }
        #region Private Methods
        private ExceptionMessageModel GetUFMessage(Exception exception)
        {
            if (exception is BaseException)
            {
                return GenerateUFMessageFromBaseException(exception as BaseException);
            }
            else
            {
                // Bilinmeyen bir hata geldiyse direk APP durdurabilirim.
                // Ya da acil bir SMS servisi ile kendime bildirim atabilirim.
            }
            throw new Exception();
        }
        private ExceptionMessageModel GenerateUFMessageFromBaseException(BaseException baseException)
        {
            return GetExceptionMessageModel((IException)baseException);
        }
        private ExceptionMessageModel GetExceptionMessageModel(IException myException)
        {
            IMessageCreator messageCreator = null;

            switch (myException)
            {
                case IValidationException validationException:
                    messageCreator = new ValidationMessageCreator();
                    break;
                case IBusinessException businessException:
                    messageCreator = new BusinessMessageCreator();
                    break;
                case IAuthenticationException authenticationException:
                    messageCreator = new AuthenticationMessageCreator();
                    break;
                case IDataException dataException:
                    messageCreator = new DataMessageCreator();
                    break;
                default:
                    break;
            }
            return messageCreator.GetExceptionMessageModel(myException);
        }
        #endregion
    }
}

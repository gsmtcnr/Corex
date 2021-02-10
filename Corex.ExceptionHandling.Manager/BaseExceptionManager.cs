using Corex.ExceptionHandling.Infrastructure;
using Corex.ExceptionHandling.Manager.MessageCreators;
using Corex.ExceptionHandling.Manager.Models;
using Corex.Model.Infrastructure;
using System;
using System.Collections.Generic;

namespace Corex.ExceptionHandling.Manager
{
    public abstract class BaseExceptionManager
    {
        // Resource işlemi varsa, burada yapılmalı. 
        private readonly Exception _ex;
        public BaseExceptionManager(Exception ex)
        {
            _ex = ex;
        }
        public List<MessageItem> GetMessages()
        {
            List<MessageItem> resultMessages = new List<MessageItem>();
            ExceptionMessageModel messageModel = GetUFMessage(_ex);
            SetMessages(resultMessages, messageModel);
            return resultMessages;
        }
        #region Private Methods
        private static void SetMessages(List<MessageItem> resultMessages, ExceptionMessageModel messageModel)
        {

                foreach (var item in messageModel.Messages)
                {
                    resultMessages.Add(new MessageItem
                    {
                        Code = item.Code,
                        Message = item.Message
                    });
                }
        }
        private static ExceptionMessageModel GetUFMessage(Exception exception)
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
        private static ExceptionMessageModel GenerateUFMessageFromBaseException(BaseException baseException)
        {
            return GetExceptionMessageModel((IException)baseException);
        }
        private static ExceptionMessageModel GetExceptionMessageModel(IException myException)
        {
            IMessageCreator messageCreator = null;
            switch (myException)
            {
                case IValidationException:
                    messageCreator = new ValidationMessageCreator();
                    break;
                case IBusinessException:
                    messageCreator = new BusinessMessageCreator();
                    break;
                case IAuthenticationException:
                    messageCreator = new AuthenticationMessageCreator();
                    break;
                case IDataException:
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

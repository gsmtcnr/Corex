using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Corex.Validation.Infrastucture
{
    public abstract class ValidationBase<T> where T : class
    {
        public T Item { get; protected set; }
        public bool IsValid { get; protected set; }
        public List<ValidationMessage> Messages { get; private set; }
        public ValidationBase(T item)
        {
            Item = item;
            Messages = new List<ValidationMessage>();
            IsValid = true;
            Validate();
        }
        public virtual void MinValueValidation(string propertyName, int value, int minValue)
        {
            if (!(value > minValue))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.MIN_VALUE,
                    Message = CodeFormat(propertyName, minValue.ToString())
                });
            }
        }
        public virtual void MinValueValidation(string propertyName, decimal value, decimal minValue)
        {
            if (!(value > minValue))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.MIN_VALUE,
                    Message = CodeFormat(propertyName, minValue.ToString())
                });
            }
        }
        public virtual void MinValueValidation(string propertyName, double value, double minValue)
        {
            if (!(value > minValue))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.MIN_VALUE,
                    Message = CodeFormat(propertyName, minValue.ToString())
                });
            }
        }
        public virtual void StringLimitValidation(string propertyName, string value, int maxLimit)
        {
            if (StringValueIsValid(value) && !StringValueLeghtIsValid(value, maxLimit))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.LIMIT_STRING,
                    Message = CodeFormat(propertyName, maxLimit)
                });
            }
        }
        public virtual void StringRequiredValidation(string propertyName, string value)
        {
            if (!StringValueIsValid(value))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.EMPTY_STRING,
                    Message = CodeFormat(propertyName)
                });
            }
        }
        public virtual void DateRequiredValidation(string propertyName, DateTime date)
        {
            if (!DateValueIsValid(date))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.EMPTY_DATE,
                    Message = CodeFormat(propertyName)
                });
            }
        }
        public virtual void DateRequiredValidation(string propertyName, string date)
        {
            if (!DateFormatIsValid(date))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.NOTVALID_VALUE,
                    Message = CodeFormat(propertyName)
                });
            }
        }
        public virtual void EmailFormatValidation(string propertyName, string email)
        {
            if (!EmailFormatIsValid(email))
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.NOTVALID_VALUE,
                    Message = CodeFormat(propertyName)
                });
            }
        }
        public virtual void RelationValidation(string propertyName, int value)
        {
            if (value == 0)
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.EMPTY_RELATION,
                    Message = CodeFormat(propertyName)
                });
            }
        }
        public virtual void DuplicateValidation(string propertyName, bool isDuplicate)
        {
            if (isDuplicate)
            {
                IsValid = false;
                Messages.Add(new ValidationMessage()
                {
                    Code = ValidationConstans.DUPLICATE_ERROR,
                    Message = CodeFormat(propertyName)
                });
            }
        }
        public virtual string CodeFormat(string property)
        {
            return property;
        }
        /// <summary>
        /// Hatayı formatlı bir şekilde geri döner
        /// Sunum katmanı resource dosyasından bu değerleri replace etmeli..
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string CodeFormat(string property, object value)
        {
            return property + "|" + value.ToString();
        }
        protected abstract void Validate();
        /// <summary>
        /// String değer boş değil ise "true" döner.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool StringValueIsValid(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// DateTime defaultvalue değil ise "true" döner.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool DateValueIsValid(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return false;
            return dateTime != default(DateTime);
        }
        /// <summary>
        /// "value" karakter sayısı "limitLenght" değerinden büyük değil ise "true" döner..
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limitLenght"></param>
        /// <returns></returns>
        public bool StringValueLeghtIsValid(string value, int limitLenght)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            int lenght = value.Length;
            return limitLenght >= lenght;
        }
        /// <summary>
        /// Verilen "e-mail" değeri formatı doğru ise "true" döner..
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EmailFormatIsValid(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (StringValueIsValid(email))
            {
                if (Regex.IsMatch(email, expresion))
                {
                    if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;
        }
        public bool DateFormatIsValid(string dateValue)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateValue, out dateTime))
                return true;
            else
                return false;
        }
    }
}

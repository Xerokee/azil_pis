using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Common
{
    public class HttpRequestResponse<T>
    {
        public T Result { get; set; }
        public List<ErrorMessage> ErrorMessages { get; set; }


        public HttpRequestResponse()
        {
            ErrorMessages = new List<ErrorMessage>();
        }

        public HttpRequestResponse(T result) : this()
        {
            Result = result;
        }

        public HttpRequestResponse<T> AddErrorMessage(string message)
        {
            ErrorMessages.Add(new ErrorMessage(message));
            return this;
        }

        //public HttpRequestResponse<T> AddErrorMessages(ErrorMessage error)
        //{
        //	ErrorMessages.Add(error);
        //	return this;

        //}
    }
}

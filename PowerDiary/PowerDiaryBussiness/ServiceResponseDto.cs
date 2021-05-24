using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerDiaryBusiness
{
    public enum ServiceResponseDtoStatus { Success = 1, Error = 0, AccessDenied = 2, NotFound = 3 }
    public class ServiceResponseDto<TInstance>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ServiceResponseDto" /> is status.
        /// 1 indicates success, 0 indicates fail
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ServiceResponseDtoStatus Status { get; set; }

        /// <summary>
        /// Gets or sets general message, can be null if not needed
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public TInstance Data { get; set; }

        /// <summary>
        /// Gets or sets Error Messages. Can be null.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IList<string> ErrorMessages { get; set; }

        public string FormattedMessage
        {
            get
            {
                if (ErrorMessages != null)
                {
                    return $"{Message}, {String.Join(", ", ErrorMessages.Select(x => x))}";
                }
                else
                {
                    return $"{Message}";
                }
            }
        }
    }

    /// <summary>
    /// Stands for service response
    /// </summary>
    public static class SR
    {
        public static ServiceResponseDto<T> Successfull<T>(this T instanse, string message = "Success")
        {
            return new ServiceResponseDto<T>
            {
                Data = instanse,
                Message = message,
                Status = ServiceResponseDtoStatus.Success
            };
        }

        public static ServiceResponseDto<T> Successfull<T>(string message)
        {
            return new ServiceResponseDto<T>
            {
                Message = message,
                Status = ServiceResponseDtoStatus.Success
            };
        }

        public static ServiceResponseDto<T> AccessDenied<T>(string message)
        {
            return new ServiceResponseDto<T>
            {
                Message = message,
                Status = ServiceResponseDtoStatus.AccessDenied
            };
        }

        public static ServiceResponseDto<T> Failed<T>(string message,
            IList<string> errors = null)
        {
            //logger.Error(message, DateTime.Now);
            return new ServiceResponseDto<T>
            {
                ErrorMessages = errors,
                Message = message,
                Status = ServiceResponseDtoStatus.Error
            };
        }

        public static ServiceResponseDto<T> NotFound<T>(this T instanse, string message = "NotFound")
        {
            return new ServiceResponseDto<T>
            {
                Data = instanse,
                Message = message,
                Status = ServiceResponseDtoStatus.NotFound
            };
        }

        public static ServiceResponseDto<T> NotFound<T>(string message)
        {
            return new ServiceResponseDto<T>
            {
                Message = message,
                Status = ServiceResponseDtoStatus.NotFound
            };
        }


        /// <summary>
        /// Error response with the data, an error message, the error list and the function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <param name="pFunction"></param>
        /// <returns></returns>
        public static ServiceResponseDto<T> Failed<T>(this T data, string message,
            IList<string> errors = null, string pFunction = "")
        {
            var failedResult = Failed<T>(message, errors);
            failedResult.Data = data;
            failedResult.Status = ServiceResponseDtoStatus.Error;

            string ebiMessage = message;
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    ebiMessage += "\r\n" + error;
                }
            }
            //logger.Error(ebiMessage, DateTime.Now);
            return failedResult;
        }

        /// <summary>
        /// Error response with the exception, and the function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <param name="pFunction"></param>
        /// <returns></returns>
        public static ServiceResponseDto<T> Failed<T>(Exception e, string pFunction)
        {
            //logger.Error(e);

            return new ServiceResponseDto<T>
            {
                ErrorMessages = ExtractExceptionMessages(e),
                Message = "Exception occured in :" + pFunction,
                Status = ServiceResponseDtoStatus.Error
            };
        }

        private static List<string> ExtractExceptionMessages(Exception e)
        {
            var exceptionMessages = new List<string>();
            while (e?.Message != null)
            {
                exceptionMessages.Add(e.Message);
                e = e.InnerException;
            }
            return exceptionMessages;
        }
    }
}

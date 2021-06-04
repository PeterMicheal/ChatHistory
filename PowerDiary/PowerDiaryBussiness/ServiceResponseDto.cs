using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerDiaryBusiness
{
    public enum ServiceResponseDtoStatus { Success = 1, Error = 0, AccessDenied = 2, NotFound = 3 }
    public class ServiceResponseDto<TInstance>
    {
        public ServiceResponseDtoStatus Status { get; set; }

        public string Message { get; set; }

        public TInstance Data { get; set; }

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

    public static class ServiceResponse
    {
        public static ServiceResponseDto<T> Successful<T>(this T instanse, string message = "Success")
        {
            return new ServiceResponseDto<T>
            {
                Data = instanse,
                Message = message,
                Status = ServiceResponseDtoStatus.Success
            };
        }

        public static ServiceResponseDto<T> Successful<T>(string message)
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

        public static ServiceResponseDto<T> Failed<T>(this T data, string message,
            IList<string> errors = null, string pFunction = "")
        {
            var failedResult = Failed<T>(message, errors);
            failedResult.Data = data;
            failedResult.Status = ServiceResponseDtoStatus.Error;

            string errorMessage = message;
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    errorMessage += "\r\n" + error;
                }
            }
            return failedResult;
        }

        public static ServiceResponseDto<T> Failed<T>(Exception e, string pFunction)
        {
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

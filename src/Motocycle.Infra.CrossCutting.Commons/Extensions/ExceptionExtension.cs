using System;
using System.Collections.Generic;
using System.Text;

namespace Motocycle.Infra.CrossCutting.Commons.Extensions
{
    public static class ExceptionExtension
    {
        public static string GetErrorMsg(this Exception ex)
        {
            StringBuilder sb = new StringBuilder(ex?.Message);
            Exception inner = ex.InnerException;

            while (inner is not null)
            {
                sb.Append($" - {inner.Message}");
                inner = inner.InnerException;
            }
            return sb.ToString();
        }

        public static List<string> GetErrorList(this Exception ex)
        {
            List<string> errorList = new List<string>();

            if (ex is not null)
            {
                errorList.Add(ex.Message);
            }

            Exception inner = ex?.InnerException;
            while (inner is not null)
            {
                errorList.Add(inner.Message);
                inner = inner.InnerException;
            }

            return errorList;
        }
    }
}

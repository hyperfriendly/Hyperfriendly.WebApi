using System;
using System.Collections.Generic;

namespace Hyperfriendly.WebApi.ErrorHandling
{
    public class ExceptionTransformer
    {
        private readonly Dictionary<Type, dynamic> _transformers;

        public ExceptionTransformer()
        {
            _transformers = new Dictionary<Type, dynamic>();
        }

        public Func<Exception, ErrorResponse> DefaultTransformer { get; set; }

        public void AddTransformer<TException>(Func<TException, ErrorResponse> func) where TException : Exception
        {
            _transformers.Add(typeof(TException), func);
        }

        public ErrorResponse Transform(Exception ex)
        {
            var exceptionType = ex.GetType();
            if (!_transformers.ContainsKey(exceptionType))
            {
                if (DefaultTransformer == null)
                {
                    throw new ApplicationException(string.Format("No tranformer is defined for type \"{0}\"", exceptionType.Name));
                }

                return DefaultTransformer(ex);
            }
            var transformer = _transformers[exceptionType];
            return transformer((dynamic)ex);
        }
    }
}
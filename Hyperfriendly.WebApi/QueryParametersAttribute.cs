using System;
using System.Collections.Generic;
using System.Linq;

namespace Hyperfriendly.WebApi
{
    public class QueryParametersAttribute : Attribute
    {
        public List<string> Parameters { get; set; }
        public QueryParametersAttribute(params string[] queryParam)
        {
            Parameters = queryParam.ToList();
        }
    }
}
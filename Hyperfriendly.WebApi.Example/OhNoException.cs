using System;

namespace Hyperfriendly.WebApi.Example
{
    public class OhNoException : Exception
    {
        public OhNoException(string shout)
        {
            Shout = shout;
        }

        public string Shout { get; private set; }
    }
}
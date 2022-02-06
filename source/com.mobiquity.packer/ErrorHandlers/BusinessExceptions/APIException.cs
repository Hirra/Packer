using System;

namespace com.mobiquity.packer
{
    /// <summary>
    /// API Exception
    /// </summary>
    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {
        }

        public APIException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

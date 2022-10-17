using System;

namespace SalesWeb.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message):base(message)
        {

        }
    }
}

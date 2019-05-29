using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Exceptions
{
    public class PortalDomainException : Exception
    {
        public PortalDomainException()
        { }

        public PortalDomainException(string message)
            : base(message)
        { }

        public PortalDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}

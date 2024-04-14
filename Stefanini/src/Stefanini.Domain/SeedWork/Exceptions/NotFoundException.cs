using System.Diagnostics.CodeAnalysis;

namespace Stefanini.Domain.SeedWork.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message)
        {
        }
    }
}

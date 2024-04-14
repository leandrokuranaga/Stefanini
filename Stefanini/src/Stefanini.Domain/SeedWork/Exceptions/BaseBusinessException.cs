using System.Diagnostics.CodeAnalysis;

namespace Stefanini.Domain.SeedWork.Exceptions
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseBusinessException : Exception
    {
        public string Page { get; protected set; }
        public string Key { get; protected set; }

        protected BaseBusinessException(string page, string key)
            : base()
        {

            Page = page;
            Key = key;

        }
    }
}

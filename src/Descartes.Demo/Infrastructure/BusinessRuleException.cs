using System;
using System.Runtime.Serialization;

namespace Descartes.Demo.Infrastructure
{
    /// <summary>
    /// Represents errors that happened when trying to validate business rules
    /// </summary>
    [Serializable]
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException() { }
        public BusinessRuleException(string message) : base(message) { }
        public BusinessRuleException(string message, Exception inner) : base(message, inner) { }
        protected BusinessRuleException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
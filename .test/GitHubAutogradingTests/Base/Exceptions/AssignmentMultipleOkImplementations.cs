using System;
using System.Runtime.Serialization;

namespace GitHubAutogradingTests
{
    [Serializable]
    internal class AssignmentMultipleOkImplementations : Exception
    {
        public AssignmentMultipleOkImplementations()
        {
        }

        public AssignmentMultipleOkImplementations(string? message) : base(message)
        {
        }

        public AssignmentMultipleOkImplementations(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AssignmentMultipleOkImplementations(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace GitHubAutogradingTests
{
    [Serializable]
    internal class AssignmentNothingChangedException : Exception
    {
        public AssignmentNothingChangedException()
        {
        }

        public AssignmentNothingChangedException(string? message) : base(message)
        {
        }

        public AssignmentNothingChangedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AssignmentNothingChangedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
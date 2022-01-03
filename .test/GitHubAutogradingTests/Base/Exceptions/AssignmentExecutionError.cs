using System;
using System.Runtime.Serialization;

namespace GitHubAutogradingTests
{
    [Serializable]
    internal class AssignmentExecutionError : Exception
    {
        public AssignmentExecutionError()
        {
        }

        public AssignmentExecutionError(string? message) : base(message)
        {
        }

        public AssignmentExecutionError(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AssignmentExecutionError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace GitHubAutogradingTests
{
    [Serializable]
    internal class AssignmenthasNoExecutables : Exception
    {
        public AssignmenthasNoExecutables()
        {
        }

        public AssignmenthasNoExecutables(string? message) : base(message)
        {
        }

        public AssignmenthasNoExecutables(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AssignmenthasNoExecutables(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
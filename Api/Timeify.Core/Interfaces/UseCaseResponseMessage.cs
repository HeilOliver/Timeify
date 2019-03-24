namespace Timeify.Core.Interfaces
{
    public abstract class UseCaseResponseMessage
    {
        protected UseCaseResponseMessage(bool success = false, string message = null)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
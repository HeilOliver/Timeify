namespace Timeify.Core.Dto
{
    public sealed class ApplicationError
    {
        public ApplicationError(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; }
        public string Description { get; }
    }
}
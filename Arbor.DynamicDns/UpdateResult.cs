namespace Arbor.DynamicDns
{
    public class UpdateResult
    {
        public string Message { get; }

        public UpdateResult(string message)
        {
            Message = message;
        }
    }
}
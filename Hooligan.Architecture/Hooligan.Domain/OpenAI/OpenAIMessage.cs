namespace Hooligan.Domain.OpenAI
{
    public record OpenAIMessage
    {
        public string role { get; set; }
        public string message { get; set; }
    }
}

namespace Motocycle.Domain.Core.Messages
{
    public abstract class BaseReportResponse : ResponseBase
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
namespace MarkupProcessor.Application.Dto
{
    public class MDContentsDto
    {
        public MDContentsDto(
            string payload,
            string eventName)
        {
            Payload = payload;
            EventName = eventName;
        }

        public string Payload { get; set; }
        public string EventName { get; set; }
    }
}

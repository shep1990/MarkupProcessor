namespace MarkupProcessor.Handlers
{
    public abstract class HandlerBase
    {
        public ILogger Logger { get; }

        public HandlerBase(ILogger logger)
        {
            Logger = logger;
        }
    }
}

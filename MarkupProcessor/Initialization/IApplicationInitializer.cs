namespace MarkupProcessor.Initialization
{
    public interface IApplicationInitializer
    {
        Task Initialize(IServiceProvider provider, IWebHostEnvironment environment);
    }
}

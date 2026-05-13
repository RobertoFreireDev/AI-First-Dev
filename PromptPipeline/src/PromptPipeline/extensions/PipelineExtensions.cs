public static class PipelineExtensions
{
    public static PromptPipeline UseDefault(this PromptPipeline pipeline)
    {
        return pipeline
            .Use(new LoggingMiddleware())
            .Use(new TimingMiddleware());
    }
}
using HotChocolate.Execution.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class CustomRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddGraphQLConventions(
        this IRequestExecutorBuilder builder)
    {
        builder.AddPagingArguments();
        builder.AddSorting();
        builder.AddFiltering();
        builder.AddProjections();
        builder.AddGlobalObjectIdentification();
        builder.AddQueryConventions();
        builder.AddQueryContext();
        builder.ModifyPagingOptions(o => o.RequirePagingBoundaries = false);
        return builder;
    } 
}

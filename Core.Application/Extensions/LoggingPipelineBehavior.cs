using MediatR;
using Serilog;
using System.Text.Json;

namespace Application.Behaviors;


public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingPipelineBehavior(ILogger logger)
    {
        _logger = logger.ForContext<LoggingPipelineBehavior<TRequest, TResponse>>();
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.Information(
            "Starting request {RequestName} at {DateTimeUtc}. Request details: {RequestData}",
            typeof(TRequest).Name,
            DateTime.UtcNow,
            JsonSerializer.Serialize(request));

        TResponse response;
        try
        {
            response = await next();

            _logger.Information(
                "Completed request {RequestName} at {DateTimeUtc}. Response: {ResponseData}",
                typeof(TRequest).Name,
                DateTime.UtcNow,
                JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            _logger.Error(
                "Request failed {RequestName} at {DateTimeUtc}. Exception: {ExceptionMessage}, StackTrace: {StackTrace}",
                typeof(TRequest).Name,
                DateTime.UtcNow,
                ex.Message,
                ex.StackTrace);

            throw;
        }

        return response;
    }
}

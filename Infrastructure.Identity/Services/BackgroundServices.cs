using Core.Application.Interfaces;
using Hangfire;
using System.Linq.Expressions;

namespace Infrastructure.Identity.Services
{
    public class BackgroundServices : IBackgroundJobServices
    {
        private readonly IBackgroundJobClient _backgroundClient;

        public BackgroundServices(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundClient = backgroundJobClient;
        }

        public string AddEnqueue(Expression<Action> methodCall)
        {
            return _backgroundClient.Enqueue(methodCall);
        }

        public string AddEnqueue<T>(Expression<Action<T>> methodCall)
        {
            return _backgroundClient.Enqueue<T>(methodCall);
        }

        public string AddContinuations(Expression<Action> methodCall, string jobid)
        {
            return _backgroundClient.ContinueJobWith(jobid, methodCall);
        }

        public string AddContinuations<T>(Expression<Action<T>> methodCall, string jobid)
        {
            return _backgroundClient.ContinueJobWith<T>(jobid, methodCall);
        }

        public string AddSchedule(Expression<Action> methodCall, TimeSpan time)
        {
            var result = _backgroundClient.Schedule(methodCall, time);
            return result;

        }

        public string AddSchedule<T>(Expression<Action<T>> methodCall , TimeSpan time)
        {
            var result = _backgroundClient.Schedule<T>(methodCall, time);
            return result;
        }
    }
}

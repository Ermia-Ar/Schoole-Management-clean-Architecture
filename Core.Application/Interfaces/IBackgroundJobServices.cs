using System.Linq.Expressions;

namespace Core.Application.Interfaces
{
    public interface IBackgroundJobServices
    {
        public string AddEnqueue(Expression<Action> methodCall);

        public string AddEnqueue<T>(Expression<Action<T>> methodCall);

        public string AddContinuations(Expression<Action> methodCall, string jobid);

        public string AddContinuations<T>(Expression<Action<T>> methodCall, string jobid);

        public string AddSchedule(Expression<Action> methodCall, TimeSpan recuringTime);

        public string AddSchedule<T>(Expression<Action<T>> methodCall, TimeSpan recuringTime);
    }
}


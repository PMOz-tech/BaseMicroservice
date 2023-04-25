using Hangfire.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace BaseMicroservice.Services.Interfaces
{
    public interface IBaseMicroServices
    {
        void Start(HostControl hc);
        void Stop();
        void Pause();
        void Continue();
        void Shutdown();
        void Resume();
        void TryRequest(Action action, int maxFailures, int startTimeOutsMs = 100, int resetTimeout = 10000, Action<Exception> onError = null);
        Task TryRequestAsync([NotNull] Func<Task> action, int maxFailures, int startTimeOutMS = 100, int resetTimeout = 10000, [CanBeNull] Action<Exception> onError = null);
        void PublishMessage(object message, string connectStr = "host=localhost", string topic = "");
        Task PublishMessageAsync(Object message, string connectStr = "host=localhost", string topic = "");



    }
}

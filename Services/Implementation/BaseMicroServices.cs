using Autofac;
using BaseMicroservice.Models;
using BaseMicroservice.Services.Interfaces;
using CacheManager.Core;
using EasyNetQ;
using Hangfire.Annotations;
//using Hangfire.Logging;
using log4net;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;
using Validation;

namespace BaseMicroservice.Services.Implementation
{
    public class BaseMicroServices<T> : IBaseMicroServices where T : class, new()
    {
        private static Timer _timer = null;
        readonly static log4net.ILog _log = LogManager.GetLogger(typeof(BaseMicroServices<T>));
        private string _workerId;
        readonly ILifetimeScope _lifeTimeScope;
        private static string _name;
        private static HostControl _host;
        private static T _type;
        private IBus _bus;
        private ICacheManager<object> _cache = null;

        public BaseMicroServices()
        {
            double interval = 60000;
            _timer = new Timer(interval);
            Assumes.True(_timer != null, "_timer is null");
            _timer.Elapsed += OnTick;
            _timer.AutoReset = true;
            _workerId = Guid.NewGuid().ToString();
            _name = nameof(T);
        }

        protected virtual void OnTick([NotNull] object sender, [NotNull] ElapsedEventArgs e)
        {
            Console.WriteLine(string.Intern("Heartbeat"));
            Requires.NotNull<ILog>(_log, string.Intern("log is null"));
            _log?.Debug(_name + " (" + _workerId.ToString() + string.Intern("): ") + SystemClock.Instance.GetCurrentInstant().ToDateTimeUtc().ToLocalTime().ToLongTimeString() + string.Intern(": Heartbeat"));
            HealthStatusMessage h = new HealthStatusMessage
            {
                ID = _workerId,
                MemoryUsed = Environment.WorkingSet,
                CPU = Convert.ToDouble(GetCpuUsage()),
                ServiceName = _name,
                Message = "OK",
                Status = (int)MSSTATUS.healthy,

            };
            _bus.PubSub.Publish(h);
        }



        public int GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", Environment.MachineName);
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000); //This avoid that answer always 0
            return (int)cpuCounter.NextValue();
        }

        public void Start(HostControl hc)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Continue()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void TryRequest(Action action, int maxFailures, int startTimeOutsMs = 100, int resetTimeout = 10000, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }

        public Task TryRequestAsync([NotNull] Func<Task> action, int maxFailures, int startTimeOutMS = 100, int resetTimeout = 10000, [CanBeNull] Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }

        public void PublishMessage(object message, string connectStr = "host=localhost", string topic = "")
        {
            throw new NotImplementedException();
        }

        public Task PublishMessageAsync(object message, string connectStr = "host=localhost", string topic = "")
        {
            throw new NotImplementedException();
        }
    }
}

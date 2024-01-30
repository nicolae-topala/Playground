using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Quartz;
using TaskScheduling.Quartz.Jobs;

namespace TaskScheduling.Quartz.Setups
{
    public class LoggingBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        private readonly JobKey _jobKey;
        private readonly IConfiguration _configuration;
        private int _intervalInSeconds;

        public LoggingBackgroundJobSetup(IConfiguration configuration)
        {
            _jobKey = JobKey.Create(nameof(LoggingBackgroundJob));
            _configuration = configuration;
        }
        public void Configure(QuartzOptions options)
        {
            int.TryParse(_configuration["Quartz:IntervalInSeconds"], out _intervalInSeconds);

            options
                .AddJob<LoggingBackgroundJob>(jobBuilder => jobBuilder
                    .WithIdentity(_jobKey))
                .AddTrigger(trigger =>  trigger
                        .ForJob(_jobKey)
                        .WithSimpleSchedule(schedule => schedule
                            .WithIntervalInSeconds(_intervalInSeconds)
                            .RepeatForever()
                        )
                );
        }
    }
}

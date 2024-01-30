using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TaskScheduling.Quartz.Jobs;
using TaskScheduling.Quartz.Setups;

namespace TaskScheduling.Quartz
{
    public static class QuartzDI
    {
        public static void AddQuartzServices(this IServiceCollection services)
        {
            services.AddQuartz();

            // Creating a new instance of a job when a trigger fires and executing it
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<LoggingBackgroundJobSetup>();
        }
    }
}
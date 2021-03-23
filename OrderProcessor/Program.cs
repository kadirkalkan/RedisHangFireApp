using Hangfire;
using Hangfire.SqlServer;
using OrderProcessor.BackgroundJobs;
using OrderProcessor.Managers;
using OrderProcessor.Redis;
using OrderProcessor.Redis.Helpers;
using System;

namespace OrderProcessor
{
    class Program
    {

        private static RedisContext _redisContext;
        private static ConsoleManager _consoleManager;
        static void Main(string[] args)
        {
            Initialize();


            // ****** HangFire Recurring Jobs Trigger *************

            RecurringJobs.GetNewOrders();
            
            // --------- Console holder -------------

            using (var server = new BackgroundJobServer())
            {
                _consoleManager.StopClosing();
            }

            // ****** HangFire Recurring Jobs Trigger *************

        }

        private static void Initialize()
        {
            _redisContext = RedisContext.GetInstance();
            _consoleManager = ConsoleManager.GetInstance();

            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(String.Format("Server={0}; Database=Hangfire.Sample; User Id=sa;Password=Kadir123*!;", Utils.SQL_URL), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                });

        }

    }
}

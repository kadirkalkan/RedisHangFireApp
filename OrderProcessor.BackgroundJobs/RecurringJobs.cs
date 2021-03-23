using Hangfire;
using System;
using System.Threading;

namespace OrderProcessor.BackgroundJobs
{
    public class RecurringJobs
    {
        public static void GetNewOrders()
        {
            Console.WriteLine("GetNewOrders Job Triggered. Triggered Time : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            RecurringJob.AddOrUpdate(() => FetchOrders(), Cron.Hourly);
        }
        
        public static void FetchOrders()
        {
            Console.WriteLine("Fetching Users Please Wait. Triggered Time : " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            Thread.Sleep(3000);
        }
    }
}

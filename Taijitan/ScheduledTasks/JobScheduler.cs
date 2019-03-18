using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.ScheduledTasks
{
    public static class JobScheduler
    {
        public static async Task StartAsync()
        {
            // construct a scheduler factory
            NameValueCollection props = new NameValueCollection
    {
        { "quartz.serializer.type", "binary" }
    };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            // get a scheduler
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            // define the job and tie it to our LesmomentJob class
            IJobDetail job = JobBuilder.Create<LesmomentJob>()
                .WithIdentity("lesmomentjob", "group1")
                .Build();

            // Trigger the job to run now, and then every day
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("lesmomentjobTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever())
            .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}

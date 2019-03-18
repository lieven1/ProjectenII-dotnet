using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan.Data.Repositories;
using Taijitan.Models.Domain;

namespace Taijitan.ScheduledTasks
{
    public class LesmomentJob : IJob
    {
        ILesmomentRepository _lesmomentRepository;

        public LesmomentJob(ILesmomentRepository lesmomentRepository)
        {
            _lesmomentRepository = lesmomentRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _lesmomentRepository.GenereerLesmomentDag();
            await Console.Out.WriteLineAsync("Lesmomenten worden gegenereerd.");
        }
    }
}

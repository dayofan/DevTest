using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomerService customerService;

        public JobService(ApplicationDbContext context, ICustomerService customerService)
        {
            this.context = context;
            this.customerService = customerService;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs.Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                Customer = customerService.GetCustomer(x.CustomerId),
                When = x.When
            }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            var job = context.Jobs.Where(x => x.JobId == jobId).SingleOrDefault();
            var customer = customerService.GetCustomer(job.CustomerId);

            return new JobModel
            {
                JobId = job.JobId,
                Engineer = job.Engineer,
                Customer = customer,
                When = job.When
            };
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                CustomerId = model.Customer.Id,
                When = model.When
            });
            var customer = customerService.GetCustomer(model.Customer.Id);

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Customer = customer,
                Engineer = addedJob.Entity.Engineer,
                When = addedJob.Entity.When
            };
        }
    }
}

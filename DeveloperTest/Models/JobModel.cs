using System;

namespace DeveloperTest.Models
{
    public class JobModel
    {
        public int JobId { get; set; }

        public string Engineer { get; set; }

        public BaseCustomerModel Customer { get; set; }

        public DateTime When { get; set; }
    }
}

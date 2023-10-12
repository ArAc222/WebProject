using System;
using System.Collections.Generic;
using Example.Model.Common;

namespace Example.Model
{
    public class JobModel: IJobModel
    {
        public Guid Id { get; set; }
        public int Salary { get; set; }
        public string Type { get; set; }
        public List<WorkerModel> Workers { get; set; }
    }
}

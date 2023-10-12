using Example.Model.Common;
using System;

namespace Example.Model
{
    public class WorkerModel: IWorkerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }

        public Guid JobId { get; set; }
        public IJobModel Job { get; set; }
    }
}

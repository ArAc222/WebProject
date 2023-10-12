using System;

namespace Example.Model.Common
{
    public interface IWorkerModel
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        char Gender { get; set; }
        Guid JobId { get; set; }

        IJobModel Job { get; set; }
    }
}


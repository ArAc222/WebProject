using System;

namespace Example.Model.Common
{
    public interface IJobModel
    {
        Guid Id { get; set; }
        int Salary { get; set; }
        string Type { get; set; }
    }
}


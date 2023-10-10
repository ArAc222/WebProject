using System;

namespace Example.Model
{
    public class WorkerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public Guid JobId { get; set; }
    }
}

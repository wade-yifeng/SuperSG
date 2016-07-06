namespace Sleemon.Data
{
    using System;

    public class UserTaskModel
    {
        public int UserTaskId { get; set; }

        public string Title { get; set; }

        public DateTime StartFrom { get; set; }

        public int Point { get; set; }

        public int No { get; set; }
        
        public byte Status { get; set; }

        public byte TaskCategory { get; set; }
    }
}
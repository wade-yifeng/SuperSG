namespace Sleemon.Data
{
    using System;

    public class TrainingBaseModel
    {
        public int TrainingId { get; set; }

        public string Subject { get; set; }

        public DateTime StartFrom { get; set; }

        public string Avatar { get; set; }

        public int Status { get; set; }
    }
}

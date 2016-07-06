namespace Sleemon.Data
{
    using System;
    using System.Collections.Generic;

    public class TrainingDetailModel : TrainingBaseModel
    {
        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime? EndTo { get; set; }

        public int MaxParticipant { get; set; }

        public DateTime JoinDeadline { get; set; }

        public bool IsPublic { get; set; }

        public bool IsCheckInNeeded { get; set; }

        public string CheckInQRCode { get; set; }

        public byte State { get; set; }

        public int StatusId { get; set; }

        public string LastUpdateUser { get; set; }

        public string LastUpdateUserName { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public List<TaskDetailsModel> Tasks { get; set; }
    }
}
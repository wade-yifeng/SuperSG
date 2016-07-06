namespace Sleemon.Data
{
    using System;

    public class LearningFileListModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string FilePath { get; set; }

        public string LastUpdateUserName { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public byte Status { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}

﻿namespace Sleemon.Data
{
    using System;

    public class LearningFileListModel : LearningFileDetailModel
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}

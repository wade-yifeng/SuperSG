﻿namespace Sleemon.Data
{
    using System;

    public class QuestionnaireDetailModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string LastUpdateUserName { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}

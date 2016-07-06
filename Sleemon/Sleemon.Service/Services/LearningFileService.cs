namespace Sleemon.Service
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;
    using Sleemon.Core;
    using Sleemon.Data;

    public class LearningFileService : ILearningFileService
    {
        private readonly ISleemonEntities _invoicingEntities;

        public LearningFileService([Dependency] ISleemonEntities invoicingEntities)
        {
            this._invoicingEntities = invoicingEntities;
        }

        public IList<LearningFileListModel> GetLearningFileList(int pageIndex, int pageSize, string fileTitle)
        {
            throw new NotImplementedException();
        }

        public IList<LearningCourseListModel> GetCourseList(int pageIndex, int pageSize, string courseTitle)
        {
            throw new NotImplementedException();
        }

        public LearningCourseDetailModel GetCourseDetailById(int courseId)
        {
            throw new NotImplementedException();
        }

        public ResultBase SaveCourseDetail(LearningCourseDetailModel course)
        {
            throw new NotImplementedException();
        }

        public ResultBase DeleteCourseById(int courseId)
        {
            throw new NotImplementedException();
        }
    }
}

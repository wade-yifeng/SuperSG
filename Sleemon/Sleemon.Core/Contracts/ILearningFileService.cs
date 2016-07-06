namespace Sleemon.Core
{
    using Sleemon.Data;
    using System.Collections.Generic;

    public interface ILearningFileService
    {
        IList<LearningCourseListModel> GetCourseList(int pageIndex, int pageSize, string courseTitle);

        LearningCourseDetailModel GetCourseDetailById(int courseId);

        ResultBase SaveCourseDetail(LearningCourseDetailModel course);

        ResultBase DeleteCourseById(int courseId);

        IList<LearningFileListModel> GetLearningFileList(int pageIndex, int pageSize, string fileTitle);
    }
}

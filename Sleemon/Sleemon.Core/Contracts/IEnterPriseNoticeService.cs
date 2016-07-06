namespace Sleemon.Core
{
    using System.Collections.Generic;

    using Sleemon.Data;

    public interface IEnterpriseNoticeService
    {
        IList<EnterpriseNoticePreviewModel> GetEnterpriseSummeryNotices(int pageIndex, int pageSize);

        IList<EnterpriseNoticePreviewModel> GetSlideEnterpriseSummeryNotices(int topCount);

        EnterpriseNoticeDetailModel GetEnterpriseNoticeById(int id);

        IList<UserComment> GetTopCommentsByNoticeId(int id, string userId, int topCount);

        IList<UserComment> GetPagedCommentsByNoticeId(int id, string userId, int pageIndex, int pageSize, out int totalCount);

        ProConNoticeResult ProNoticeById(int id, string userId);

        ProConNoticeResult ConNoticeById(int id, string userId);

        ProCommentResult ProCommentById(int id, string userId);

        bool SubmitEnterpriseNotice(EnterpriseNoticeSubmitModel enModel);

        IList<EnterpriseNotice> GetEnterpriseNotices(int pageIndex, int pageSize, string noticeTitle, out int totalCount);

        IList<EnterpriseNoticePreviewModel> GetLatestNotices(int previousLatestNoticeId);

        NewCommentViewModel AddNoticeComment(int noticeId, string userId, string comment, byte category);
    }
}

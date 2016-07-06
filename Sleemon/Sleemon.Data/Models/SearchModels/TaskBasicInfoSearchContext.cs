namespace Sleemon.Data
{
    using System;
    using System.Linq.Expressions;

    using Sleemon.Common;

    public class TaskBasicInfoSearchContext
    {
        public string TaskName { get; set; }

        public byte TaskCategory { get; set; }

        public int BelongTo { get; set; }

        public byte? Status { get; set; }

        public DateTime? StartFrom { get; set; }

        public DateTime? EndTo { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public Expression<Func<Task, bool>> GenerateSearchConditions()
        {
            Expression<Func<Task, bool>> searchConditions = p => p.IsActive;

            if (!string.IsNullOrEmpty(this.TaskName))
            {
                searchConditions = searchConditions.And(p => p.Title.Contains(this.TaskName));
            }

            searchConditions = searchConditions.And(p => p.TaskCategory == this.TaskCategory);
            searchConditions = searchConditions.And(p => p.BelongTo == this.BelongTo);

            if (this.Status.HasValue)
            {
                searchConditions = searchConditions.And(p => p.Status == this.Status.Value);
            }

            if (this.StartFrom.HasValue)
            {
                searchConditions = searchConditions.And(p => p.StartFrom >= this.StartFrom.Value);
            }

            if (this.EndTo.HasValue)
            {
                searchConditions = searchConditions.And(p => p.EndTo <= this.EndTo.Value);
            }

            return searchConditions;
        }
    }
}

namespace Sleemon.Service
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Unity;
    using Sleemon.Core;
    using Sleemon.Data;

    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly ISleemonEntities _invoicingEntities;

        public QuestionnaireService([Dependency] ISleemonEntities invoicingEntities)
        {
            this._invoicingEntities = invoicingEntities;
        }

        public IList<QuestionnaireListModel> GetQuestionnaireList(int pageIndex, int pageSize, string questionnaireTitle)
        {
            throw new NotImplementedException();
        }
    }
}

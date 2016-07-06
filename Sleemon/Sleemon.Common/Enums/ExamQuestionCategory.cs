namespace Sleemon.Common
{
    using System.ComponentModel;

    public enum ExamQuestionCategory
    {
        [Description("单选")]
        SingleOption = 0,

        [Description("多选")]
        MultiOptions = 1,

        [Description("主观题")]
        Subjective = 2,
        
        [Description("打分题")]
        Grade = 3
    }
}

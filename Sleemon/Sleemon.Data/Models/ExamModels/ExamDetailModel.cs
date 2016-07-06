namespace Sleemon.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ExamDetailModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入试卷名")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "请输入总分")]
        [Range(0, double.MaxValue, ErrorMessage = "总分必须为数字")]
        public double TotalScore { get; set; }

        [Required(ErrorMessage = "请输入及格分")]
        [Range(0, double.MaxValue, ErrorMessage = "及格分必须为数字")]
        public double PassingScore { get; set; }

        public string LastUpdateUser { get; set; }

        public string LastUpdateUserName { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public byte State { get; set; }

        public List<ExamQuestionModel> Questions { get; set; }
    }

    public class ExamQuestionModel
    {
        public int ExamQuestionId { get; set; }

        public short No { get; set; }

        [Required(ErrorMessage = "请输入题目")]
        public string Question { get; set; }

        public string Image { get; set; }

        public byte Category { get; set; }

        public string CorrectAnswer { get; set; }

        [Required(ErrorMessage = "请输入单题得分")]
        [Range(0, double.MaxValue, ErrorMessage = "单题得分分必须为数字")]
        public double Score { get; set; }

        public List<ExamChoiceModel> Choices { get; set; }
    }

    public class ExamChoiceModel
    {
        [Required(ErrorMessage = "请选择选项")]
        public byte Choice { get; set; }

        [Required(ErrorMessage = "请输入选项描述")]
        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsAnswer { get; set; }
    }
}

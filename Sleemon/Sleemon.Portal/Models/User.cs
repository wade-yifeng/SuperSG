using System.ComponentModel.DataAnnotations;

namespace Sleemon.Portal.Models
{
    public class User
    {
        [Display(Name = "员工号", Description = "请输入员工号")]
        [Required(ErrorMessage = "员工号不为空")]
        public string UserUniqueId { get; set; }

        /// <summary>
        /// 存放在ClaimsIdentity
        /// 显示用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 存放在ClaimsIdentity
        /// 显示用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 登陆跳转地址
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 是否自动登陆
        /// </summary>
        public bool IsAutoLogin { get; set; }

        [Display(Name = "密码", Description = "请输入密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

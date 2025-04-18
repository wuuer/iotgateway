using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalkingTec.Mvvm.Core
{
    [Table("FrameworkGroups")]
    [SoftKey(nameof(FrameworkGroup.GroupCode))]
    public class FrameworkGroup : TreePoco<FrameworkGroup>, ITenant
    {
        [Display(Name = "_Admin.GroupCode")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Validate.{0}number")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [CanNotEdit]
        public string GroupCode { get; set; }

        [Display(Name = "_Admin.GroupName")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string GroupName { get; set; }

        [Display(Name = "_Admin.Remark")]
        public string GroupRemark { get; set; }

        [Display(Name = "_Admin.GroupManager")]
        public string Manager { get; set; }

        [NotMapped]
        [Display(Name = "_Admin.UsersCount")]
        public int UsersCount { get; set; }

        [Display(Name = "_Admin.Tenant")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string TenantCode { get; set; }
    }
}
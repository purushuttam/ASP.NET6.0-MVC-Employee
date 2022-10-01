using Demo.Models;
using System.ComponentModel.DataAnnotations;

namespace Demo.ViewModels
{
    public class DepartmentEditViewModel : Department
    {/*
        [Key]
        public int DeptId { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Maximum length of Name is 50 charactor.")]
        public string DeptName { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Maximum length of Name is 50 charactor.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Alphabet only.")]
        public string MgrName { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Maximum length of Location is 50 charactor")]
        public string Location { get; set; }*/
    }
}

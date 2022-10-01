using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Demo.ViewModels
{
    public class EmpDept
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Maximum length of Name is 50 charactor.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Alphabet only.")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Alphabet only.")]
        public string LastName { get; set; }
        [Required]
        public string? Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime JD { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Please Enter Valid Email ID.")]
        [Display(Name = "Office Email id")]
        public string Email { get; set; }
        public bool Java { get; set; }
        public bool Cpp { get; set; }
        public bool CSharp { get; set; }

        [Required]
        public int DeptId { get; set; }
        public string? PhotoPath { get; internal set; }
        /*public int? DeptId { get; set; }*/
        [Required, MaxLength(50, ErrorMessage = "Maximum length of Name is 50 charactor.")]
        public string DepartmentName { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Maximum length of Name is 50 charactor.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Alphabet only.")]
        public string ManagerName { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Maximum length of Location is 50 charactor")]
        public string Location { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Models
{
    public class DepartmentModel
    {
        public int DeptId { get; set; }
        public List<SelectListItem>?  DepartmentList { get; set; }
    }
}

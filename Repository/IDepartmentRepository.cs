using Demo.Models;

namespace Demo.Repository
{
    public interface IDepartmentRepository
    {
        Department GetDepartment(int DeptId);
        List<Department> GetAllDepartment();
        Department Add(Department department);
        Department Update(Department department);
        Department Delete(int DeptId);
    }
}

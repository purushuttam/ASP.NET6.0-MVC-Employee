using Demo.Models;
using Demo.ViewModels;

namespace Demo.Repository
{
    public interface IEmployeeRepository
    {
        // store procedure by mam
        Credential GetCredential();
        Credential ChangeCredential(Credential Changecredential);
        Employee GetDetailsById(int Id);
        List<Employee> GetEmployees();
        List<EmpDept> GetEmpDeptList();
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee); 
        Employee DeleteEmployee(int Id);
        EmpDept GetEmpDeptList(int v);
        
    }
}

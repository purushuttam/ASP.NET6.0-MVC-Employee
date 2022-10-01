using System.Linq;
using Demo.Data;
using Demo.Models;
using Demo.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private string _connectionString;

        public SQLEmployeeRepository(AppDbContext context , IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EmployeeDbConnnection");
        }
        public List<EmpDept> GetEmpDeptList()
        {
            List<EmpDept> EmpVMObjList = new List<EmpDept>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spEmpDept", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var cnt = reader.VisibleFieldCount;
                        while (reader.Read())
                        {
                            EmpVMObjList.Add(MapToValueListM(reader));
                        }
                    }
                }
            }
            return EmpVMObjList;
        }

        private EmpDept MapToValueListM(SqlDataReader reader)
        {
            return new EmpDept()
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"].ToString(),
                Gender = reader["Gender"].ToString(),
                LastName = reader["LastName"].ToString(),
                DOB = Convert.ToDateTime(reader["DOB"]),
                JD = Convert.ToDateTime(reader["JD"]),
                Email = reader["Email"].ToString(),
                Java = Convert.ToBoolean(reader["Java"]),
                Cpp = Convert.ToBoolean(reader["Cpp"]),
                CSharp = Convert.ToBoolean(reader["CSharp"]),
                DeptId = (int)reader["DeptId"],
                PhotoPath = reader["PhotoPath"].ToString(),
                /*DeptId = (int)reader["DeptId"],*/
                DepartmentName = reader["DepartmentName"].ToString(),
                ManagerName = reader["ManagerName"].ToString(),
                Location = reader["Location"].ToString()
            };
        }
        private EmpDept MapToValueM(SqlDataReader reader)
        {
            return new EmpDept()
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"].ToString(),
                Gender = reader["Gender"].ToString(),
                LastName = reader["LastName"].ToString(),
                DOB = Convert.ToDateTime(reader["DOB"]),
                JD = Convert.ToDateTime(reader["JD"]),
                Email = reader["Email"].ToString(),
                Java = Convert.ToBoolean(reader["Java"]),
                Cpp = Convert.ToBoolean(reader["Cpp"]),
                CSharp = Convert.ToBoolean(reader["CSharp"]),
                DeptId = (int)reader["DeptId"],
                PhotoPath = reader["PhotoPath"].ToString(),
                /*DeptId = (int)reader["DeptId"],*/
                DepartmentName = reader["DepartmentName"].ToString(),
                ManagerName = reader["ManagerName"].ToString(),
                Location = reader["Location"].ToString()
            };
        }

        // -------------store procedure----------

        public Credential GetCredential()
        {
            Credential CD = new Credential();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spCredential", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (var readerCD = cmd.ExecuteReader())
                    {
                        while (readerCD.Read())
                        {
                            CD = CDMapToValue(readerCD);
                        }
                    }
                }
            }
            return CD;
        }
        public Credential ChangeCredential(Credential Changecredential)
        {
            Credential cd = new Credential();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spCredential", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", 1));
                    cmd.Parameters.Add(new SqlParameter("@UserName", Changecredential.UserName));
                    cmd.Parameters.Add(new SqlParameter("@PassWord", Changecredential.PassWord));
                    cmd.Parameters.Add(new SqlParameter("@action", "update"));
                    con.Open();
                    using (SqlDataReader readerCD = cmd.ExecuteReader())
                    {
                        while (readerCD.Read())
                        {
                            cd = CDMapToValue(readerCD);
                        }
                    }
                }
            }
            return cd;
        }

        private Credential CDMapToValue(SqlDataReader readerCD)
        {
            return new Credential()
            {
                Id = (int)readerCD["Id"],
                UserName = readerCD["UserName"].ToString(),
                PassWord = readerCD["PassWord"].ToString()
            };
        }
        private Employee MapToValue(SqlDataReader reader)
        {
            return new Employee()
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"].ToString(),
                Gender = reader["Gender"].ToString(),
                LastName = reader["LastName"].ToString(),
                DOB = Convert.ToDateTime(reader["DOB"]),
                JD = Convert.ToDateTime(reader["JD"]),
                Email = reader["Email"].ToString(),
                Java = Convert.ToBoolean(reader["Java"]),
                Cpp = Convert.ToBoolean(reader["Cpp"]),
                CSharp = Convert.ToBoolean(reader["CSharp"]),
                DeptId = (int)reader["DeptId"],
                PhotoPath = reader["PhotoPath"].ToString()
            };
        }


        private Employee MapToValueList(SqlDataReader reader)
        {
            return new Employee()
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"].ToString(),
                Gender = reader["Gender"].ToString(),
                LastName = reader["LastName"].ToString(),
                DOB = Convert.ToDateTime(reader["DOB"]),
                JD = Convert.ToDateTime(reader["JD"]),
                Email = reader["Email"].ToString(),
                Java = Convert.ToBoolean(reader["Java"]),
                Cpp = Convert.ToBoolean(reader["Cpp"]),
                CSharp = Convert.ToBoolean(reader["CSharp"]),
                DeptId = (int)reader["DeptId"],
                PhotoPath = reader["PhotoPath"].ToString()
            };
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> EmpVMObjList = new List<Employee>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spCRUD", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var cnt = reader.VisibleFieldCount;
                        while (reader.Read())
                        {
                            EmpVMObjList.Add(MapToValueList(reader));
                        }
                    }
                }
            }
            return EmpVMObjList;
        }

        public Employee GetDetailsById(int Id)
        {
            Employee EmpVMObj = new Employee();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spEmpDept", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", Id));
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var cnt = reader.VisibleFieldCount;
                        while (reader.Read())
                        {
                            EmpVMObj = MapToValue(reader);
                        }
                    }
                }
            }
            return EmpVMObj;
        }

        public string GetLastEmployeeCode()
        {
            throw new NotImplementedException();
        }
        
        public Employee AddEmployee(Employee employee)
        {
            Employee Ec = new Employee();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spCRUD", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FirstName",employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                    cmd.Parameters.Add(new SqlParameter("@DOB", employee.DOB));
                    cmd.Parameters.Add(new SqlParameter("@JD", employee.JD));
                    cmd.Parameters.Add(new SqlParameter("@Email", employee.Email));
                    cmd.Parameters.Add(new SqlParameter("@Java", employee.Java));
                    cmd.Parameters.Add(new SqlParameter("@Cpp", employee.Cpp));
                    cmd.Parameters.Add(new SqlParameter("@CSharp", employee.CSharp));
                    cmd.Parameters.Add(new SqlParameter("@DeptId", employee.DeptId));
                    cmd.Parameters.Add(new SqlParameter("@PhotoPath", employee.PhotoPath));
                    cmd.Parameters.Add(new SqlParameter("@action", "insert"));
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var cnt = reader.VisibleFieldCount;
                        while (reader.Read())
                        {
                            Ec = MapToValue(reader);
                        }
                    }
                }
            }
            return Ec;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            Employee Ec = new Employee();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spCRUD", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", employee.Id));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                    cmd.Parameters.Add(new SqlParameter("@DOB", employee.DOB));
                    cmd.Parameters.Add(new SqlParameter("@JD", employee.JD));
                    cmd.Parameters.Add(new SqlParameter("@Email", employee.Email));
                    cmd.Parameters.Add(new SqlParameter("@Java", employee.Java));
                    cmd.Parameters.Add(new SqlParameter("@Cpp", employee.Cpp));
                    cmd.Parameters.Add(new SqlParameter("@CSharp", employee.CSharp));
                    cmd.Parameters.Add(new SqlParameter("@DeptId", employee.DeptId));
                    cmd.Parameters.Add(new SqlParameter("@PhotoPath", employee.PhotoPath));
                    cmd.Parameters.Add(new SqlParameter("@action", "update"));
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var cnt = reader.VisibleFieldCount;
                        while (reader.Read())
                        {
                            Ec = MapToValue(reader);
                        }
                    }
                }
            }
            return Ec;
        }
       
        public Employee DeleteEmployee(int Id)
        {
            Employee EmpVMObj = new Employee();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spCRUD", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", Id));
                    cmd.Parameters.Add(new SqlParameter("@action","delete"));
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var cnt = reader.VisibleFieldCount;
                        while (reader.Read())
                        {
                            EmpVMObj = MapToValue(reader);
                        }
                    }
                }
            }
            return EmpVMObj;
        }

        public EmpDept GetEmpDeptList(int id)
        {
            EmpDept EmpVMObjList = new EmpDept();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spEmpDept", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var cnt = reader.VisibleFieldCount;
                        while (reader.Read())
                        {
                            EmpVMObjList = MapToValueListM(reader);
                        }
                    }
                }
            }
            return EmpVMObjList;
        }

       
    }
}

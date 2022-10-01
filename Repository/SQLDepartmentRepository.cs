using System.Configuration;
using System.Linq;
using Demo.Data;
using Demo.Models;
using Microsoft.Data.SqlClient;

namespace Demo.Repository
{
    public class SQLDepartmentRepository : IDepartmentRepository
    {
        private string _connectionString;

        public SQLDepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EmployeeDbConnnection");
        }
        //------------- store procedure --------------
        private Department MapToValue(SqlDataReader reader)
        {
            return new Department()
            {
                DeptId = (int)reader["DeptId"],
                DepartmentName = reader["DepartmentName"].ToString(),
                ManagerName = reader["ManagerName"].ToString(),
                Location = reader["Location"].ToString()
            };
        }
        private Department MapToValueList(SqlDataReader reader)
        {
            return new Department()
            {
                DeptId = (int)reader["DeptId"],
                DepartmentName = reader["DepartmentName"].ToString(),
                ManagerName = reader["ManagerName"].ToString(),
                Location = reader["Location"].ToString()
            };
        }
        public List<Department> GetAllDepartment()
        {
            List<Department> EmpVMObjList = new List<Department>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spDepartment", con))
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

        public Department GetDepartment(int DeptId)
        {
            Department EmpVMObj = new Department();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spDepartment" +
                    "" +
                    "", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DeptId", DeptId));
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


        public Department Add(Department department)
        {
            Department Ec = new Department();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spDepartment", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DeptId", department.DeptId));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentName", department.DepartmentName));
                    cmd.Parameters.Add(new SqlParameter("@ManagerName", department.ManagerName));
                    cmd.Parameters.Add(new SqlParameter("@Location", department.Location));
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

        public Department Update(Department department)
        {
            Department Ec = new Department();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spDepartment", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DeptId", department.DeptId));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentName", department.DepartmentName));
                    cmd.Parameters.Add(new SqlParameter("@ManagerName", department.ManagerName));
                    cmd.Parameters.Add(new SqlParameter("@Location", department.Location));
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

        public Department Delete(int DeptId)
        {
            Department EmpVMObj = new Department();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spDepartment", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DeptId", DeptId));
                    cmd.Parameters.Add(new SqlParameter("@action", "delete"));
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

        

        //---------------department dropdown--------------


    }
}

using Demo.Models;
using Demo.Repository;
using System.IO;
using Demo.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using Demo.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Components.Forms;
using NuGet.Versioning;


namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public HomeController(IEmployeeRepository EmployeeRepository,
            IDepartmentRepository DepartmentRepository,
            IHostingEnvironment hostingEnvironment )
        {
            _EmployeeRepository = EmployeeRepository;
            _DepartmentRepository = DepartmentRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        //--------------------Login -----------------------
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login1(string UserName, string PassWord)
        {
            if (ModelState.IsValid)
            {
                var user = _EmployeeRepository.GetCredential();
                if (UserName.Equals(user.UserName) && PassWord.Equals(user.PassWord))
                {
                    return RedirectToAction("Index");
                }
                return View("~/views/home/WrongCredentials.cshtml");
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Admin()
        {
            Credential credential = _EmployeeRepository.GetCredential();
            Credential d = new Credential()
            {
                UserName = credential.UserName,
                PassWord = credential.PassWord
            };
            return View(d);
            
        }
        [HttpPost]
        public IActionResult Admin(Credential model)
        {
            if (ModelState.IsValid)
            {
                Credential newcredential = _EmployeeRepository.GetCredential();
                newcredential.UserName = model.UserName;
                newcredential.PassWord = model.PassWord;
                _EmployeeRepository.ChangeCredential(newcredential);
                return RedirectToAction("Index");
            }
            return View();
        }
        //--------------------Filter-----------------------------
        [HttpGet]
        public ActionResult Filter(string searchBy, string search)        
        {
            if (string.IsNullOrEmpty(search))
            {
                return View("~/views/home/EmployeeNotFound.cshtml");
            }
            else
            {
                if (searchBy == "FirstName")
                {
                    var model = _EmployeeRepository.GetEmployees().Where(m => m.FirstName.StartsWith(search));
                    if (model.Count() == 0)
                        return RedirectToAction("EmployeeNotFound");
                    else
                        return View(model);
                }
                else if (searchBy == "Gender")
                {
                    var model = _EmployeeRepository.GetEmployees().Where(m => m.Gender.StartsWith(search));
                    if (model.Count() == 0)
                        return RedirectToAction("EmployeeNotFound");
                    else
                        return View(model);
                }
                else if(searchBy == "Id")
                {
                    var model = _EmployeeRepository.GetEmployees().Where(m => m.Id.Equals(Convert.ToInt32(search)));
                    if(model.Count() == 0)
                        return RedirectToAction("EmployeeNotFound");
                    else
                        return View(model);
                }
                else
                {
                    var model = _EmployeeRepository.GetEmployees();
                    return View(model);
                }
            }
        }
        public IActionResult Delete(int? id)
        {
            Employee e = _EmployeeRepository.DeleteEmployee(id ?? 1);
            _EmployeeRepository.DeleteEmployee(e.Id);
            return RedirectToAction("Index", new { id = e.Id });
            
        }
        //----------------------------------Photo Upload----------------------------------
        private string ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uiqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsfolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uiqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsfolder, uiqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                } 
            }
            return uiqueFileName;
        }
        public ViewResult Index()
        {
            List<Employee> model = _EmployeeRepository.GetEmployees();
            return View(model);
        }
        public ViewResult Master()
        {
            List<EmpDept> model = _EmployeeRepository.GetEmpDeptList();
            return View(model);
        }
        public ViewResult Details(int? id)
        {
            /*HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _EmployeeRepository.GetDetailsById(id ?? 1),
                PageTitle = "SQL Employee Details",
                *//*Departmentname = _DepartmentRepository.get*//*
            };*/

            EmpDept empDept = _EmployeeRepository.GetEmpDeptList(id ?? 1);

            return View(empDept);
        }

        [HttpGet]
        public IActionResult Create()
        {
            EmployeeCreateViewModel departmentModel = new EmployeeCreateViewModel();
            departmentModel.DepartmentList = new List<SelectListItem>();

            AppDbContext appDbContext = new AppDbContext();
            var data = appDbContext.Departments.ToList();
            departmentModel.DepartmentList.Add(new SelectListItem
            {
                Text = "Select department",
                Value = ""
            });
            foreach (var item in data)
            {
                departmentModel.DepartmentList.Add(new SelectListItem
                {
                    Text = item.DepartmentName,
                    Value = Convert.ToString(item.DeptId)
                });
            }
            return View(departmentModel);
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            departmentModel.DepartmentList = new List<SelectListItem>();

            AppDbContext appDbContext = new AppDbContext();
            var data = appDbContext.Departments.ToList();
            departmentModel.DepartmentList.Add(new SelectListItem
            {
                Text = "Select department",
                Value = ""
            });
            foreach (var item in data)
            {
                departmentModel.DepartmentList.Add(new SelectListItem
                {
                    Text = item.DepartmentName,
                    Value = Convert.ToString(item.DeptId)
                });
            }
            if (ModelState.IsValid)
            {
                string uiqueFileName = ProcessUploadFile(model);

                Employee newEmployee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DOB = model.DOB,
                    JD = model.JD,
                    Email = model.Email,
                    Java = model.Java,
                    Cpp = model.Cpp,
                    CSharp = model.CSharp,
                    DeptId = model.DeptId ,
                    PhotoPath = uiqueFileName,
                };
                _EmployeeRepository.AddEmployee(newEmployee);

                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {            
            Employee employee = _EmployeeRepository.GetDetailsById(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                JD = employee.JD,
                Email = employee.Email,
                Java = employee.Java,
                Cpp = employee.Cpp,
                CSharp = employee.CSharp,
                DeptId = employee.DeptId,
                ExistingPhotoPath = employee.PhotoPath
            };

            employeeEditViewModel.DepartmentList = new List<SelectListItem>();
            AppDbContext appDbContext = new AppDbContext();
            var data = appDbContext.Departments.ToList();
            employeeEditViewModel.DepartmentList.Add(new SelectListItem
            {
                Text = "Select department",
                Value = ""
            });
            foreach (var item in data)
            {
                employeeEditViewModel.DepartmentList.Add(new SelectListItem
                {
                    Text = item.DepartmentName,
                    Value = Convert.ToString(item.DeptId)
                });
            }
            return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {

            employeeEditViewModel.DepartmentList = new List<SelectListItem>();
            AppDbContext appDbContext = new AppDbContext();
            var data = appDbContext.Departments.ToList();
            employeeEditViewModel.DepartmentList.Add(new SelectListItem
            {
                Text = "Select department",
                Value = ""
            });
            foreach (var item in data)
            {
                employeeEditViewModel.DepartmentList.Add(new SelectListItem
                {
                    Text = item.DepartmentName,
                    Value = Convert.ToString(item.DeptId)
                });
            }
            if (ModelState.IsValid)
            {
                Employee employee = _EmployeeRepository.GetDetailsById(model.Id);
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Gender = model.Gender;
                employee.DOB = model.DOB;
                employee.JD = model.JD;
                employee.Email = model.Email;
                employee.Java = model.Java;
                employee.Cpp = model.Cpp;
                employee.CSharp = model.CSharp;
                employee.DeptId = model.DeptId;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadFile(model);
                }

                _EmployeeRepository.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }

            return View();
        }
        //---------departments controller------------
        public IActionResult DIndex()
        {
            var model = _DepartmentRepository.GetAllDepartment();
            return View(model);
        }
        [HttpGet]
        public ViewResult DCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DCreate(Department model)
        {
            if (ModelState.IsValid)
            {
                Department newDepartment = new Department
                {
                    DepartmentName = model.DepartmentName,
                    ManagerName = model.ManagerName,
                    Location = model.Location
                };
                _DepartmentRepository.Add(newDepartment);
                return RedirectToAction("DIndex");
            }

            return View();
        }
        [HttpGet]
        [Route("/home/DEdit/{DeptId:int}")]
        public IActionResult DEdit(int DeptId)
        {
            Department department = _DepartmentRepository.GetDepartment(DeptId);
            DepartmentEditViewModel departmentEditViewModel = new DepartmentEditViewModel()
            {
                DeptId = department.DeptId,
                DepartmentName = department.DepartmentName,
                ManagerName = department.ManagerName,
                Location = department.Location
            };
            return View(departmentEditViewModel);

        }
        [HttpPost]
        public IActionResult DEdit(DepartmentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Department department = _DepartmentRepository.GetDepartment(model.DeptId);
                department.DepartmentName = model.DepartmentName;
                department.ManagerName = model.ManagerName;
                department.Location = model.Location;

                _DepartmentRepository.Update(department);
                return RedirectToAction("DIndex");
            }
            return View();
        }
        [Route("/home/DDelete/{DeptId:int}")]
        public IActionResult DDelete(int DeptId)
        {

            Department d = _DepartmentRepository.GetDepartment(DeptId);
            _DepartmentRepository.Delete(d.DeptId);
            return RedirectToAction("DIndex");

        }
        [Route("/home/DDetails/{DeptId:int}")]
        public IActionResult DDetails(int DeptId)
        {
            Department department = _DepartmentRepository.GetDepartment(DeptId);
            return View(department);
        }
        //-----------drop down-----------
        [HttpGet]
        public ActionResult drop()
        {
            DepartmentModel departmentModel = new DepartmentModel();
            departmentModel.DepartmentList = new List<SelectListItem>();

            AppDbContext deptContext = new AppDbContext();
            var data = deptContext.Departments.ToList();
            departmentModel.DepartmentList.Add(new SelectListItem
            {
                Text = "Select department",
                Value = ""
            });
            foreach (var item in data)
            {
                departmentModel.DepartmentList.Add(new SelectListItem
                {
                    Text = item.DepartmentName,
                    Value = Convert.ToString(item.DeptId)
                });
            }
            return View(departmentModel);
        }
        [HttpPost]
        public ActionResult drop(DepartmentModel departmentModel)
        {
            departmentModel.DepartmentList = new List<SelectListItem>();

            AppDbContext deptContext = new AppDbContext();
            var data = deptContext.Departments.ToList();
            departmentModel.DepartmentList.Add(new SelectListItem
            {
                Text = "Select department",
                Value = ""
            });
            foreach (var item in data)
            {
                departmentModel.DepartmentList.Add(new SelectListItem
                {
                    Text = item.DepartmentName,
                    Value = Convert.ToString(item.DeptId)
                });
            }
            ViewBag.value = departmentModel.DepartmentList;
            ViewBag.Text = departmentModel.DepartmentList.Where(m => m.Value == departmentModel.DeptId.ToString()).FirstOrDefault()?.Text;
            return View(departmentModel);
        }













































        //--------------Employee Not Found Action-----------------//
        public IActionResult EmployeeNotFound()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
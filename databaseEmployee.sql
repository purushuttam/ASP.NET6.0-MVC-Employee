Create database EmployeeDBCode
GO

USE [EmployeeDBCode]
GO
/****** Object:  Table [dbo].[Credentials]    Script Date: 10/1/2022 11:11:02 PM ******/
SET ANSI_NULLS ON
GO  
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credentials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[PassWord] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 10/1/2022 11:11:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DeptId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
	[ManagerName] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DeptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/1/2022 11:11:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[DOB] [datetime2](7) NOT NULL,
	[JD] [datetime2](7) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Java] [bit] NOT NULL,
	[Cpp] [bit] NOT NULL,
	[CSharp] [bit] NOT NULL,
	[DeptId] [int] NOT NULL,
	[PhotoPath] [nvarchar](max) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DeptId])
REFERENCES [dbo].[Departments] ([DeptId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employee_Department]
GO
/****** Object:  StoredProcedure [dbo].[spCredential]    Script Date: 10/1/2022 11:11:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spCredential]
@Id int = null,
@UserName nvarchar(50) = null,
@PassWord nvarchar(50) = null,
@action nvarchar(10) = null
as
begin
 if(@Id is null and @action is null)
  begin
   select * from credentials
  end
 if(@Id is not null and @action is null)
  begin
   select * from credentials where Id = @Id
  end
 if(@action = 'insert')
  begin
    insert into credentials values (@UserName,@PassWord)
  end
 if(@action = 'update')
  begin
   update credentials set 
   UserName = @UserName,
   PassWord = @PassWord
   Where Id = @Id
  end
  if(@action = 'delete')
   begin
     delete from credentials where Id = @Id
   end
end
GO
/****** Object:  StoredProcedure [dbo].[spCRUD]    Script Date: 10/1/2022 11:11:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spCRUD]
@id int = null,
@FirstName nvarchar(50) = null,
@LastName nvarchar(50) = null,
@Gender nvarchar(50) = null,
@DOB datetime2(7) = null,
@JD datetime2(7) = null,
@Email nvarchar(50) = null,
@Java bit = null,
@Cpp bit = null,
@CSharp bit = null,
@DeptId int = null,
@PhotoPath nvarchar(max) = null,
@action nvarchar(10) = null
as
begin
 if(@id is null and @action is null)
  begin
   select * from Employees
  end
 if(@id is not null and @action is null)
  begin
   select * from Employees where id = @id
  end
 if(@action = 'insert')
  begin
    insert into Employees values (@FirstName,@LastName,@Gender,@DOB,@JD,@Email,@Java,@Cpp,@CSharp,@DeptId,@PhotoPath)
  end
 if(@action = 'update')
  begin
   update Employees set 
   FirstName = @FirstName,
   LastName = @LastName,
   Gender = @Gender,
   DOB = @DOB,
   JD = @JD,
   Email = @Email,
   Java = @Java,
   Cpp = @Java,
   CSharp = @CSharp,
   DeptId = @DeptId,
   PhotoPath = @PhotoPath
   Where Id = @id
  end
  if(@action = 'delete')
   begin
     delete from Employees where id = @id
   end
end
GO
/****** Object:  StoredProcedure [dbo].[spDepartment]    Script Date: 10/1/2022 11:11:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spDepartment]
@DeptId int = null,
@DepartmentName nvarchar(50) = null,
@ManagerName nvarchar(50) = null,
@Location nvarchar(50) = null,
@action nvarchar(10) = null
as
begin
 if(@DeptId is null and @action is null)
  begin
   select * from Departments
  end
 if(@DeptId is not null and @action is null)
  begin
   select * from Departments where DeptId = @DeptId
  end
 if(@action = 'insert')
  begin
    insert into Departments values (@DepartmentName,@ManagerName,@Location)
  end
 if(@action = 'update')
  begin
   update Departments set 
   DepartmentName = @DepartmentName,
   ManagerName = @ManagerName,
   Location = @Location
   Where DeptId = @DeptId
  end
  if(@action = 'delete')
   begin
     delete from Departments where DeptId = @DeptId
   end
end
GO
/****** Object:  StoredProcedure [dbo].[spEmpDept]    Script Date: 10/1/2022 11:11:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spEmpDept]
@DeptId int = null,
@Id int = null
as 
begin
 if(@Id is null)
  begin
   select *
   from employees e inner join departments d
   on e.DeptId = d.DeptId 
  end
 if(@Id is not null)
  begin
   select *
   from employees e join departments d
   on e.DeptId = d.deptid where e.Id = @Id
  end
end
GO

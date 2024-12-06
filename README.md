# NF48_ConsoleApp_SQLServer_Logging_Debugging
+ Windows 11 64-bit CPUs
+ Git for Windows
+ SQL Server 2019
+ SQL Server Management Studio (SSMS)
+ SQL Server Profiler
+ Visual Studio 2019
+ .NET Framework 4.8
+ Console App (C#)
+ Microsoft.SqlServer.Management.Smo
+ Logging (Write to a text file)
+ Run your T-SQL through sqlcmd.exe
+ Debugging (DEBUG mode)
+ [SQLCMD.exe Utility](https://learn.microsoft.com/en-us/sql/tools/sqlcmd/sqlcmd-utility)
+ Poor Man's T-SQL Formatter
+ BareTail (View text/log file)
+ ILSpy (ILSpy_selfcontained_9.0.0.7833-preview3-x64.zip)
+ [Manually writing simple CRUD stored procedures](https://dev.to/peledzohar/t-sql-crud-procedures-auto-generator-1cl1)
+ [Retrieve SQL Server identity column values](https://learn.microsoft.com/en-us/sql/connect/ado-net/retrieve-identity-or-autonumber-values)
```
CREATE PROCEDURE dbo.InsertCategory
  @CategoryName nvarchar(15),
  @Identity int OUT
AS
INSERT INTO Categories (CategoryName) VALUES(@CategoryName)
SET @Identity = SCOPE_IDENTITY()
```
+ [Generic Repository](https://www.ben-morris.com/why-the-generic-repository-is-just-a-lazy-anti-pattern/)
```
public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> query);
    T GetByID(int id);
    void Add(T item);
    void Update(T item);
    void Delete(T item);
}
```

## SQL Server Management Objects (SMO) Programming Guide
+ Install-Package Microsoft.SqlServer.SqlManagementObjects
+ [SQL Server Management Objects (SMO) Programming Guide](https://learn.microsoft.com/en-us/sql/relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide)
+ [Reference Microsoft.SqlServer.Smo.dll](https://stackoverflow.com/questions/6453415/reference-microsoft-sqlserver-smo-dll)
+ C:\Program Files\Microsoft SQL Server\110\SDK\Assemblies\Microsoft.SqlServer.ConnectionInfo.dll
+ C:\Program Files\Microsoft SQL Server\110\SDK\Assemblies\Microsoft.SqlServer.Smo.dll
+ C:\Program Files (x86)\Microsoft SQL Server\100\SDK\Assemblies (for the 64bit version)
+ C:\Program Files\Microsoft SQL Server\100\SDK\Assemblies\
+ Microsoft.SqlServer.ConnectionInfo.dll
+ Microsoft.SqlServer.Smo.dll
+ Microsoft.SqlServer.Management.Sdk.Sfc.dll
+ Microsoft.SqlServer.SqlEnum.dll
+ For SQL Server 2016, this location is C:\Program Files (x86)\Microsoft SQL Server\130\SDK\Assemblies (for the 64bit version)
```
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

//NOTE: The content of script should not be included the "GO" statement
SqlConnection conn = new SqlConnection(sqlConnectionString);
SqlCommand cmd = new SqlCommand(script, conn);
cmd.ExecuteNonQuery();
```
+ [Execute TSQL Without using SMO?](https://stackoverflow.com/questions/8073170/execute-tsql-without-using-smo)
+ [How can I execute a .sql from C#?](https://stackoverflow.com/questions/1449646/how-can-i-execute-a-sql-from-c)
+ [Versioning your Database on a Budget with C# and SMO](https://www.codeproject.com/Tips/639743/Versioning-your-Database-on-a-Budget-with-Csharp-a)
+ [Run your T-SQL through sqlcmd.exe](https://bitmugger.blogspot.com/2008_04_01_archive.html)

## ILSpy
+ https://github.com/icsharpcode/ILSpy
+ https://github.com/icsharpcode/ILSpy/releases
+ ILSpy_selfcontained_9.0.0.7833-preview3-x64.zip

## Poor Man's T-SQL Formatter
+ https://github.com/TaoK/PoorMansTSqlFormatter
+ http://architectshack.com/poormanstsqlformatter.ashx

## Scriptio
+ [SQL Script Writer](https://github.com/fredatgithub/Scriptio)

## Schemazen
+ [Script and create SQL Server objects quickly](https://github.com/sethreno/schemazen)

## SQLServerScripter
+ [Easily generate scripts from a SQL Server database on the command line in Linux](https://github.com/mkurz/SQLServerScripter)

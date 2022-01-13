using InterviewTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        public ListController()
        {
        }

        /*
         * List API methods goe here
         * */

        [HttpGet]
        public Task<List<Employee>> Get()
        {
            var employees = new List<Employee>();
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();
            var queryCmd = connection.CreateCommand();
            queryCmd.CommandText = "SELECT id, Name, Value FROM Employees";

            using (var reader = queryCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Value = reader.GetInt32(2)
                    });
                }
            }
            return Task.FromResult(employees);
        }

        //[HttpGet]
        //public Task<List<Employee>> Get(int Id)
        //{
        //    var employee = new Employee();
        //    var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
        //    using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
        //    {
        //        connection.Open();
        //        var queryCmd = connection.CreateCommand();
        //        queryCmd.CommandText = "SELECT Id, Name, Value FROM Employees WHERE Id = @Id";
        //        queryCmd.Parameters.AddWithValue("@Id", employee.Id);
        //        using (var reader = queryCmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                employee.Add(new Employee
        //                {
        //                    Id = reader.GetInt32(0),
        //                    Name = reader.GetString(1),
        //                    Value = reader.GetInt32(2)
        //                });
        //            }
        //        }
        //        return employee;
        //    }
        //}

        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            //UPDATE Employees
            //    SET Name = @name, Value = @value 
            //    WHERE Id = @Id

            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var queryCmd = connection.CreateCommand();
                queryCmd.CommandText = "UPDATE Employees SET Name = @Name, Value = @Value WHERE Id = @Id";
                queryCmd.Parameters.AddWithValue("@Id", employee.Id);
                queryCmd.Parameters.AddWithValue("@Name", employee.Name);
                queryCmd.Parameters.AddWithValue("@Value", employee.Value);
                queryCmd.ExecuteNonQuery();
            }
            //// This function will increase all the values, but perhaps we would not want to update the employee we have modified
            //IncreaseEmployeesValues();
            return Ok();
        }

        [HttpPost]
        public ActionResult<Employee> Post(Employee employee)
        {
            //INSERT INTO Employees(name, value)
            //    VALUES(@name, @value)
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            //DELETE FROM Employees
            //    WHERE id = @id
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.Controllers
{
   
    [ApiController]
    public class listTodoController : ControllerBase
    {

        private IConfiguration _configuration;
        public listTodoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("get_tasks")]
        public JsonResult get_tasks()
        {
            string query = "select * from listTodo";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("mydb");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(SqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                }
            }

            return new JsonResult(table);
        }

        [HttpPost("add_tasks")]
        public JsonResult add_tasks([FromForm] string task)
        {
            string query = "insert into listTodo values (@task)";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("mydb");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(SqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@task", task);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPost("delete_task")]
        public JsonResult delete_task([FromForm] string id)
        {
            string query = "delete from listTodo where id=@id";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("mydb");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(SqlDatasource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut("edit_task")]
        public JsonResult edit_task([FromForm] string id, [FromForm] string task)
        {
            string query = "update listTodo set task = @task where id = @id";
            string SqlDatasource = _configuration.GetConnectionString("mydb");
            using (SqlConnection myConn = new SqlConnection(SqlDatasource)) 
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myCommand.Parameters.AddWithValue("@task", task);
                    myCommand.ExecuteNonQuery();
                }
            }

            return new JsonResult("Updated Successfully");
        }

    }
}

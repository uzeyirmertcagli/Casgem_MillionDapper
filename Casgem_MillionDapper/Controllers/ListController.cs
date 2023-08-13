using Casgem_MillionDapper.DAL;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Numerics;

namespace Casgem_MillionDapper.Controllers
{
    public class ListController : Controller
    {
        private readonly string _connectionString = "Server = LAPTOP-1DJS186I; initial catalog = CARPLATES; integrated security = true";


        public async Task<IActionResult> Index(string s = "-")
        {
            await using var conneciton = new SqlConnection(_connectionString);
            if (s == "-")
            {

                var values = conneciton.QueryAsync<Plates>("SELECT Top 100 * FROM PLATES ");
                return View(values);
            }
            else
            {
                var values = conneciton.QueryAsync<Plates>($"SELECT * FROM PLATES where BRAND = '{s}' ");
                return View(values);

            }

        }
    }
}

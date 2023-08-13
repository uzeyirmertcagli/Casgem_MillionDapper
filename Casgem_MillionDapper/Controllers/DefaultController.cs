using Casgem_MillionDapper.DAL.Dtos;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casgem_MillionDapper.Controllers
{
    public class DefaultController : Controller
    {
        private readonly string _connectionString = "Server = LAPTOP-1DJS186I; initial catalog = CARPLATES; integrated security = true; Connect Timeout=60000";

        public async Task<IActionResult> Index()
        {
            await using var conneciton = new SqlConnection(_connectionString);

            var brandMax = (await conneciton.QueryAsync<Brand>("SELECT TOP 1 BRAND, COUNT(*) AS count FROM PLATES GROUP BY BRAND ORDER BY count DESC")).FirstOrDefault();
            var brandMin = (await conneciton.QueryAsync<Brand>("SELECT TOP 1 BRAND, COUNT(*) AS count FROM PLATES GROUP BY BRAND ORDER BY count ASC")).FirstOrDefault();

            var plateMax = (await conneciton.QueryAsync<Plate>("SELECT TOP 1 SUBSTRING(PLATE, 1, 2) AS plate, COUNT(*) AS count FROM PLATES GROUP BY SUBSTRING(PLATE, 1, 2) ORDER BY count DESC")).FirstOrDefault();
            var plateMin = (await conneciton.QueryAsync<Plate>("SELECT TOP 1 SUBSTRING(PLATE, 1, 2) AS plate, COUNT(*) AS count FROM PLATES GROUP BY SUBSTRING(PLATE, 1, 2) ORDER BY count ASC")).FirstOrDefault();

            var shiftType = (await conneciton.QueryAsync<ShiftType>("SELECT TOP 1 SHIFTTYPE, COUNT(*) AS count FROM PLATES GROUP BY SHIFTTYPE ORDER BY count DESC")).FirstOrDefault();

            var fuelType = (await conneciton.QueryAsync<Fuel>("SELECT TOP 1 FUEL, COUNT(*) AS count FROM PLATES GROUP BY FUEL ORDER BY count DESC")).FirstOrDefault();

            var colorType = (await conneciton.QueryAsync<Color>("SELECT TOP 1 COLOR, COUNT(*) AS count FROM PLATES GROUP BY COLOR ORDER BY count DESC")).FirstOrDefault();



            ViewData["brandMax"] = brandMax.BRAND;
            ViewData["countMaxBrand"] = brandMax.Count;

            ViewData["brandMin"] = brandMin.BRAND;
            ViewData["countMinBrand"] = brandMin.Count;

            ViewData["plateMax"] = plateMax.PLATE;
            ViewData["countMaxPlate"] = plateMax.Count;

            ViewData["plateMin"] = plateMin.PLATE;
            ViewData["countMinPlate"] = plateMin.Count;

            ViewData["shiftType"] = shiftType.SHIFTTYPE;
            ViewData["shiftTypeCount"] = shiftType.Count;

            ViewData["fuelType"] = fuelType.FUEL;
            ViewData["fuelTypeCount"] = fuelType.Count;

            ViewData["colorType"] = colorType.COLOR;
            ViewData["colorTypeCount"] = colorType.Count;

            return View();

        }
    }

}

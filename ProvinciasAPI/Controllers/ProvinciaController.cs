using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProvinciasAPI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProvinciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinciaController : ControllerBase
    {
        private readonly ILogger<ProvinciaController> _logger;
        private readonly HttpClient _httpClient;

        public ProvinciaController(ILogger<ProvinciaController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll endpoint called");
            var response = await _httpClient.GetFromJsonAsync<ProvinciaResponse>("https://apis.datos.gob.ar/georef/api/provincias");
            if (response != null && response.Provincias != null)
            {
                var result = response.Provincias.Select(p => new { p.Nombre, p.Centroide.Lat, p.Centroide.Lon });
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("GetNombre")]
        public async Task<IActionResult> GetNombre([FromQuery] string nombre)
        {
            _logger.LogInformation("GetNombre endpoint called with parameter: {Nombre}", nombre);
            var response = await _httpClient.GetFromJsonAsync<ProvinciaResponse>($"https://apis.datos.gob.ar/georef/api/provincias?nombre={nombre}");
            if (response != null && response.Provincias != null && response.Provincias.Count > 0)
            {
                var provincia = response.Provincias.First();
                var result = new { provincia.Nombre, provincia.Centroide.Lat, provincia.Centroide.Lon };
                return Ok(result);
            }
            return NotFound();
        }
    }
}

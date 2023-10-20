using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromServices] IRegistrarUsuarioUseCase useCase)
        {
            var resposta = await useCase.Executar(new RequisicaoRegistrarUsuarioJson
            {
                Email = "matheus@gmail.com",
                Nome = "Matheus",
                Senha = "123321da",
                Telefone = "16 9 9244-8192"
            });
            
            return Ok(resposta);
        }
    };
}
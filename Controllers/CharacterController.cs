using Microsoft.AspNetCore.Mvc;

namespace RickAndMortyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
     private readonly FetchDataService _fetchDataService;

      public CharacterController(FetchDataService fetchDataService)
    {
        _fetchDataService = fetchDataService;
    }

    [HttpPost]
    public async Task<RequestResponse> SaveCharactersByLocation(int locationId) {

        var characters =  await _fetchDataService.SaveCharactersByLocation(locationId);
        Console.WriteLine(characters.Data);

        return new RequestResponse {
            Estado = EstadoRespuesta.OK,
            Mensaje = "Se agregaron los personajs de la locación indicada."
        };  
    }

    [HttpGet]
    public async Task<RequestResponse> GetCharacters() {

        var response = await _fetchDataService.GetCharacters();
        return new RequestResponse {
            Data = response.Data,
            Estado = EstadoRespuesta.OK,
            Mensaje = "Personajes obtenidos correctamente."
        }; 
    }
}
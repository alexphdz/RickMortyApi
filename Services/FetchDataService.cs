using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class FetchDataService {
    private readonly HttpClient _httpClient;
    private readonly RickMortyDbContext _rickMortyDbContext;

    public FetchDataService(HttpClient httpClient, RickMortyDbContext rickMortyDbContext)
    {
        _rickMortyDbContext = rickMortyDbContext;
        _httpClient = httpClient;
    }
    public async Task<RequestResponse> SaveCharactersByLocation(int locationId)
    {
        try {
            var location = await _httpClient.GetFromJsonAsync<Location>($"https://rickandmortyapi.com/api/location/{locationId}");
            if(location!.residents.Count == 0) {
                return new RequestResponse { Mensaje = "Esta locación no contiene residentes.", Estado = EstadoRespuesta.INFO }; 
            }
            var residentsUrls = location.residents.Take(5);

            foreach (var residentUrl in residentsUrls)
            {
                var resident = await _httpClient.GetFromJsonAsync<Character>(residentUrl);

                await _rickMortyDbContext.Set<Character>().AddAsync(resident!);
            }

            await _rickMortyDbContext.SaveChangesAsync();

            return new RequestResponse { Mensaje = "Obtenidos", Estado = EstadoRespuesta.OK }; 
            // return response!.residents;
        } catch(Exception ex) {
            return new RequestResponse { Mensaje = "Ocurrió un error al guardar los personajes." + ex.Message, Estado = EstadoRespuesta.ERROR }; 
        }
    }

    public async Task<RequestResponse> GetCharacters() {
        try {
            List<Character> characters = await _rickMortyDbContext.Characters.ToListAsync();
            return new RequestResponse { Mensaje = "Personajes obtenidos.", Data =  characters, Estado = EstadoRespuesta.OK };
        } catch(Exception ex) {
            return new RequestResponse { Mensaje = "Ocurrió un error al obtener los personajes." + ex.Message, Estado = EstadoRespuesta.ERROR }; 
        }
    }
}
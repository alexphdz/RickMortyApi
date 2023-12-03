using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

public class FetchDataService {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly RickMortyDbContext _rickMortyDbContext;

    public FetchDataService(HttpClient httpClient, RickMortyDbContext rickMortyDbContext, IConfiguration configuration)
    {
        _rickMortyDbContext = rickMortyDbContext;
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task<RequestResponse> SaveCharactersByLocation(int locationId)
    {
        try {
            string rickandmortyapi = _configuration["Endpoints:RickAndMortyApi"]!;
            var location = await _httpClient.GetFromJsonAsync<Location>($"{rickandmortyapi}/location/{locationId}");
            if(location!.Residents.Count == 0) {
                return new RequestResponse { Mensaje = "Esta locación no contiene residentes.", Estado = EstadoRespuesta.INFO }; 
            }
            var residentsUrls = location.Residents.Take(5);

            foreach (var residentUrl in residentsUrls)
            {
                List<Episode> characterEpisodes = new List<Episode>();
                HttpResponseMessage response = await _httpClient.GetAsync(residentUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Character? character = JsonConvert.DeserializeObject<Character>(json);
                    
                    foreach(var ep in character!.Episode) {
                        HttpResponseMessage episodeResponse  = await _httpClient.GetAsync(ep);
                        string episodeJson = await episodeResponse.Content.ReadAsStringAsync();
                        Episode episode = JsonConvert.DeserializeObject<Episode>(episodeJson)!;
                        if (!_rickMortyDbContext.Set<Episode>().Local.Any(e => e.Id == episode.Id))
                        {
                            characterEpisodes.Add(episode);
                        }   
                    }
                    character.Episodes = characterEpisodes;
                    await _rickMortyDbContext.Set<Character>().AddAsync(character);
                    await _rickMortyDbContext.Set<Episode>().AddRangeAsync(characterEpisodes);
                }
                else
                {
                    return new RequestResponse { Mensaje = "Ocurrió un error al guardar los personajes." , Estado = EstadoRespuesta.ERROR }; 
                }
            }

            await _rickMortyDbContext.SaveChangesAsync();
            return new RequestResponse { Mensaje = "Obtenidos", Estado = EstadoRespuesta.OK }; 
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
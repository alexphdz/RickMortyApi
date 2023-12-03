
public enum EstadoRespuesta { OK, ALERT, INFO, ERROR, NO_AUTORIZADO }

public class RequestResponse
{
    public EstadoRespuesta Estado { get; set; } = EstadoRespuesta.OK;
    public string? Mensaje { get; set; }
    public object? Data { get; set; }
}


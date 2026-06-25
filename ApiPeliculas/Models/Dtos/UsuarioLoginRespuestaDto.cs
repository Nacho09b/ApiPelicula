namespace ApiPeliculas.Models.Dtos
{
    public class UsuarioLoginRespuestaDto
    {
        public UsuarioDatosDTO Usuario { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}

namespace ApiPeliculas.Models.Dtos
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Duracion { get; set; }
        public string RutaImagen { get; set; }
        public enum TipoClasificacion { A, B, B15, C, D }
        public TipoClasificacion Clasificacion { get; set; }
        public int CategoriaId { get; set; }
    }
}

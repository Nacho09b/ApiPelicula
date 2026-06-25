namespace ApiPeliculas.Models.Dtos
{
    public class CrearPeliculaDto
    {
        //public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string RutaImagen { get; set; }
        public enum CrearTipoClasificacion {A,B,B15,C,D}
        public CrearTipoClasificacion Clasificacion { get; set; }
        public int CategoriaId { get; set; }
    }
}

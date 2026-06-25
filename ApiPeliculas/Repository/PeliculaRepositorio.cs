using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace ApiPeliculas.Repository
{
    public class PeliculaRepositorio : IPeliculaRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public PeliculaRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public ICollection<Pelicula> GetPeliculas()
        {
            return _bd.Pelicula.OrderBy(p => p.Nombre).ToList();
        }

        public ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId)
        {
            return _bd.Pelicula.Where(p => p.CategoriaId == categoriaId).ToList();
            //return _bd.Pelicula.Include(p => p.Nombre).Where(p => p.CategoriaId == categoriaId).ToList();
        }

        public IEnumerable<Pelicula> BuscarPelicula(string nombre)
        {
            //return _bd.Pelicula.Where(p => p.Nombre.Contains(nombre)).ToList();
            IQueryable<Pelicula> query = _bd.Pelicula;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre) || p.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public Pelicula GetPelicula(int peliculaId)
        {
            return _bd.Pelicula.FirstOrDefault(p => p.Id == peliculaId);
            
        }

        public bool ExistePelicula(int id)
        {
            return _bd.Pelicula.Any(p => p.Id == id);
        }

        public bool ExistePelicula(string nombre)
        {
            return _bd.Pelicula.Any(p => p.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public bool CrearPelicula(Pelicula pelicula)
        {
            _bd.Pelicula.Add(pelicula);
            return Guardar();
        }

        public bool ActualizarPelicula(Pelicula pelicula)
        {
            pelicula.FechaCreacion = DateTime.Now;
            var peliculaExistente = _bd.Pelicula.FirstOrDefault(p => p.Id == pelicula.Id);
            if (peliculaExistente != null)
            {
               _bd.Entry(peliculaExistente).CurrentValues.SetValues(pelicula);
            }
            else
            {
                _bd.Pelicula.Update(pelicula);
            }
            
            return Guardar();
        }

        public bool BorrarPelicula(Pelicula pelicula)
        {
            _bd.Pelicula.Remove(pelicula);
            return Guardar();
        }
    }
}

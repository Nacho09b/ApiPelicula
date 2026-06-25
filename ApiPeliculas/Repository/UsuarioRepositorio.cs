using ApiPeliculas.Data;
using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using System.Security.Cryptography;

namespace ApiPeliculas.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public UsuarioRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public Usuario GetUsuario(int usuarioId)
        {
            return _bd.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuarios.OrderBy(u => u.NombreUsuario).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            //_bd.Usuarios.Any(u => u.Nombre == usuario);
            var usuarioBD = _bd.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario);
            return usuarioBD == null ? true : false;
        }

        public Task<UsuarioLoginDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var PasswordeNCrypted = ObtenerMd5(usuarioRegistroDto.Password);//BCrypt.Net.BCrypt.HashPassword(UsuarioRegistroDto.Password);
            Usuario usuario = new Usuario()
            {
                NombreUsuario = usuarioRegistroDto.NombreUsuario,
                Nombre = usuarioRegistroDto.Nombre,
                Password = PasswordeNCrypted,
                Role = usuarioRegistroDto.Role
            };

            _bd.Usuarios.Add(usuario);
            await _bd.SaveChangesAsync();
            usuario.Password = PasswordeNCrypted;
            return usuario;

        }

        //Metodo para encriptar Password con MD5 se usa para encriptar la contraseña del usuario antes de guardarla en la base de datos
        public static string ObtenerMd5(string input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(input);
            data = md5.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();]
            }
            return resp;
        }

        
    }
}

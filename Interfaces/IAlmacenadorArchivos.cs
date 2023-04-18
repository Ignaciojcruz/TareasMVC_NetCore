using TareasMVC_NetCore.Models;

namespace TareasMVC_NetCore.Interfaces
{
    public interface IAlmacenadorArchivos
    {
        Task Borrar(string ruta, string contenedor);
        Task<AlmacenarArchivoResultado[]> Almacenar(string contenedor, IEnumerable<IFormFile> archivos);
    }
}

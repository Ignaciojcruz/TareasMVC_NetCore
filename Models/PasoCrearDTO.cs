using System.ComponentModel.DataAnnotations;

namespace TareasMVC_NetCore.Models
{
    public class PasoCrearDTO
    {
        [Required]
        public string Descripcion { get; set; }
        public bool Realizado { get; set; }
    }
}

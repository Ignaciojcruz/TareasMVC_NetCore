using System.ComponentModel.DataAnnotations;

namespace TareasMVC_NetCore.Models
{
    public class TareaEditarDTO
    {
        [Required]
        [StringLength(250)]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}

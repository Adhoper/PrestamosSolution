using System.ComponentModel.DataAnnotations;

namespace Prestamos.Web.Models
{
    public class PrestamoViewModel
    {
        public PrestamoViewModel()
        {
            FechaNacimiento = DateTime.Today;
        }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        [Display(Name = "Monto del préstamo")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La cantidad de meses es obligatoria.")]
        [Display(Name = "Meses del préstamo")]
        public int Meses { get; set; }

        // Resultado
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public int? Edad { get; set; }
        public decimal? Tasa { get; set; }
        public decimal? Cuota { get; set; }

        public List<int> OpcionesMeses { get; set; } = new() { 3, 6, 9, 12 };
    }
}

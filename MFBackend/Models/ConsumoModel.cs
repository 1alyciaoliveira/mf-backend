using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MFBackend.Models
{
    [Table("Consumo")]
    public class Consumo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Obrigatório informar a descrição!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage ="Obrigatório informar a data!")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage ="Obrigatório informar o valor!")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage ="Obrigatório informar a quilometragem!")]
        public int Km { get; set; }

        [Display(Name = "Tipo de Combustível")]
        public TipoCombustivel Tipo { get; set; }

        [Display(Name = "Veículo")]
        [Required(ErrorMessage ="Obrigatório informar o veículo!")]
        [ForeignKey("VeiculoId")]
        public int VeiculoId { get; set; }
      
        public Veiculos? Veiculo { get; set; }
    }

    public enum TipoCombustivel
    {
        Gasolina,
        Etanol,
    }
}
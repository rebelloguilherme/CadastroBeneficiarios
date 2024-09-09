using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }        
        public long IdCliente { get; set; }        

        /// <summary>
        /// CPF
        /// </summary>
        [Required]//TODO: ADD front-end validation
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

    }
}
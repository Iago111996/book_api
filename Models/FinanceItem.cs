using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Models
{
    [Table("tb_finances")]
    public class FinanceItem
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FIN_ID { get; set; }

        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime FIN_DCREATE { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório")]
        public string FIN_VCATEGORY { get; set; }

        [StringLength(300, ErrorMessage = "O gênero não pode estrapolar 300 caracteres")]
        public string FIN_VTITLE { get; set; }

        [Required(ErrorMessage = "O campo valor é obrigatório")]
        public double FIN_DVALUE { get; set; }

        [Required(ErrorMessage = "O campo tipo é obrigatório")]
        public bool FIN_BTYPE { get; set; }
    }
}
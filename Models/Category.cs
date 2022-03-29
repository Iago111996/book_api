using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Models
{

    [Table("tb_categories")]
    public class Category
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CAT_ID { get; set; }

        [Required(ErrorMessage = "O campo chave é obrigatório")]
        public string CAT_VKEY { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string CAT_VNAME { get; set; }

        [Required(ErrorMessage = "O campo cor é obrigatório")]
        public string CAT_VCOLOR { get; set; }

        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime CAT_DCREATE { get; set; }
    }

}
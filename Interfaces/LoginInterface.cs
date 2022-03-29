using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Interfaces
{
    public class LoginInterface
    {
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email invalido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório.")]
        [StringLength(20, ErrorMessage = "A senha deve ter no mínimo 8, e no máximo 20 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
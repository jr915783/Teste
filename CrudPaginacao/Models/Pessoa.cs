using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudPaginacao.Models
{
    public class Pessoa
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Numero máximo de caracteres atingido !")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(14, ErrorMessage = "Cpf inválido!")]
        [MinLength(14, ErrorMessage = "Cpf inválido!")]
        [Remote("ValidarCpf", "Pessoas", HttpMethod = "Post", ErrorMessage = "Cpf  Já cadastrado", AdditionalFields = "Id")]
        public string Cpf { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido !")]
        public string Email { get; set; }
        [MinLength(12, ErrorMessage = "Telefone inválido!")]
        public string Telefone { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Campo Cep obrigatório!")]
        [MaxLength(100)]
        public string Cep { get; set; }
        [MaxLength(100)]
        public string Rua { get; set; }
        [MaxLength(100)]
        public string Bairro { get; set; }
        [MaxLength(100)]
        public string Cidade { get; set; }
        [MaxLength(100)]
        public string Uf { get; set; }

        public bool Ativo { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyBuySDK.Models
{
    [Table("Estabelecimento")]
    public class Estabelecimento
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string CaminhoImagem { get; set; }

        public virtual List<Produto> Produtos { get; set; }
    }
}
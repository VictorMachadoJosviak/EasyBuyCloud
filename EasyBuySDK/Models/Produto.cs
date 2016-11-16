using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EasyBuySDK.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        public string Marca { get; set; }

        public double Preco { get; set; }

        public bool Disponivel { get; set; }

        public virtual Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }

        public virtual Estabelecimento Estabelecimento { get; set; }

        public int EstabelecimentoId { get; set; }

        public string CaminhoImagem { get; set; }
    }
}
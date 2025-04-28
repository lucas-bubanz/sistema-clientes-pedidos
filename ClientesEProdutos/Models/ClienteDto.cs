using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesEProdutos.Models
{
    public class ClienteDto
    {
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EnderecoCliente { get; set; }
    }
}
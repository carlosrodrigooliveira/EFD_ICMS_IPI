using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SPED.Model
{
    [Serializable]
    public class Produto_Conversao
    {
        /// <summary>
        /// Unidade comercial a ser convertida na unidade de estoque, referida no registro 0200.
        /// </summary>
        public String UNID_CONV {get;set;}

        /// <summary>
        /// Fator de conversão: fator utilizado para converter (multiplicar) a unidade a ser convertida na unidade adotada no inventário.
        /// </summary>
        public Decimal FAT_CONV { get; set; }

    }
}

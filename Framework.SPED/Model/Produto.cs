using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SPED.Model
{
    [Serializable]
    public class Produto
    {
        /// <summary>
        /// Código do item 
        /// </summary>
        public String COD_ITEM { get; set; }

        /// <summary>
        /// Descrição do item
        /// </summary>
        public String DESCR_ITEM { get; set; }

        /// <summary>
        /// Representação alfanumérico do código de barra do produto, se houver
        /// </summary>
        public String COD_BARRA { get; set; }

        /// <summary>
        /// Código anterior do item com relação à última  informação apresentada.
        /// </summary>
        public String COD_ANT_ITEM { get; set; }

        /// <summary>
        /// Unidade de medida utilizada na quantificação de estoques.
        /// </summary>
        public String UNID_INV { get; set; }

        /// <summary>
        ///     Tipo do item – Atividades Industriais, Comerciais e Serviços:
        ///     00 – Mercadoria para Revenda;
        ///     01 – Matéria-prima;
        ///     02 – Embalagem;
        ///     03 – Produto em Processo;
        ///     04 – Produto Acabado;
        ///     05 – Subproduto;
        ///     06 – Produto Intermediário;
        ///     07 – Material de Uso e Consumo;
        ///     08 – Ativo Imobilizado;
        ///     09 – Serviços;
        ///     10 – Outros insumos;
        ///     99 – Outras
        /// </summary>
        public Int32 TIPO_ITEM { get; set; }

        /// <summary>
        /// Código da Nomenclatura Comum do Mercosul
        /// </summary>
        public String COD_NCM { get; set; }

        /// <summary>
        /// Código EX, conforme a TIPI
        /// </summary>
        public String EX_IPI { get; set; }

        /// <summary>
        /// Código do gênero do item, conforme a Tabela 4.2.1
        /// </summary>
        public Int32 COD_GEN { get; set; }

        /// <summary>
        /// Código do serviço conforme lista do Anexo I da Lei Complementar Federal nº 116/03.
        /// </summary>
        public String COD_LST { get; set; }

        /// <summary>
        /// Alíquota de ICMS aplicável ao item nas operações internas
        /// </summary>
        public Decimal ALIQ_ICMS { get; set; }

        /// <summary>
        /// Código Especificador da Substituição Tributária
        /// </summary>
        public Int32 CEST { get; set; }

        /// <summary>
        /// REGISTRO 0220: FATORES DE CONVERSÃO DE UNIDADES
        /// </summary>
        public IList<Produto_Conversao> Conversao {get;set;}
    }
}

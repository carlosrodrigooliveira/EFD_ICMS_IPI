using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SPED.Model
{
    [Serializable]
    public class Participante
    {
        /// <summary>
        /// Código de identificação do participante no arquivo.
        /// </summary>
        public String COD_PART { get; set; }

        /// <summary>
        /// Nome pessoal ou empresarial do participante.
        /// </summary>
        public String NOME { get; set; }

        /// <summary>
        /// Nome de fantasia associado ao nome empresarial
        /// </summary>
        public String FANTASIA { get; set; }
         
        /// <summary>
        /// Indica o tipo de participantes 
        /// 1 -  Pessoa Física |  2 - Pessoa Jurídica
        /// </summary>
        public TipoParticipante TipParticipante { get; set; }

        /// <summary>
        /// CPF do participante / CNPJ do participante
        /// </summary>
        public String CPF_CNPJ { get; set; }

        /// <summary>
        /// Inscrição Estadual do participante
        /// </summary>
        public String IE { get; set; }

        /// <summary>
        /// Inscrição Municipal da entidade.
        /// </summary>
        public String IM { get; set; }

        /// <summary>
        /// Código de Endereçamento Postal, e preenchido com zero por default
        /// </summary>
        public Int32 CEP { get; set; }

        /// <summary>
        /// Código do país do participante, conforme a tabela indicada no item 3.2.1 no site do sped (IBGE)
        /// </summary>
        public Int32 COD_PAIS { get; set; }

        /// <summary>
        /// Sigla da unidade da federação da entidade.
        /// </summary>
        public String UF { get; set; }

        /// <summary>
        /// Código do município, conforme a tabela IBGE
        /// </summary>
        public Int32 COD_MUN { get; set; }

        /// <summary>
        /// Número de inscrição do participante na SUFRAMA
        /// </summary>
        public String SUFRAMA { get; set; }

        /// <summary>
        /// Logradouro e endereço do imóvel
        /// </summary>
        public String END { get; set; }

        /// <summary>
        /// Número do imóvel 
        /// </summary>
        public String NUM { get; set; }

        /// <summary>
        /// Dados complementares do endereço
        /// </summary>
        public String COMPL { get; set; }

        /// <summary>
        /// Bairro em que o imóvel está situado
        /// </summary>
        public String BAIRRO { get; set; }

        /// <summary>
        /// Número do telefone (DDD+FONE). 
        /// </summary>
        public String FONE { get; set; }

        /// <summary>
        /// Número do fax
        /// </summary>
        public String FAX { get; set; }

        /// <summary>
        /// Endereço do correio eletrônico
        /// </summary>
        public String EMAIL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SPED.Model.EFD_ICMS
{
    [Serializable]
    public class EFD_ICMS_IPI
    {
        #region Dados da Escrituração (REGISTRO 0000)

        /// <summary>
        /// REGISTRO 0000 - Código da versão do leiaute conforme a tabela indicada no Ato COTEPE.
        /// </summary>
        public String COD_VER { get; set; }

        /// <summary>
        /// REGISTRO 0000 - Código da finalidade do arquivo: 
        /// 0 - Remessa do arquivo original;
        /// 1 - Remessa do arquivo substituto.
        /// </summary>
        public String COD_FIN { get; set; }

        /// <summary>
        /// REGISTRO 0000 - Data inicial das informações contidas no arquivo.
        /// </summary>
        public DateTime DT_INI { get; set; }

        /// <summary>
        /// REGISTRO 0000 - Data final das informações contidas no arquivo.
        /// </summary>
        public DateTime DT_FIN { get; set; }

        /// <summary>
        ///  REGISTRO 0000 - Perfil de apresentação do arquivo fiscal;
        ///  A – Perfil A; B – Perfil B; C – Perfil C;
        /// </summary>
        public String IND_PERFIL { get; set; }

        /// <summary>
        ///  REGISTRO 0000 - Indicador de tipo de atividade;
        ///  0 – Industrial ou equiparado a industrial; 1 – Outros.
        /// </summary>
        public String IND_ATIV { get; set; }
        


        #endregion

        #region Controle do Arquivo / Recibo / Erro 

        #region Arquivo

        /// <summary>
        /// Nome do arquivo TXT importado
        /// </summary>
        public String Arquivo_Nome { get; set; }

        /// <summary>
        /// Tamanho do arquivo do arquivo em MB
        /// </summary>
        public Decimal Arquivo_Tamanho { get; set; }

        /// <summary>
        /// Hash do arquivo, calculado a partir dos  Bytes do arquivo para ser utilizando como identificador único
        /// </summary>
        public String Arquivo_Hash { get; set; }

        /// <summary>
        /// Hash do conteudo do arquivo, calculado a partir de todas as linhas do arquivo e removendo a assinatura do mesmo.
        /// </summary>
        public String Arquivo_HashConteudo { get; set; }

        #endregion

        #region Recibo

        /// <summary>
        /// Nome do arquivo do Recibo de entrega da declaração 
        /// </summary>
        public String Recibo_NomeArquivo { get; set; }

        /// <summary>
        /// Tamanho do Recibo de entrega da declaração
        /// </summary>
        public Decimal Recibo_Tamanho { get; set; }

        /// <summary>
        /// Hash do conteúdo do arquivo sem a assinatura enviada para a SEFAZ
        /// </summary>
        public String Recibo_HashArquivoTransmitido { get; set; }
         
        /// <summary>
        /// Assinatura da transmissão gerada pelo ReceitaNet
        /// </summary>
        public String Recibo_AssinaturaTransmissao { get; set; }
         
        /// <summary>
        /// Hash da assinatura do recibo
        /// </summary>
        public String Recibo_HashAssinatura { get; set; }

        /// <summary>
        /// Data de envio da declaração a SEFAZ
        /// </summary>
        public String Recibo_DataEnvio { get; set; }

        #endregion

        #region Erro

        public String Linha { get; set; }
        public String LinhaAnterior { get; set; }

        public Boolean PossuiErro { get; set; }
        public String Erro { get; set; }
        public Int32 LinhaAtual { get; set; }

        #endregion

        #endregion

        #region Participante / Contribuinte

        /// <summary>
        ///  Contribuinte da EFD ICMS IPI, utiliza o merge dos registros "0000" - Abertura do Arquivo Digital e Identificação da entidade  e "0005" -Dados Complementares da entidade para realizar o preenchimento.    
        /// </summary>
        public Participante Entidade { get; set; }

        /// <summary>
        /// Registro 0150 - Tabela de Cadastro do Participante
        /// </summary>
        public IList<Participante> Participantes { get; set; }

        #endregion

        #region Produto

        public IList<Produto> Produtos { get; set; }

        #endregion
    }
}

using Framework.SPED.Model;
using Framework.SPED.Model.EFD_ICMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SPED
{
    public static class EFD_ICMS_IPI_BO
    {
        #region Propriedade e Enum 

        public enum TipoParticipante
        {
            Todos = -1,
            Contribuinte = 0,
            Participante = 1
        }

        #endregion

        #region Participante

        public static IList<Participante> GetParticipantes(String arquivo, TipoParticipante tipoParticipante)
        {
            Stream arquivoStream = null;

            try
            {
                arquivoStream = new FileStream(arquivo, FileMode.Open, FileAccess.Read);
                return GetParticipantes(arquivoStream, tipoParticipante);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (arquivoStream != null)
                    arquivoStream.Close();

                arquivoStream = null;
            }
        }

        public static IList<Participante> GetParticipantes(Stream arquivo, TipoParticipante tipoParticipante)
        {
            return new List<Participante>();
        }

        private static Participante PreencheParticipante(String linha)
        {
            String[] colunas = linha.Split('|');

            Participante partipante = new Participante();

            partipante.COD_PART = Facilities.TextoIsNullOrEmpty(colunas[2]);
            partipante.NOME = Facilities.TextoIsNullOrEmpty(colunas[3]);
            partipante.FANTASIA = partipante.NOME;
            partipante.COD_PAIS = Facilities.ConverterInt32SPED(colunas[4]);

            if (String.IsNullOrEmpty(colunas[5]) == false)
            {
                partipante.TipParticipante = SPED.TipoParticipante.PessoaJuridica;
                partipante.CPF_CNPJ = colunas[5];
            }
            else
            {
                partipante.TipParticipante = SPED.TipoParticipante.PessoaFisica;
                partipante.CPF_CNPJ = colunas[6];
            }

            partipante.IE = Facilities.TextoIsNullOrEmpty(colunas[7]);
            partipante.COD_MUN = Facilities.ConverterInt32SPED(colunas[8]);
            partipante.SUFRAMA = Facilities.TextoIsNullOrEmpty(colunas[9]);
            partipante.END = Facilities.TextoIsNullOrEmpty(colunas[10]);
            partipante.NUM = Facilities.TextoIsNullOrEmpty(colunas[11]);
            partipante.COMPL = Facilities.TextoIsNullOrEmpty(colunas[12]);
            partipante.BAIRRO = Facilities.TextoIsNullOrEmpty(colunas[13]);

            return partipante;
        }


        #endregion

        #region Produto

        private static Produto PreencheProduto(String linha)
        {
            String[] colunas = linha.Split('|');

            Produto produto = new Produto();

            produto.COD_ITEM = Facilities.TextoIsNullOrEmpty(colunas[2]);
            produto.DESCR_ITEM = Facilities.TextoIsNullOrEmpty(colunas[3]);
            produto.COD_BARRA = Facilities.TextoIsNullOrEmpty(colunas[4]);
            produto.COD_ANT_ITEM = Facilities.TextoIsNullOrEmpty(colunas[5]);
            produto.UNID_INV = Facilities.TextoIsNullOrEmpty(colunas[6]);
            produto.TIPO_ITEM = Facilities.ConverterInt32SPED(colunas[7]);
            produto.COD_NCM = Facilities.TextoIsNullOrEmpty(colunas[8]);
            produto.EX_IPI = Facilities.TextoIsNullOrEmpty(colunas[9]);
            produto.COD_GEN = Facilities.ConverterInt32SPED(colunas[10]);
            produto.COD_LST = Facilities.TextoIsNullOrEmpty(colunas[11]);
            produto.ALIQ_ICMS = Facilities.ConverterDecimalSPED(colunas[12]);
            produto.CEST = Facilities.ConverterInt32SPED(colunas[13]);

            produto.Conversao = new List<Produto_Conversao>();

            return produto;
        }

        private static Produto_Conversao PreencheProdutoFatorConversao(String linha)
        {
            String[] colunas = linha.Split('|');

            Produto_Conversao produto_conversao = new Produto_Conversao();

            produto_conversao.UNID_CONV = Facilities.TextoIsNullOrEmpty(colunas[2]);
            produto_conversao.FAT_CONV = Facilities.ConverterDecimalSPED(colunas[3]);

            return produto_conversao;
        }

        #endregion

        #region Processa Arquivo

        /// <summary>
        /// Método Responsável em processar o arquivo de EFD ICMS e retornar o objeto equivalente a escrituração
        /// </summary>
        /// <param name="arquivo">Arquivo da EFD ICMS no formato original em txt</param>
        /// <param name="pathTemporario">Diretório temporário utilizado para salvar o arquivo temporário para cálculo dos controles do arquivo</param>
        /// <param name="calcularControleArquivo">Calcula os Hash do arquivo que serão utilizados para controle, nas propriedades abaixo:
        /// 1) Arquivo_Hash = Hash do arquivo, calculado a partir dos  Bytes do arquivo para ser utilizando como identificador único
        /// 2) Arquivo_HashConteudo = Hash do conteudo do arquivo, calculado a partir de todas as linhas do arquivo e removendo a assinatura do mesmo
        /// </param>
        /// <param name="participante">True - Lista todos os participantes referente ao registro 0150 | False = Não retorna participante</param>
        /// <param name="produto">True - Lista todos os produtos e conversões referente aos registro 0200 e 0220 | False = Não retorna nenhum produto</param>
        /// <returns>Retorna a escrituração EFD ICMS IPI, em caso de erro retorna NULL</returns>        
        public static EFD_ICMS_IPI GetEscrituracao(Stream arquivo, String pathTemporario, Boolean calcularControleArquivo, Boolean participante, Boolean produto)
        {
            #region Declaração Variáveis 

            String[] colunas;
            StreamReader arquivoLeitura = null;
            EFD_ICMS_IPI efd_icms_ipi = new EFD_ICMS_IPI() { Linha = "", LinhaAnterior = "", LinhaAtual = 0, Erro = "", PossuiErro = false };

            #endregion

            try
            {
                #region Controle arquivo

                if (calcularControleArquivo)
                {
                    String nome_arquivo = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".txt";

                    efd_icms_ipi.Arquivo_Tamanho = Facilities.GetBytesParaMega(arquivo.Length);
                    efd_icms_ipi.Arquivo_Hash = Facilities.GetHash<MD5>(arquivo).ToUpper();
                    efd_icms_ipi.Arquivo_HashConteudo = Facilities.GetHashSemAssinatura(arquivo, pathTemporario, nome_arquivo).ToUpper(); 
                }
                else
                {
                    efd_icms_ipi.Arquivo_Tamanho = 0;
                    efd_icms_ipi.Arquivo_Hash = "";
                    efd_icms_ipi.Arquivo_HashConteudo = "";
                }

                #endregion

                arquivo.Position = 0;
                arquivoLeitura = new StreamReader(arquivo, Encoding.GetEncoding("ISO-8859-1"));

                #region Registo 0000

                efd_icms_ipi.LinhaAtual++;
                efd_icms_ipi.Linha = arquivoLeitura.ReadLine();

                if (efd_icms_ipi.Linha.Substring(0, 6) != "|0000|")
                    throw new Exception("Arquivo inválido.");

                colunas = efd_icms_ipi.Linha.Split('|');

                efd_icms_ipi.COD_VER = Facilities.TextoIsNullOrEmpty(colunas[2]);
                efd_icms_ipi.COD_FIN = Facilities.TextoIsNullOrEmpty(colunas[3]);
                efd_icms_ipi.DT_INI = Facilities.ConverterDataSPED(colunas[4]).Value;
                efd_icms_ipi.DT_FIN = Facilities.ConverterDataSPED(colunas[5]).Value;
                efd_icms_ipi.IND_PERFIL = Facilities.TextoIsNullOrEmpty(colunas[14]);
                efd_icms_ipi.IND_ATIV = Facilities.TextoIsNullOrEmpty(colunas[15]);

                #region Contribuinte

                efd_icms_ipi.Entidade = new Participante();

                efd_icms_ipi.Entidade.NOME = Facilities.TextoIsNullOrEmpty(colunas[6]);

                if (String.IsNullOrEmpty(colunas[7]) == false)
                {
                    efd_icms_ipi.Entidade.TipParticipante = SPED.TipoParticipante.PessoaJuridica;
                    efd_icms_ipi.Entidade.CPF_CNPJ = colunas[7];
                }
                else
                {
                    efd_icms_ipi.Entidade.TipParticipante = SPED.TipoParticipante.PessoaFisica;
                    efd_icms_ipi.Entidade.CPF_CNPJ = colunas[8];
                }

                efd_icms_ipi.Entidade.UF = Facilities.TextoIsNullOrEmpty(colunas[9]);
                efd_icms_ipi.Entidade.IE = Facilities.TextoIsNullOrEmpty(colunas[10]);
                efd_icms_ipi.Entidade.COD_MUN = Facilities.ConverterInt32SPED(colunas[11]);
                efd_icms_ipi.Entidade.IM = Facilities.TextoIsNullOrEmpty(colunas[12]);
                efd_icms_ipi.Entidade.SUFRAMA = Facilities.TextoIsNullOrEmpty(colunas[13]);

                #endregion

                efd_icms_ipi.Participantes = new List<Participante>();
                efd_icms_ipi.Produtos = new List<Produto>();

                #endregion

                #region Processa Registros

                while (!arquivoLeitura.EndOfStream)
                {
                    efd_icms_ipi.LinhaAtual++;
                    efd_icms_ipi.Linha = arquivoLeitura.ReadLine();

                    #region Registro 0005 - DADOS COMPLEMENTARES DA ENTIDADE (Contribuinte)

                    if (efd_icms_ipi.Linha.Substring(0, 6) == "|0005|")
                    {
                        colunas = efd_icms_ipi.Linha.Split('|');

                        efd_icms_ipi.Entidade.FANTASIA = Facilities.TextoIsNullOrEmpty(colunas[2]);
                        efd_icms_ipi.Entidade.CEP = Facilities.ConverterInt32SPED(colunas[3]);
                        efd_icms_ipi.Entidade.END = Facilities.TextoIsNullOrEmpty(colunas[4]);
                        efd_icms_ipi.Entidade.NUM = Facilities.TextoIsNullOrEmpty(colunas[5]);
                        efd_icms_ipi.Entidade.COMPL = Facilities.TextoIsNullOrEmpty(colunas[6]);
                        efd_icms_ipi.Entidade.BAIRRO = Facilities.TextoIsNullOrEmpty(colunas[7]);
                        efd_icms_ipi.Entidade.FONE = Facilities.TextoIsNullOrEmpty(colunas[8]);
                        efd_icms_ipi.Entidade.FAX = Facilities.TextoIsNullOrEmpty(colunas[9]);
                        efd_icms_ipi.Entidade.EMAIL = Facilities.TextoIsNullOrEmpty(colunas[10]);
                    }

                    #endregion

                    #region Registro 0150 - TABELA DE CADASTRO DO PARTICIPANTE

                    if (participante && efd_icms_ipi.Linha.Substring(0, 6) == "|0150|")
                    {
                        PreencheParticipante(efd_icms_ipi.Linha);

                        while (!arquivoLeitura.EndOfStream && ((efd_icms_ipi.Linha.Substring(0, 6) == "|0150|" || (efd_icms_ipi.Linha.Substring(0, 6) == "|0175|"))))
                        {
                            efd_icms_ipi.LinhaAtual++;
                            efd_icms_ipi.Linha = arquivoLeitura.ReadLine();

                            if (efd_icms_ipi.Linha.Substring(0, 6) == "|0150|")
                                efd_icms_ipi.Participantes.Add(PreencheParticipante(efd_icms_ipi.Linha));
                        }
                    }

                    #endregion


                    #region Registro 0200 -   TABELA DE IDENTIFICAÇÃO DO ITEM (PRODUTO E SERVIÇOS) (0200 | 0205 | 0206 | 0210 | 0220)

                    if (produto && efd_icms_ipi.Linha.Substring(0, 6) == "|0200|")
                    {
                        efd_icms_ipi.Produtos.Add(PreencheProduto(efd_icms_ipi.Linha));

                        while (!arquivoLeitura.EndOfStream && (efd_icms_ipi.Linha.Substring(0, 6) == "|0200|" || efd_icms_ipi.Linha.Substring(0, 6) == "|0205|" || efd_icms_ipi.Linha.Substring(0, 6) == "|0206|" || efd_icms_ipi.Linha.Substring(0, 6) == "|0210|" || efd_icms_ipi.Linha.Substring(0, 6) == "|0220|"))
                        {
                            efd_icms_ipi.LinhaAtual++;
                            efd_icms_ipi.Linha = arquivoLeitura.ReadLine();

                            if (efd_icms_ipi.Linha.Substring(0, 6) == "|0200|")
                                efd_icms_ipi.Produtos.Add(PreencheProduto(efd_icms_ipi.Linha));
                            else if (efd_icms_ipi.Linha.Substring(0, 6) == "|0220|")
                                efd_icms_ipi.Produtos[efd_icms_ipi.Produtos.Count -1].Conversao.Add(PreencheProdutoFatorConversao(efd_icms_ipi.Linha));  
                        }
                    }

                    #endregion


                    //Verifica se é a última linha e interrompe o processamento
                    if (efd_icms_ipi.Linha.Substring(0, 6) == "|9999|")
                        break;
                }

                #endregion

                return efd_icms_ipi;
            }
            catch (Exception ex)
            {
                efd_icms_ipi.Erro = ex.Message.ToString();
                efd_icms_ipi.PossuiErro = true;
                return efd_icms_ipi;
            }
            finally
            {
                #region Finally

                if (arquivoLeitura != null)
                    arquivoLeitura.Close();

                arquivoLeitura = null;

                #endregion
            }
        }

        #endregion


    }
}

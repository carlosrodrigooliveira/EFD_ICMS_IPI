using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Framework.SPED
{
    public static class Facilities
    {
        public static String RemoveCaracteresEspeciais(String texto)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
            for (int i = 0; i < acentos.Length; i++)
            {
                texto = texto.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };
            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                texto = texto.Replace(caracteresEspeciais[i], "");
            }
            /** Troca os espaços no início por "" **/
            texto = texto.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            texto = texto.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  " " **/
            texto = texto.Replace("\\s+", " ");
            return texto;
        }

        #region Tamanho Arquivo

        public static Decimal GetBytesParaMega(Decimal tamanho)
        {
            return (Decimal)(tamanho / (1024 * 1024));
        }

        #endregion

        #region Hash

        /// <summary>
        /// Calcula Hash do arquivo a partir dos bytes do arquivo
        /// </summary>
        /// <typeparam name="T">Tipo de criptografia utilizada</typeparam>
        /// <param name="stream">Arquivo a ser caculado o Hash</param>
        /// <returns>retorna o hash do arquivo calculado</returns>
        public static String GetHash<T>(Stream stream) where T : HashAlgorithm
        {
            StringBuilder sb = new StringBuilder();

            MethodInfo create = typeof(T).GetMethod("Create", new Type[] { });
            using (T crypt = (T)create.Invoke(null, null))
            {
                byte[] hashBytes = crypt.ComputeHash(stream);
                foreach (byte bt in hashBytes)
                {
                    sb.Append(bt.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calcula Hash do texto enviado
        /// </summary>
        /// <typeparam name="T">Tipo de criptografia utilizada</typeparam>
        /// <param name="input">Texto a ser caculado o Hash</param>
        /// <returns>retorna o hash do texto calculado</returns>
        public static String GetHashText<T>(String input) where T : HashAlgorithm
        {
            StringBuilder sb = new StringBuilder();

            MethodInfo create = typeof(T).GetMethod("Create", new Type[] { });
            using (T crypt = (T)create.Invoke(null, null))
            {
                byte[] hashBytes = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (byte bt in hashBytes)
                {
                    sb.Append(bt.ToString("x2"));
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// Calcula Hash do arquivo removendo assinatura
        /// </summary>
        /// <param name="arquivo">Arquivo a ser caculado o Hash</param>
        /// <param name="path">Caminho do arquivo a ser criado para gravação do arquivo temporário</param>
        /// <param name="nomeArquivo">Nome do arquivo temporario</param>
        /// <returns>retorna o hash do arquivo calculado</returns>
        public static String GetHashSemAssinatura(Stream arquivo, String path,String nomeArquivo)
        {
            #region Arquivo Liberado para importação

            String caminhoTempModificado = System.IO.Path.Combine(path, nomeArquivo);

            StreamWriter arquivoModificado = new StreamWriter(caminhoTempModificado, false, System.Text.Encoding.GetEncoding("ISO-8859-1"));

            arquivo.Position = 0;
            StreamReader arquivoLeitura = new StreamReader(arquivo, Encoding.GetEncoding("ISO-8859-1"));

            try
            {
                String linha = "";

                while (!arquivoLeitura.EndOfStream)
                {
                    linha = arquivoLeitura.ReadLine();

                    if (linha.Substring(0, 1) == "|")
                        arquivoModificado.WriteLine(linha);
                }
            }
            catch (Exception ex)
            {
                String s = ex.Message;
            }
            finally
            {
                arquivoModificado.Close(); 
            }

            String HashArquivo = "";

            using (FileStream fStream = File.OpenRead(caminhoTempModificado))
            {
                HashArquivo = Facilities.GetHash<SHA1>(fStream).ToUpper();
            }

            try
            {
                File.Delete(caminhoTempModificado);
            }
            catch (Exception)
            {
                 
            }

            return HashArquivo;

            #endregion
        }

        #endregion

        #region Converter

        public static DateTime? ConverterDataSPED(String valor)
        {
            try
            {
                return DateTime.Parse(valor.Substring(0, 2) + @"/" + valor.Substring(2, 2) + @"/" + valor.Substring(4));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Int32 ConverterInt32SPED(String valor)
        {
            try
            {
                return Int32.Parse(valor.Trim().Replace(".", ""));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Decimal ConverterDecimalSPED(String valor)
        {
            try
            {
                return Decimal.Parse(valor.Replace('.', ','));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region Texto
         
        public static String TextoIsNullOrEmpty(String texto)
        {
            if (String.IsNullOrEmpty(texto))
                return "";
            else
                return texto;
        }
         
        #endregion
    }
}

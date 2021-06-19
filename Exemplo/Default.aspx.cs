using Framework.SPED;
using Framework.SPED.Model.EFD_ICMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime inicio = DateTime.Now;

            String caminho = Server.MapPath("~");
             
            EFD_ICMS_IPI escrituracao = EFD_ICMS_IPI_BO.GetEscrituracao(flpArquivo.PostedFile.InputStream, caminho, true,true, true);

            //Adiciono o contribuinte como participante
            escrituracao.Participantes.Add(escrituracao.Entidade);

            grvParticpante.DataSource = escrituracao.Participantes;
            grvParticpante.DataBind();

            grvProduto.DataSource = escrituracao.Produtos;
            grvProduto.DataBind();

            TimeSpan ts = DateTime.Now - inicio;

            litMensagem.Text = "Foram encontrados " + escrituracao.Participantes.Count().ToString() + " participantes e " + escrituracao.Produtos.Count().ToString() + " produtos, dados gerados em " + ts.TotalSeconds.ToString() + " segundos.";

        }
        catch (Exception ex)
        {
            litMensagem.Text = ex.Message;
        }
    }
}
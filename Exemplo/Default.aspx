<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"  ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="litMensagem" runat="server"></asp:Literal>
        <hr />
        Arquivo:  <asp:FileUpload ID="flpArquivo" runat="server" /> 
        <asp:Button ID="btnImportar" runat="server" Text="Button" OnClick="btnImportar_Click" />
        <hr />
        <asp:GridView ID="grvParticpante" runat="server">
        </asp:GridView>
        <hr />
         <asp:GridView ID="grvProduto" runat="server">
        </asp:GridView>
    </form>
</body>
</html>

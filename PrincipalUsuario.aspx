<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrincipalUsuario.aspx.cs" Inherits="AgendaMedicamentos.PrincipalUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Cerrar Sesión" />
    <br />
    <br />
    Tus medicamentos son los siguientes:
    <asp:GridView runat="server" ID="MedicamentosGrid"></asp:GridView>
    <br />
    <br />

   <asp:Label runat="server" ID="Label1" Text=""></asp:Label>
    <asp:GridView ID="HistGrid" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="HistGridViewSelected" DataKeyNames="hist">
        <Columns>
            <asp:BoundField DataField="hist" Visible="false" /> 
            <asp:BoundField DataField="cTrat" Visible="false" /> 
            <asp:BoundField DataField="MedicamentoNombre" HeaderText="Medicamento Name" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label runat="server" ID="SelectedName" Text=""></asp:Label><asp:Button runat="server" ID="RegistrarTomada" Text="Registrar Tomada" OnClick="Registrar_Tomada"></asp:Button>
    <br />
    <br />
    Mensajes:

    <asp:CheckBoxList ID="chkMessages" runat="server" DataTextField="texto" DataValueField="cMen">
    </asp:CheckBoxList>
    <br />
    <asp:Button ID="btnMarkAsRead" runat="server" Text="Leido" OnClick="btnMarkAsRead_Click" />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Registrar nuevo medicamento" />
    <br />
    <hr />
    <br />
    <asp:GridView runat="server" ID="GridView1"></asp:GridView>
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Registrar nuevo contacto" />
    <br />


</form>
</body>
</html>

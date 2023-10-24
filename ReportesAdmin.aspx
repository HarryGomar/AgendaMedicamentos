<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportesAdmin.aspx.cs" Inherits="AgendaMedicamentos.ReportesAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Cerrar Sesión" />
            <br />
            <br />
            Reportes:
            <br />
            <br />
            Los contactos de los usuarios que no han leido 3 mensajes o mas:
            <asp:GridView ID="GridView4" runat="server" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
            </asp:GridView>
            <br />
            <br />



            Tratamientos que se han cumplido sin fallas:<br />
            <asp:GridView ID="GridView2" runat="server" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
            </asp:GridView>
            <br />
            <br />
            <br />
            Selecciona usuario para ver sus medicamentos:
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:GridView ID="GridView3" runat="server">
            </asp:GridView>
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="cargarGridReporteAntiFlex" Text="Generar Reporte" />
            <br />
            <br />
            <br />

            Reporte Flexible:
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            <br />
            Selecciona los datos que quieres incluir en el reporte:<br />
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
            </asp:CheckBoxList>
            <br />
            <br />
            Selecciona el medicamento del que quieres saber la información de los usuarios:<br />
            <br />
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
            <br />
            <br />

            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Generar Reporte Flexible" />
        </div>
        <div>&nbsp;</div>
        <div>&nbsp;</div>
        <div>&nbsp;</div>
    </form>
</body>
</html>

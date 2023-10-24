<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroMedicamento.aspx.cs" Inherits="AgendaMedicamentos.RegistroMedicamento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Cerrar Sesión" />
            <br />
            <br />
            En este apartado puedes registrar un nuevo medicamento:
            <br />
            <br />

            Nombre del medicamento:
            <asp:DropDownList ID="DropDownList2" runat="server"> </asp:DropDownList>

            <br />
            <br />

            No esta tu medicamento? Agregalo:
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Agrega Nombre" />



            <br />
            <br />
            Eliga la frecuencia del tratamiento:<br />

            <asp:DropDownList ID="DropDownList1" runat="server" >
                <asp:ListItem Value="168">Cada 7 días</asp:ListItem>
                <asp:ListItem Value="168">Cada 6 días</asp:ListItem>
                <asp:ListItem Value="168">Cada 5 días</asp:ListItem>
                <asp:ListItem Value="168">Cada 4 días</asp:ListItem>
                <asp:ListItem Value="168">Cada 3 días</asp:ListItem>
                <asp:ListItem Value="48">Cada 2 días</asp:ListItem>
                <asp:ListItem Value="24">Cada 24 horas</asp:ListItem>
                <asp:ListItem Value="8">Cada 12 horas</asp:ListItem>
                <asp:ListItem Value="8">Cada 10 horas</asp:ListItem>
                <asp:ListItem Value="8">Cada 8 horas</asp:ListItem>
                <asp:ListItem Value="8">Cada 7 horas</asp:ListItem>
                <asp:ListItem Value="6">Cada 6 horas</asp:ListItem>
                <asp:ListItem Value="8">Cada 5 horas</asp:ListItem>
                <asp:ListItem Value="4">Cada 4 horas</asp:ListItem>
                <asp:ListItem Value="8">Cada 3 horas</asp:ListItem>
                <asp:ListItem Value="2">Cada 2 horas</asp:ListItem>
            </asp:DropDownList>

            <br />
            <br />
            <br />
            Dosis del tratamiento (mg):
            <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
            <br />
            <br />
            <br />
            Introduzca la fecha de termino de su tratamiento: (MES/DÍA/AÑO)<br />
            <asp:TextBox ID="TextBox10" runat="server" ></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Registra Datos" />
            <br />
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Regresa a página anterior" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>

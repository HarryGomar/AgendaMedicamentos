<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrincipalAdmin.aspx.cs" Inherits="AgendaMedicamentos.PrincipalAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Cerrar Sesión" />
        <br />
        <br />
        <br />

        <asp:Button ID="Button7" runat="server" Text="Reportes" OnClick="Button7_Click" />



        <br />
        <br />
        Editar Catalogo de Medicamentos:
        Seleciona 
        <asp:DropDownList ID="DropDownList6" runat="server">
        </asp:DropDownList>

        Nuevo nombre del Medicamento:
        <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="updateMedicamentoNombre" Text="Cambiar Nombre Medicamento" />

        <br />
        <br />
        <br />

        Agregar al catalogo de Medicamentos:
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Agrega Nombre" />

        <br />
        <hr />
        <br />

        Agregar/quitar o modificar tratamientos de un usuario:<br />
        <br />
        Ingresa datos usuarios:<br />
        Correo usuario:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Validar datos" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <hr />
        
        <asp:Panel ID="pnlContent" runat="server">
            Seleciona medicamento:<br />
            <asp:DropDownList ID="DropDownList5" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;Borrar medicamento:<br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Elimina" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />

            &nbsp;&nbsp;&nbsp;Modificar medicamento:<br />
            Nuevos datos:<br />
            <br />

            Nombre nuevo de medicamento:
            
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>

            <br />
            Nueva frecuencia:
            <asp:DropDownList ID="DropDownList2" runat="server" >
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
            Nueva dosis de termino del medicamento:
            <asp:TextBox ID="TextBox4" runat="server" ></asp:TextBox>
            <br />
            <br />
            Nueva fecha de termino de tratamiento: (MES/DÍA/AÑO)<br />
            <asp:TextBox ID="TextBox7" runat="server" ></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Modifica" />
            <br />
            <hr />

            Nuevo Medicamento
            <br />
            <br />
            Nombre del medicamento:
            <asp:DropDownList ID="DropDownList3" runat="server"> </asp:DropDownList>
            <br />
            <br />
            <br />
            Eliga la frecuencia del tratamiento:<br />

            <asp:DropDownList ID="DropDownList4" runat="server" >
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
            <asp:TextBox ID="TextBox3" runat="server" ></asp:TextBox>
            <br />
            <br />
            <br />
            Introduzca la fecha de termino de su tratamiento: (MES/DÍA/AÑO)<br />
            <asp:TextBox ID="TextBox8" runat="server" ></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Agregar" />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server"></asp:Label>


        </asp:Panel>
    </div>

</form>
</body>
</html>

using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgendaMedicamentos
{
    public partial class RegistroContacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idU"] == null || Session["nombreU"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }

            if (DropDownList1.Items.Count == 0)
            {
                String query = "select cTipo,nombre from Tipo";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);
                try
                {
                    OdbcDataReader lector = comando.ExecuteReader();

                    DropDownList1.DataSource = lector;
                    DropDownList1.DataTextField = "nombre";
                    DropDownList1.DataValueField = "cTipo";
                    DropDownList1.DataBind();
                }
                catch (Exception ex)
                {

                }
                conexion.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox5.Text != "" && TextBox6.Text != "" && TextBox7.Text != "" && TextBox8.Text != "")
            {
                String query = "insert into contacto values((select isnull(max(idC),0)+1 from contacto), ?,?,?,?,?,?)";
                String s5, s6, s7, s8;
                s5 = TextBox5.Text;//Nombre Contacto
                s6 = TextBox6.Text;//Correo contacto
                s7 = TextBox7.Text;//Teléfono Contacto
                s8 = TextBox8.Text;//Domicilio Contacto
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);

                comando.Parameters.AddWithValue("cTipo", DropDownList1.SelectedValue.ToString());
                comando.Parameters.AddWithValue("nombreC", s5);
                comando.Parameters.AddWithValue("correoC", s6);
                comando.Parameters.AddWithValue("telefonoC", s7);
                comando.Parameters.AddWithValue("domicilioC", s8);
                comando.Parameters.AddWithValue("idU", Session["idU"]);
                

                comando.ExecuteNonQuery();
                Label1.Text = "En nombre del contacto es: " + s5 + " el correo del contacto es: " + s6 + " el teléfono del contacto es: " + s7 +
                    " el domicilio del contacto es: " + s8;

                TextBox5.Text = "";//Nombre Contacto
                TextBox6.Text = "";//Correo contacto
                TextBox7.Text = "";//Teléfono Contacto
                TextBox8.Text = "";//Domicilio Contacto
                conexion.Close();
            }
            else
            {
                Label1.Text = "Hay elemento(s) incompletos";
            }

        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrincipalUsuario.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Inicio.aspx");
        }
    }
}
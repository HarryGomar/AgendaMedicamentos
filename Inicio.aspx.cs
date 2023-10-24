using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgendaMedicamentos
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String query = "select idU, nombre from Usuario where correo= ? and passwrd=?";
            int idU;
            String nombreU;
            OdbcConnection conexion = new ConexionBD().con;

            OdbcCommand comando = new OdbcCommand(query, conexion);

            comando.Parameters.AddWithValue("correo", TextBox1.Text);
            comando.Parameters.AddWithValue("passwrd", TextBox2.Text);
            OdbcDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                lector.Read();
                idU = lector.GetInt32(0);
                nombreU = lector.GetString(1);
                Session.Timeout = 10;
                Session.Add("idU", idU);
                Session.Add("nombreU", nombreU);

                Response.Redirect("PrincipalUsuario.aspx");
            }
            else
            {
                Label1.Text = "Datos inválidos introducidos";
            }
            lector.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroUsuario.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAdmin.aspx");
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
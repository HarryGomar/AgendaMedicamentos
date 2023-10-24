using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgendaMedicamentos
{
    public partial class LoginAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String query = "select idA, nombre from Administrador where correo=? and passwrd=?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("correoAdmin", TextBox1.Text);
            comando.Parameters.AddWithValue("passwrd", TextBox2.Text);

            OdbcDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                lector.Read();
                int idAdmin = lector.GetInt32(0);
                String nombreAdmin = lector.GetString(1);
                Session.Add("idAdmin", idAdmin);
                Session.Add("nombreAdmin", nombreAdmin);
            }
            lector.Close();
            TextBox1.Text = "";
            TextBox2.Text = "";

            if (Session["idAdmin"] != null && Session["nombreAdmin"] != null)
            {
                Response.Redirect("PrincipalAdmin.aspx");

            }
            else
            {
                Label1.Text = "Datos no válidos";
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");

        }
    }
}
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
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 10;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Verificar que los datos de introducción sean válidos y regresar a la página principal
            String s1, s2, s3, s4, s5;
            s1 = TextBox1.Text; //Nombre
            s2 = TextBox2.Text;//Correo
            s3 = TextBox5.Text;//Contraseña
            s4 = TextBox3.Text;//Telefono
            s5 = TextBox4.Text;//Direccion
            if (s1 != "" && s2 != "" && s3 != "" && s4 != "" && s5 != "")
            {
                String query = "insert into Usuario values((select isnull (max(idU),0)+1 from Usuario),?,?,?,?,?)";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);

                comando.Parameters.AddWithValue("nombreU", s1);
                comando.Parameters.AddWithValue("correoU", s2);
                comando.Parameters.AddWithValue("contraseñaU", s3);
                comando.Parameters.AddWithValue("telefonoU", s4);
                comando.Parameters.AddWithValue("domicilioU", s5);
                comando.ExecuteNonQuery();

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";


                Label1.Text = "Registro exitoso";


                //Response.Redirect("Inicio.aspx");
            }
            else
            {
                Label1.Text = "Verificar los datos introducidos";
            }

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");

        }
    }
}
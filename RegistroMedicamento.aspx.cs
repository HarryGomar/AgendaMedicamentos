using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgendaMedicamentos
{
    public partial class RegistroMedicamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idU"] == null || Session["nombreU"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }

            cargarMedicamentos();




        }

        protected void cargarMedicamentos()
        {
            if (DropDownList2.Items.Count == 0)
            {
                String query = "select cMed,nombre from medicamento order by nombre asc";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);
                try
                {
                    OdbcDataReader lector = comando.ExecuteReader();

                    DropDownList2.DataSource = lector;
                    DropDownList2.DataTextField = "nombre";
                    DropDownList2.DataValueField = "cMed";
                    DropDownList2.DataBind();
                }
                catch (Exception ex)
                {

                }
                conexion.Close();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String fechafin, dosis;
            int freq, cMed;

        

            if (!int.TryParse(DropDownList2.SelectedValue, out cMed))
            {
                cMed = -1;
            }


            if (!int.TryParse(DropDownList1.SelectedValue, out freq))
            {
                freq = -1;
            }

            fechafin = TextBox10.Text;//Fecha de fin
            dosis = TextBox1.Text;

            if (cMed != -1 && fechafin != "" && freq != -1)
            {

                String query = "INSERT INTO tratamiento VALUES ((SELECT ISNULL(MAX(cTrat), 0) + 1 FROM tratamiento), ?, ?, ?, CURRENT_TIMESTAMP,?, ?)";

                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);

                comando.Parameters.AddWithValue("nombreM", cMed);
                comando.Parameters.AddWithValue("frecuenciaM", freq);
                comando.Parameters.AddWithValue("dosis", dosis);
                comando.Parameters.AddWithValue("fechaFin", fechafin);
                comando.Parameters.AddWithValue("idU", Session["idU"]);
                
                comando.ExecuteNonQuery();
                

                Label1.Text = "El nombre del medicamento que introdujiste es: " + DropDownList2.SelectedItem.Text + " y lo tomarás con la frecuencia de: " + DropDownList1.SelectedItem.Text + " hasta la fecha de: " + fechafin;

                TextBox1.Text = "";
                TextBox10.Text = "";


                String queryHist = "insert into historial values ((SELECT ISNULL(MAX(cHist), 0) + 1 FROM historial), (SELECT MAX(cTrat) from tratamiento), 0, CURRENT_TIMESTAMP)";
                comando = new OdbcCommand(queryHist, conexion);
                comando.ExecuteNonQuery();


            }
            else
            {
                Label1.Text = "Hacen falta datos para introducir medicamento";
            }


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrincipalUsuario.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String query = "INSERT INTO medicamento VALUES ((SELECT ISNULL(MAX(cMed), 0) + 1 FROM medicamento), ?)";

            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);

            String nombre = TextBox2.Text;

            comando.Parameters.AddWithValue("nombre", nombre);

            try
            {
                comando.ExecuteNonQuery();
                TextBox2.Text = "";
            }
            catch(Exception ex)
            {
                TextBox2.Text = ex.Message;
            }

            DropDownList2.Items.Clear();
            cargarMedicamentos();


        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Inicio.aspx");
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgendaMedicamentos
{
    public partial class PrincipalAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idAdmin"] == null || Session["nombreAdmin"] == null)
            {
                Response.Redirect("LoginAdmin.apsx");
            }
            Session.Timeout = 30;

            if (Session["idUSelected"] == null)
            {
                pnlContent.Visible = false;
            }

            if(DropDownList3.Items.Count == 0)
            {
                cargarMedicamentos();
            }

        }


        protected void cargarMedicamentos()
        {
            String query = "select nombre, cMed from medicamento order by nombre asc";

            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);

            OdbcDataReader lector = comando.ExecuteReader();

            DropDownList3.DataSource = lector;
            DropDownList3.DataValueField = "cMed";
            DropDownList3.DataTextField = "nombre";
            DropDownList3.DataBind();


            lector.Close();
            lector = comando.ExecuteReader();

            DropDownList1.DataSource = lector;
            DropDownList1.DataValueField = "cMed";
            DropDownList1.DataTextField = "nombre";
            DropDownList1.DataBind();


            lector.Close();
            lector = comando.ExecuteReader();

            DropDownList6.DataSource = lector;
            DropDownList6.DataValueField = "cMed";
            DropDownList6.DataTextField = "nombre";
            DropDownList6.DataBind();
            lector.Close();

        }

        protected void cargarMeds()
        {
            String query = "select m.nombre, t.ctrat, m.cMed from tratamiento t inner join medicamento m on t.cMed = m.cMed where t.idU=? order by nombre asc";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("idU", Session["idUSelected"]);

            try
            {
                OdbcDataReader lector = comando.ExecuteReader();

                DropDownList5.DataSource = lector;
                DropDownList5.DataValueField = "cTrat";
                DropDownList5.DataTextField = "nombre";
                DropDownList5.DataBind();

                lector.Close();
                
            }

            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
            conexion.Close();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            String fechafin, dosis;
            int freq, cMed;

            if (!int.TryParse(DropDownList3.SelectedValue, out cMed))
            {
                cMed = -1;
            }


            if (!int.TryParse(DropDownList4.SelectedValue, out freq))
            {
                freq = -1;
            }

            fechafin = TextBox8.Text;//Fecha de fin
            dosis = TextBox3.Text;

            if (cMed != -1 && fechafin != "" && freq != -1)
            {

                String query = "INSERT INTO tratamiento VALUES ((SELECT ISNULL(MAX(cTrat), 0) + 1 FROM tratamiento), ?, ?, ?, CURRENT_TIMESTAMP,?, ?)";

                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);

                comando.Parameters.AddWithValue("cMed", cMed);
                comando.Parameters.AddWithValue("frecuencia", freq);
                comando.Parameters.AddWithValue("dosis", dosis);
                comando.Parameters.AddWithValue("fechaFin", fechafin);
                comando.Parameters.AddWithValue("idUSelected", Session["idUSelected"]);

                comando.ExecuteNonQuery();


                Label3.Text = "El nombre del medicamento que introdujiste es: " + DropDownList3.SelectedItem.Text + " y lo tomarás con la frecuencia de: " + DropDownList4.SelectedItem.Text + " hasta la fecha de: " + fechafin;

                TextBox1.Text = "";
                TextBox8.Text = "";


                String queryHist = "insert into historial values ((SELECT ISNULL(MAX(cHist), 0) + 1 FROM historial), (SELECT MAX(cTrat) from tratamiento), 0, CURRENT_TIMESTAMP)";
                comando = new OdbcCommand(queryHist, conexion);
                comando.ExecuteNonQuery();
                cargarMeds();

            }
            else
            {
                Label1.Text = "Hacen falta datos para introducir medicamento";
            }

        }

    

        protected void Button1_Click(object sender, EventArgs e)
        {
            int cTrat;
            if (!int.TryParse(DropDownList5.SelectedValue, out cTrat))
            {
                cTrat = -1;
            }

            try
            {
                String query = "delete from tratamiento where cTrat=?";
                string query2 = "DELETE FROM historial WHERE cTrat = ?";

                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query2, conexion);

                comando.Parameters.AddWithValue("cTrat", cTrat);

                comando.ExecuteNonQuery();

                comando = new OdbcCommand(query, conexion);

                comando.Parameters.AddWithValue("cTrat", cTrat);
                
                comando.ExecuteNonQuery();
                conexion.Close();

                Label2.Text = "Eliminación realizada";

                cargarMeds();
            }
            catch (Exception ex)
            {
                Label2.Text = "Eliminación no realizada" + ex.Message;
            }

        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            int freq;

            if (!int.TryParse(DropDownList4.SelectedValue, out freq))
            {
                freq = -1;
            }

            if (TextBox7.Text != "" && TextBox4.Text != "")
            {
                String query = "update tratamiento set cMed=?, frecuencia=?, dosis=?, fechaFin=? where cTrat=? and idU=?";
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando = new OdbcCommand(query, conexion);

                comando.Parameters.AddWithValue("cMedNuevo", DropDownList1.SelectedValue);
                comando.Parameters.AddWithValue("frecuenciaMNueva", freq);
                comando.Parameters.AddWithValue("dosisNeva", TextBox4.Text);
                comando.Parameters.AddWithValue("fechaFinNueva", TextBox7.Text);
                comando.Parameters.AddWithValue("cTratAnt", DropDownList5.SelectedValue);
                comando.Parameters.AddWithValue("idU", Session["idUSelected"]);
                comando.ExecuteNonQuery();
                TextBox4.Text = "";
                TextBox4.Text = "";
                TextBox7.Text = "";

                Label4.Text = "Actualización realizada";

                cargarMeds();
            }
            else
            {
                Label4.Text = "Datos innválidos";
            }

        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            String query = "INSERT INTO medicamento VALUES ((SELECT ISNULL(MAX(cMed), 0) + 1 FROM medicamento), ?) ";

            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);

            String nombre = TextBox5.Text;

            comando.Parameters.AddWithValue("nombre", nombre);

            try
            {
                comando.ExecuteNonQuery();
                TextBox5.Text = "";
            }
            catch (Exception ex)
            {
                TextBox2.Text = ex.Message;
            }



            DropDownList2.Items.Clear();
            cargarMedicamentos();


        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            String query = "select idU, nombre from Usuario where correo=?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            comando.Parameters.AddWithValue("correoU", TextBox1.Text);

            OdbcDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                Session.Add("idUSelected", lector.GetInt32(0));
                String nombreU = lector.GetString(1);

                Label1.Text = "Usuario: " + nombreU;
                pnlContent.Visible = true;
                cargarMeds();

            }
            else
            {
                Label1.Text = "Datos inválidos";

            }

        }

        protected void updateMedicamentoNombre(object sender, EventArgs e)
        {
            String query = "update medicamento set nombre=? where cMed=?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);

            comando.Parameters.AddWithValue("nombre", TextBox2.Text);
            comando.Parameters.AddWithValue("cMed", DropDownList6.SelectedValue);

            comando.ExecuteNonQuery();
            TextBox2.Text = "";

            cargarMedicamentos();
        }



        protected void Button6_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Inicio.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportesAdmin.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgendaMedicamentos
{
    public partial class ReportesAdmin : System.Web.UI.Page
    {
        public String select = "select ";
        public String from = " from usuario inner join tratamiento on Usuario.idU = Tratamiento.idU inner join medicamento on Tratamiento.cMed = medicamento.cMed ";
        public String where = " where 1=1";
        public String query = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idAdmin"] == null || Session["nombreAdmin"] == null)
            {
                Response.Redirect("LoginAdmin.apsx");
            }
           

            if (!IsPostBack) // Make sure this is only executed on the initial page load
            {
                OdbcConnection conexion = new ConexionBD().con;
                OdbcCommand comando;
                String query;

                OdbcDataReader lector;
                // Populate DropDownList2
                if (DropDownList2.Items.Count == 0)
                {
                    query = "Select cMed, nombre from medicamento order by nombre asc";
                    comando = new OdbcCommand(query, conexion);
                    lector = comando.ExecuteReader();

                    DropDownList2.DataSource = lector;
                    DropDownList2.DataValueField = "cMed";
                    DropDownList2.DataTextField = "nombre";
                    DropDownList2.DataBind();

                    lector.Close();

                    DropDownList2.Items.Insert(0, new ListItem("(sin selección)", "-1"));
                }

                if (DropDownList1.Items.Count == 0)
                {
                    query = "select idU, nombre from usuario";

                    comando = new OdbcCommand(query, conexion);

                    lector = comando.ExecuteReader();

                    DropDownList1.DataSource = lector;
                    DropDownList1.DataValueField = "idU";
                    DropDownList1.DataTextField = "nombre";
                    DropDownList1.DataBind();
                }

                // Populate CheckBoxList1
                if (CheckBoxList1.Items.Count == 0)
                {
                    CheckBoxList1.Items.Add(new ListItem("Nombre Usuario", " usuario.nombre as nombreU"));
                    CheckBoxList1.Items.Add(new ListItem("Correo del usuario", " usuario.correo as CorreoU"));
                    CheckBoxList1.Items.Add(new ListItem("Telefono del usuario", " usuario.tel as TelefonoU"));
                    CheckBoxList1.Items.Add(new ListItem("Domicilio del usuario", " usuario.dom as DomicilioU")); 
                    CheckBoxList1.Items.Add(new ListItem("Nombre Medicamento", " medicamento.nombre as Medicamento"));
                    CheckBoxList1.Items.Add(new ListItem("Dosis", " tratamiento.Dosis as Dosis"));
                    CheckBoxList1.Items.Add(new ListItem("Fecha Inicio", "tratamiento.fechaIn as FechaIn"));
                    CheckBoxList1.Items.Add(new ListItem("Fecha Fin", " tratamiento.fechaFin as FechaFin"));

                }
            }

            CargaGridView();
            CargaGridView2();
        }



        protected void cargarGridReporteAntiFlex(object sender, EventArgs e)
        {
            query = "select m.nombre as NombreMedicamento, u.nombre AS NombreU, u.correo AS CorreoU FROM usuario u INNER JOIN tratamiento t ON u.idU = t.idU INNER JOIN medicamento m ON t.cMed = m.cMed WHERE t.idu = ?";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);

            comando.Parameters.AddWithValue("idU", DropDownList1.SelectedValue);
            try
            {
                OdbcDataReader lector = comando.ExecuteReader();

                GridView3.DataSource = lector;
                GridView3.DataBind();
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            finally
            {
                conexion.Close();
            }


        }

        public void CargaGridView()
        {
            String query = "SELECT m.nombre AS NombreMedicamento, u.nombre AS NombreU, u.correo AS CorreoU FROM usuario u INNER JOIN tratamiento t ON u.idU = t.idU INNER JOIN medicamento m ON t.cMed = m.cMed WHERE t.ctrat NOT IN (SELECT t.cTrat FROM tratamiento t INNER JOIN historial h ON h.cTrat = t.cTrat WHERE h.tomo = '0' group by t.cTrat)\r\n";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView2.DataSource = lector;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            finally
            {
                conexion.Close();
            }
        }

        public void CargaGridView2()
        {
            String query = "SELECT c.nombre AS nombreContacto, c.correo AS correoContacto, u.nombre AS usuarioNombre, u.correo as usuarioCorreo FROM contacto c inner join usuario u on c.idU = u.idU INNER JOIN (SELECT idU FROM mensaje WHERE leido = 0 GROUP BY idU HAVING COUNT(cMen) > 2) AS UnreadMsg ON u.idU = UnreadMsg.idU";
            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView4.DataSource = lector;
                GridView4.DataBind();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            finally
            {
                conexion.Close();
            }
        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("LoginAdmin.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Reset the select and where clauses
            select = "select ";
            where = " where 1=1";

            // Build the select clause based on CheckBoxList1    // STUDY THIS 
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    select += CheckBoxList1.Items[i].Value.ToString() + ",";
                }
            }
            select = select.TrimEnd(',') + ' ';

            // Build the where clause based on DropDownList2
            if (DropDownList2.SelectedValue != "-1")
            {
                where += " and medicamento.cMed=?";
            }

            // Build the complete query
            query = select + from + where;

            OdbcConnection conexion = new ConexionBD().con;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            if (DropDownList2.SelectedValue != "-1")
            {
                comando.Parameters.AddWithValue("cMed", DropDownList2.SelectedValue);
            }

            try
            {
                OdbcDataReader lector = comando.ExecuteReader();
                GridView1.DataSource = lector;
                GridView1.DataBind();
               

            }

            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }

            finally
            {
                conexion.Close();
            }

            CargaGridView();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle grid view selection if needed
        }
    }
}
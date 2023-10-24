using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgendaMedicamentos
{
    public partial class PrincipalUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idU"] == null || Session["nombreU"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }
            Session.Timeout = 30;

            checarHistorial();
            cargarGridView();
            cargarHistGridView();
            cargarMensajes();
            cargarContactosGrid();

            RegistrarTomada.Visible = false;
        }


        protected void cargarGridView()
        {
            string query = "select m.nombre, t.frecuencia as 'Frecuencia (Cada cuantas [horas])', t.fechaFin as 'Tomar hasta:' from tratamiento t inner join medicamento m on t.cMed = m.cMed where t.idU=?";

            OdbcConnection con = new ConexionBD().con;

            OdbcCommand cmd = new OdbcCommand(query, con);

            cmd.Parameters.AddWithValue("idU", Session["idU"]);

            OdbcDataReader lector = cmd.ExecuteReader();

            MedicamentosGrid.DataSource = lector;

            MedicamentosGrid.DataBind();

            

        }

        protected void cargarContactosGrid()
        {
            string query = "select nombre, correo from contacto where idu=?";

            OdbcConnection con = new ConexionBD().con;

            OdbcCommand cmd = new OdbcCommand(query, con);

            cmd.Parameters.AddWithValue("idU", Session["idU"]);

            OdbcDataReader lector = cmd.ExecuteReader();

            GridView1.DataSource = lector;

            GridView1.DataBind();

            
        }

        protected void cargarHistGridView()
        {
            string query = "WITH MaxcHist AS (SELECT t.idU, t.cTrat, MAX(h.cHist) AS MaxcHist FROM tratamiento t JOIN historial h ON t.cTrat = h.cTrat GROUP BY t.idU, t.cTrat) SELECT m.nombre AS MedicamentoNombre, mh.cTrat, mh.MaxcHist as hist FROM MaxcHist mh JOIN tratamiento t ON mh.cTrat = t.cTrat AND mh.idU = t.idU JOIN medicamento m ON t.cMed = m.cMed JOIN historial h ON mh.cTrat = h.cTrat AND mh.MaxcHist = h.cHist WHERE t.idU = ? AND h.tomo = 0 and t.fechaFin > CURRENT_TIMESTAMP";

            OdbcConnection con = new ConexionBD().con;

            OdbcCommand cmd = new OdbcCommand(query, con);

            cmd.Parameters.AddWithValue("idU", Session["idU"]);

            OdbcDataReader lector = cmd.ExecuteReader();

            HistGrid.DataSource = lector;

            HistGrid.AutoGenerateSelectButton = true;

            HistGrid.AutoGenerateColumns = false;

            HistGrid.DataBind();


            if (HistGrid.Rows.Count == 0)
            {
                Label1.Text = "Estas al corriente de tus tratamientos! ";
            }
            else
            {
                Label1.Text = "Se Debe Tomar:";
            }
            if (MedicamentosGrid.Rows.Count == 0)
            {
                Label1.Text = "Aun no tienes ningun tratamiento";
            }
        }

        protected void cargarMensajes()
        {
            if (chkMessages.Items.Count == 0)
            {

                OdbcConnection con = new ConexionBD().con;

                OdbcCommand cmd = new OdbcCommand("SELECT texto, cMen FROM mensaje WHERE idU = ? AND leido = 0", con);
                cmd.Parameters.AddWithValue("idU", Session["idU"]);

                OdbcDataReader lector = cmd.ExecuteReader();
                chkMessages.DataSource = lector;
                chkMessages.DataTextField = "texto";
                chkMessages.DataValueField = "cMen";
                chkMessages.DataBind();
                con.Close();


            }

            if (chkMessages.Items.Count == 0)
            {
                btnMarkAsRead.Visible = false;
            }

            else
            {
                btnMarkAsRead.Visible = true;
            }


        }


        protected void btnMarkAsRead_Click(object sender, EventArgs e)
        {

            String query = "UPDATE mensaje SET leido = 1 WHERE cMen = ?";
            OdbcConnection con = new ConexionBD().con;
            OdbcCommand cmd = new OdbcCommand(query, con);

            foreach (ListItem item in chkMessages.Items)
            {
                if (item.Selected)
                {
                    cmd = new OdbcCommand(query, con);
                    cmd.Parameters.AddWithValue("cMen", item.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            chkMessages.Items.Clear();
            con.Close();

            cargarMensajes();
        }




        protected void Mandar_Mensaje(string nombreMed)
        {
            //Subir "fallo tomar la medicina a la hora X el Y 

            String query = "insert into mensaje values ((SELECT ISNULL(MAX(cMen), 0) + 1 FROM mensaje), ?, 0, ?)";

            string mensaje = "Te falto tomar la pastilla " + nombreMed;


            OdbcConnection con = new ConexionBD().con;
            OdbcCommand cmd = new OdbcCommand(query, con);

            cmd.Parameters.AddWithValue("texto", mensaje);

            cmd.Parameters.AddWithValue("idU", Session["idU"]);

            cmd.ExecuteNonQuery();

            cargarMensajes();
        }

        protected void HistGridViewSelected(object sender, EventArgs e)
        {

            Session.Add("cHistSel", HistGrid.DataKeys[HistGrid.SelectedIndex].Value.ToString());
            SelectedName.Text = HistGrid.Rows[HistGrid.SelectedIndex].Cells[2].Text;

            RegistrarTomada.Visible = true;

        }

        protected void Registrar_Tomada(object sender, EventArgs e)
        {
            int cHistSelected = int.Parse(Session["cHistSel"].ToString());

            updateHistorial(cHistSelected);
            SelectedName.Text = "";
        }

        protected void updateHistorial(int cHistSelected)
        {
            string update = "update historial set fecha = CURRENT_TIMESTAMP, tomo = 1 where cHist = ?";

            OdbcConnection con = new ConexionBD().con;
            OdbcCommand cmd = new OdbcCommand(update, con);

            cmd.Parameters.AddWithValue("cHist", cHistSelected);

          
            cmd.ExecuteNonQuery();

            checarHistorial();
            cargarHistGridView();
        }

        protected void Update_Historial(int cTrat)
        {
            string query = "insert into historial values ((SELECT ISNULL(MAX(cHist), 0) + 1 FROM historial),?, 0, CURRENT_TIMESTAMP)";


            OdbcConnection con = new ConexionBD().con;
            OdbcCommand cmd = new OdbcCommand(query, con);

            cmd.Parameters.AddWithValue("cTrat", cTrat);

            cmd.ExecuteNonQuery();

            checarHistorial();
            cargarHistGridView();

        }

        protected void checarHistorial()
        {
            string query = "SELECT Historial.cTrat, medicamento.nombre FROM historial  INNER JOIN (SELECT cTrat, MAX(cHist) AS MaxChist FROM historial GROUP BY cTrat ) SubQ ON historial.cTrat = SubQ.cTrat AND historial.cHist = SubQ.MaxChist INNER JOIN tratamiento ON historial.cTrat = tratamiento.cTrat AND tratamiento.idU = ? INNER JOIN medicamento ON tratamiento.cMed = medicamento.cMed WHERE DATEDIFF(HOUR, historial.fecha, CURRENT_TIMESTAMP) > tratamiento.frecuencia and tratamiento.fechaFin > CURRENT_TIMESTAMP";

            OdbcConnection con = new ConexionBD().con;

            OdbcCommand cmd = new OdbcCommand(query, con);

            cmd.Parameters.AddWithValue("idU", Session["idU"]);

            OdbcDataReader lector = cmd.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {

                    int cTrat;
                    if (int.TryParse(lector[0].ToString(), out cTrat))
                    {
                       
                        Update_Historial(cTrat);
                        string nombreMed = lector[1].ToString();
                        Mandar_Mensaje(nombreMed);
                    }
                }
            }
        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroMedicamento.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroContacto.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Inicio.aspx");
        }
    }
}
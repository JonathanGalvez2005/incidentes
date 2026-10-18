using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Incidentes
{
    public partial class Form1 : Form
    {
        private string cadena = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jonat\OneDrive\Escritorio\repos\Incidentes\Incidentes\Incidentes.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvIncidentes.AutoGenerateColumns = false;
            dgvIncidentes.Columns[0].DataPropertyName = "id";
            dgvIncidentes.Columns[1].DataPropertyName = "fecha";
            dgvIncidentes.Columns[2].DataPropertyName = "tipo";
            dgvIncidentes.Columns[3].DataPropertyName = "lugar";
            dgvIncidentes.Columns[4].DataPropertyName = "descripcion";
            dgvIncidentes.Columns[5].DataPropertyName = "estado";

            CargarDatos();
        }

        private void CargarDatos()
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT id, fecha, tipo, lugar, descripcion, estado FROM [Table]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvIncidentes.DataSource = dt;
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFecha.Text) ||
                string.IsNullOrWhiteSpace(txtTipo.Text) ||
                string.IsNullOrWhiteSpace(txtLugar.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtEstado.Text))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            // Validar la fecha
            DateTime fecha;
            if (!DateTime.TryParse(txtFecha.Text, out fecha))
            {
                MessageBox.Show("La fecha no es válida. Usa el formato dd/mm/aaaa.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cadena))
                {
                    string sql = "INSERT INTO [Table] (fecha, tipo, lugar, descripcion, estado) " +
                                 "VALUES (@fecha, @tipo, @lugar, @descripcion, @estado)";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@tipo", txtTipo.Text.Trim());
                    cmd.Parameters.AddWithValue("@lugar", txtLugar.Text.Trim());
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                    cmd.Parameters.AddWithValue("@estado", txtEstado.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                CargarDatos();   // refresca el DataGridView
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        // Limpia los TextBox después de guardar
        private void LimpiarCampos()
        {
            txtFecha.Clear();
            txtTipo.Clear();
            txtLugar.Clear();
            txtDescripcion.Clear();
            txtEstado.Clear();
            txtFecha.Focus();
        }
    }
}

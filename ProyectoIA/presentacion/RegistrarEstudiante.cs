using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoIA.bdEntity;
using ProyectoIA.negocio;
using ProyectoIA.util;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ProyectoIA.presentacion
{
    public partial class RegistrarEstudiante : UserControl
    {
        string fileName;
        List<Estudiante> listaEstudiantes;
        List<Foto> listaFoto;
        //FaceServiceClient faceServiceClient = new FaceServiceClient("3cb39c7bfec14d77927097de75d07028", "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");
        FaceApi faceApi = new FaceApi();

        public RegistrarEstudiante()
        {
            InitializeComponent();
            cargarGrillaEstudiantes();
        }
        
        private void cargarGrillaEstudiantes()
        {
            listViewEstudiante.Items.Clear();
            using (IAContext db = new IAContext())
            {
                listaEstudiantes = db.estudiantes.ToList();
                foreach (Estudiante estudiante in listaEstudiantes)
                {
                    string[] row = { estudiante.registro, estudiante.nombre, estudiante.apellido };
                    ListViewItem item = new ListViewItem(row);
                    listViewEstudiante.Items.Add(item);
                }

                listaFoto = db.fotos.ToList();
            }
        }
        private void buttonCargar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "JPEG|*.jpg",
                ValidateNames = true,
                Multiselect = true
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    labelNombreArchivo.Text = fileName;
                    pictureBoxImagen.Image = Image.FromFile(fileName);
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            using(IAContext db = new IAContext())
            {
                Estudiante estudiante = listaEstudiantes[listViewEstudiante.FocusedItem.Index];
                db.estudiantes.Attach(estudiante);
                Foto foto = new Foto(0, ConversorImagenesBit.imageToByteArray(pictureBoxImagen.Image), estudiante);
                db.fotos.Add(foto);
                await db.SaveChangesAsync();
                MessageBox.Show("imagen guardada");
                cargarGrillaFoto(estudiante);

                faceApi.agregarFotoEstudiante(estudiante.id.ToString(), pictureBoxImagen.Image);
            }
        }

        private async void buttonGuardar_Click(object sender, EventArgs e)
        {
            using(IAContext db = new IAContext())
            {
                Estudiante estudiante = new Estudiante(0, textBoxRegistro.Text, textBoxNombre.Text, textBoxApellido.Text);
                db.estudiantes.Add(estudiante);
                db.SaveChanges();
                MessageBox.Show("estudiante registrado");
                cargarGrillaEstudiantes();

                faceApi.crearEstudiante(estudiante.id.ToString());
                
            }
        }

        private void buttonCargarGrilla_Click(object sender, EventArgs e)
        {

        }

        private void listViewEstudiante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEstudiante.FocusedItem != null)
            {
                Estudiante estudiante = listaEstudiantes[listViewEstudiante.FocusedItem.Index];
                textBoxNombre.Text = estudiante.nombre;
                textBoxRegistro.Text = estudiante.registro;
                textBoxApellido.Text = estudiante.apellido;

                listViewFoto.Items.Clear();

                cargarGrillaFoto(estudiante);
            }

        }
        private void cargarGrillaFoto(Estudiante estudiante)
        {
            listViewFoto.Items.Clear();
            if (estudiante.fotos != null)
            {
                foreach (Foto foto in estudiante.fotos)
                {
                    if (estudiante.id == foto.estudiante.id)
                    {
                    
                        string[] row = { foto.estudiante.nombre, foto.id.ToString() };
                        ListViewItem item = new ListViewItem(row);
                        listViewFoto.Items.Add(item);
                    }
                }
            }
        }

        private void listViewFoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Estudiante estudiante = listaEstudiantes[listViewEstudiante.FocusedItem.Index];
            if (listViewFoto.FocusedItem != null)
            {
                Foto foto = estudiante.fotos.ElementAt(listViewFoto.FocusedItem.Index);
                pictureBoxImagen.Image = ConversorImagenesBit.byteArrayToImage(foto.contenido);
                labelNombreArchivo.Text = foto.id.ToString();
            }
        }


        async private void button1_Click(object sender, EventArgs e)
        {

        }


        private async void button1_Click_1(object sender, EventArgs e)
        {
            faceApi.entrenar();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string resultado = await faceApi.detectar(pictureBoxImagen.Image);
            if (resultado != null)
            {
                Estudiante estudiante = listaEstudiantes.SingleOrDefault(es => es.id == int.Parse(resultado));
                MessageBox.Show(estudiante.registro + " " + estudiante.nombre + " " + estudiante.apellido);
            }
            else
            {
                MessageBox.Show("no reconocido");
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concurrencias._3
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private HttpClient httpClient;
        public Form1()
        {
            InitializeComponent();
            apiURL = "https://localhost:7284";
            httpClient = new HttpClient();
        }

        private async void BtnCharge_Click(object sender, EventArgs e)
        {
            LoadingGIF.Visible = true;
            await esperar();
            var nombre = NameTxt.Text;
            try
            {
                var saludo = await ObtenerSaludo(nombre);
                MessageBox.Show(saludo);
            }
            catch(HttpRequestException ex)
            { 
                MessageBox.Show(ex.Message);
            }
            

            LoadingGIF.Visible = false;
        }

        private async Task esperar()
        {
            await Task.Delay(5000);
        }
        private async Task<String> ObtenerSaludo(string nombre)
        {
            using (var respuesta = await httpClient.GetAsync($"{apiURL}/saludos/{nombre}"))
            {
                var saludo = await respuesta.Content.ReadAsStringAsync();
                return saludo;
            }
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

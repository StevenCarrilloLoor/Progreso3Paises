using Progreso3Paises.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Progreso3Paises.Repositories
{
    internal class CountryRepo
    {
        public string _dbPath;
        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Country>();
        }

        //Constructor
        public CountryRepo(string dbPath)
        {
            _dbPath = dbPath;
            Init();
        }

        public List<Country> RetornaListadoPaises()
        {
            return conn.Table<Country>().ToList();
        }

        // Método para guardar el país en la base de datos SQLite
        public void GuardarPais(Country country)
        {
            conn.Insert(country);
        }

        // Método para actualizar el estado de un país
        public void ActualizarStatus(Country country)
        {
            conn.Update(country);
        }

        // Método para eliminar un país de la base de datos
        public void EliminarPais(Country country)
        {
            conn.Delete(country);
        }

        public async Task<List<Country>> RetornaPaises()
        {
            HttpClient client = new HttpClient();
            string url = "https://restcountries.com/v3.1/all";
            var response = await client.GetAsync(url);
            string response_json = await response.Content.ReadAsStringAsync();

            List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(response_json);

            return countries;
        }

        public async Task GuardarTodosLosPaises()
        {
            var countries = await RetornaPaises();
            foreach (var country in countries)
            {
                GuardarPais(country);
            }
        }
    }
}

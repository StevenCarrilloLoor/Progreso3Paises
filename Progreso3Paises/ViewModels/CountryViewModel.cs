using Progreso3Paises.Models;
using Progreso3Paises.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Progreso3Paises.ViewModels
{
    public class CountryViewModel : INotifyPropertyChanged
    {
        private Country _model;
        public Country Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged(nameof(Model));
                }
            }
        }

        public ICommand ComandoCargarPaises { get; }
        public ICommand ComandoGuardarPais { get; }
        public ICommand ComandoEliminarPais { get; }
        public ICommand ComandoEditarPais { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        //Constructor
        public CountryViewModel()
        {
            Model = new Country();

            ComandoCargarPaises = new Command(async () => await CargarPaises());
            ComandoGuardarPais = new Command<Country>(GuardarPais);
            ComandoEliminarPais = new Command<Country>(EliminarPais);
            ComandoEditarPais = new Command<Country>(EditarPais);
        }
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Método para cargar los países desde la API
        private async Task CargarPaises()
        {
            CountryRepo repo = new CountryRepo("Paises.DB");

            List<Country> countries = await repo.RetornaPaises();
            foreach (var country in countries)
            {
                repo.GuardarPais(country);
            }

            OnPropertyChanged(nameof(Model));
        }

        // Método para guardar el país en la base de datos SQLite
        private void GuardarPais(Country country)
        {
            if (country != null)
            {
                // Generar el código
                var random = new Random();
                var initials = string.Concat(country.Name.ToUpper().Where(char.IsLetter).Take(2));
                var randomNumber = random.Next(1000, 2000);
                country.Code = $"{initials}{randomNumber}"; // Almacenar el código generado en la propiedad 'Code'

                CountryRepo repo = new CountryRepo("Paises.DB");
                repo.GuardarPais(country);
            }
        }


        // Método para eliminar un país de la base de datos SQLite
        private void EliminarPais(Country country)
        {
            if (country != null)
            {
                CountryRepo repo = new CountryRepo("Paises.DB");
                repo.EliminarPais(country);
            }
        }

        // Método para editar un país en la base de datos SQLite
        private void EditarPais(Country country)
        {
            if (country != null)
            {
                CountryRepo repo = new CountryRepo("Paises.DB");
                repo.ActualizarStatus(country);
            }
        }
    }
}

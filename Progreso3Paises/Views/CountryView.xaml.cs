using Progreso3Paises.ViewModels;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Progreso3Paises.Views
{
    public partial class CountryView : ContentPage
    {
        private CountryViewModel _viewModel;

        public CountryView()
        {
            InitializeComponent();

            // Crear una nueva instancia de CountryViewModel
            _viewModel = new CountryViewModel();

            // Establecer el contexto de enlace para la vista
            BindingContext = _viewModel;
        }

        // Sobrescribir el método OnAppearing para cargar los datos cuando la vista aparezca
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Llamar al comando para cargar los países
            await ExecuteLoadCountriesCommand();
        }

        private async Task ExecuteLoadCountriesCommand()
        {
            if (_viewModel.ComandoCargarPaises.CanExecute(null))
            {
                _viewModel.ComandoCargarPaises.Execute(null);
            }
        }
    }
}

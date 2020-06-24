using LibraryManager.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Clientes : ContentPage
    {
        public Clientes()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lstClientes.ItemsSource = await App.Database.clienteH.GetClienteAsync();

            AtualizarAtivos();
        }

        //Atualiza a contagem de Clientes Ativos e Inativos
        private async void AtualizarAtivos()
        {
            decimal totalAtivos = 0M;
            decimal totalInativos = 0M;

            foreach (var item in await App.Database.clienteH.GetClienteAsync())
            {
                if (!item.Estado)
                {
                    totalInativos += 1;
                } 
                else
                {
                    totalAtivos += 1;
                }

            }
            lblAtivos.Text = "Ativos: " + totalAtivos;
            lblInativos.Text = "Inativos: " + totalInativos;
        }


        private async void addCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClienteCRUD(new Cliente()));
        }

        private async void lstClientes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var esteCliente = e.Item as Cliente;

            var resposta = await DisplayActionSheet("Opções", "Cancelar", "Editar", "Detalhes");

            //Opção Escolhida
            switch (resposta)
            {
                case "Editar":
                    await Navigation.PushAsync(new ClienteCRUD(esteCliente));
                    break;

                case "Detalhes":
                    await DisplayAlert("Informações do Cliente", esteCliente.getInfo(), "Ok");
                    break;

                default:
                    break;
            }
            
        }

        //Recebe a string da caixa de pesquisa e atualiza a listagem após nova query
        private async void pesquisaCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar pesquisaCliente = (SearchBar)sender;
            lstClientes.ItemsSource = null;
            lstClientes.ItemsSource = await App.Database.clienteH.GetClienteByNameAsync(pesquisaCliente.Text);
        }
    }
}
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
    public partial class Funcionarios : ContentPage
    {
        public Funcionarios()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lstFunc.ItemsSource = await App.Database.funcionarioH.GetFuncionarioAsync();

            AtualizarAtivos();
        }

        //Atualiza a contagem de Funcionários Ativos e Inativos
        private async void AtualizarAtivos()
        {
            decimal totalAtivos = 0M;
            decimal totalInativos = 0M;

            foreach (var item in await App.Database.funcionarioH.GetFuncionarioAsync())
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

        private async void addFunc_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FuncionarioCRUD(new Funcionario()));
        }


        private async void lstFunc_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var esteFuncionario = e.Item as Funcionario;

            var resposta = await DisplayActionSheet("Opções", "Cancelar", "Editar", "Detalhes");

            //Opção Escolhida
            switch (resposta)
            {
                case "Editar":
                    await Navigation.PushAsync(new FuncionarioCRUD(esteFuncionario));
                    break;

                case "Detalhes":
                    await DisplayAlert("Informações do Funcionario", esteFuncionario.getInfo(), "Ok");
                    break;

                default:
                    break;
            }
            
        }

        //Recebe a string da caixa de pesquisa e atualiza a listagem após nova query
        private async void pesquisaFuncionario_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar pesquisaFuncionario = (SearchBar)sender;
            lstFunc.ItemsSource = null;
            lstFunc.ItemsSource = await App.Database.funcionarioH.GetFuncionarioBySearchAsync(pesquisaFuncionario.Text);
        }
    }
}
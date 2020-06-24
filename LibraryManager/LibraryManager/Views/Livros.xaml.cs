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
    public partial class Livros : ContentPage
    {
        public Livros()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lstLivros.ItemsSource = await App.Database.livroH.GetLivroAsync();

            AtualizarDisponiveis();
        }

        //Atualiza a contagem de Livros Disponíveis e Emprestados
        private async void AtualizarDisponiveis()
        {
            decimal totalDisponiveis = 0M;
            decimal totalEmprestados = 0M;

            foreach (var item in await App.Database.livroH.GetLivroAsync())
            {
                if (!item.Estado)
                {
                    totalEmprestados += 1;
                }
                else
                {
                    totalDisponiveis += 1;
                }

            }
            lblDisponiveis.Text = "Disponíveis: " + totalDisponiveis;
            lblEmprestados.Text = "Emprestados: " + totalEmprestados;
        }

        private async void addLivro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LivroCRUD(new Livro()));
        }

        private async void lstLivros_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var esteLivro = e.Item as Livro;

            var resposta = await DisplayActionSheet("Opções", "Cancelar", "Editar", "Detalhes");

            //Opção Escolhida
            switch (resposta)
            {
                case "Editar":
                    await Navigation.PushAsync(new LivroCRUD(esteLivro));
                    break;

                case "Detalhes":
                    await DisplayAlert("Informações do Livro", esteLivro.getInfo(), "Ok");
                    break;

                default:
                    break;
            }
            
        }

        //Recebe a string da caixa de pesquisa e atualiza a listagem após nova query
        private async void pesquisaLivro_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar pesquisaLivro = (SearchBar)sender;
            lstLivros.ItemsSource = null;
            lstLivros.ItemsSource = await App.Database.livroH.GetLivroBySearchAsync(pesquisaLivro.Text);
        }
    }
}
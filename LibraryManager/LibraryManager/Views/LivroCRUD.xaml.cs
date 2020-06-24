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
    public partial class LivroCRUD : ContentPage
    {
        public LivroCRUD(Livro l)
        {
            InitializeComponent();
            BindingContext = l as Livro;

            //Verifica se é uma nova entrada ou update e altera o UI de acordo
            if (l.GuidLivro == "")
            {
                btnApagar.IsEnabled = false;
                btnGravar.Text = "Adicionar";
                btnGravar.BackgroundColor = Color.FromHex("#4bb543");
            }
            else
            {
                btnApagar.IsEnabled = true;
                btnGravar.Text = "Editar";
                btnGravar.BackgroundColor = Color.FromHex("#17a2b8");
            }
        }

        private async void btnGravar_Clicked(object sender, EventArgs e)
        {
            var livroAGravar = BindingContext as Livro;
            await App.Database.livroH.SaveLivroAsync(livroAGravar);
            await Navigation.PopAsync();
        }

        private async void btnApagar_Clicked(object sender, EventArgs e)
        {
            var livroADeletar = BindingContext as Livro;

            //Verifica se o Livro se encontra associoado à um empréstimo e, caso esteja, impede que o livro seja deletado
            var emp = await App.Database.emprestimoH.GetEmprestimoByLivroFKAsync(livroADeletar.GuidLivro);
            if (emp.Count == 1)
            {
                await DisplayAlert("Operação Inválida", "Este livro se encontra associado à um empréstimo!", "Ok");
            }
            else
            {
                await App.Database.livroH.DeleteLivroAsync(livroADeletar);
                await Navigation.PopAsync();
            }
            
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
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
    public partial class Intro : ContentPage
    {
        public Intro()
        {
            InitializeComponent();
        }

        private async void btnLivro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Livros());
        }

        private async void btnCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clientes());
        }

        private async void btnFuncionario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Funcionarios());
        }

        private async void btnEmprestimo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Emprestimos());
        }
    }
}
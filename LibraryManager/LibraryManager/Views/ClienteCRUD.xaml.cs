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
    public partial class ClienteCRUD : ContentPage
    {
        public ClienteCRUD(Cliente c)
        {
            InitializeComponent();
            BindingContext = c as Cliente;

            //Verifica se é uma nova entrada ou update e altera o UI de acordo
            if (c.GuidPessoa == "")
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
            var clienteAGravar = BindingContext as Cliente;

            //Verifica se o Cliente se encontra associoado à um empréstimo e, caso esteja, impede que o estado seja passado à inativo caso
            if (clienteAGravar.Estado == false)
            {
                var emp = await App.Database.emprestimoH.GetEmprestimoByClienteFKAsync(clienteAGravar.GuidPessoa);
                if (emp.Count == 1)
                {
                    await DisplayAlert("Operação Inválida", "Este cliente se encontra associado à um empréstimo!", "Ok");
                }
                else
                {
                    await App.Database.clienteH.SaveClienteAsync(clienteAGravar);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await App.Database.clienteH.SaveClienteAsync(clienteAGravar);
                await Navigation.PopAsync();
            }
            
        }

        private async void btnApagar_Clicked(object sender, EventArgs e)
        {
            var clienteADeletar = BindingContext as Cliente;

            //Verifica se o Cliente se encontra associoado à um empréstimo e, caso esteja, impede que o cliente seja deletado
            var emp = await App.Database.emprestimoH.GetEmprestimoByClienteFKAsync(clienteADeletar.GuidPessoa);
            if (emp.Count == 1)
            {
                await DisplayAlert("Operação Inválida", "Este cliente se encontra associado à um empréstimo!", "Ok");
            }
            else
            {
                await App.Database.clienteH.DeleteClienteAsync(clienteADeletar);
                await Navigation.PopAsync();
            }
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
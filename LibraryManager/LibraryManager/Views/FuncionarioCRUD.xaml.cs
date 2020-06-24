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
    public partial class FuncionarioCRUD : ContentPage
    {
        public FuncionarioCRUD(Funcionario f)
        {
            InitializeComponent();
            BindingContext = f as Funcionario;

            //Verifica se é uma nova entrada ou update e altera o UI de acordo
            if (f.GuidPessoa == "")
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
            var funcAGravar = BindingContext as Funcionario;

            //Verifica se o Funcionário se encontra associoado à um empréstimo e, caso esteja, impede que o estado seja passado à inativo caso
            if (funcAGravar.Estado == false)
            {
                var emp = await App.Database.emprestimoH.GetEmprestimoByFuncionarioFKAsync(funcAGravar.GuidPessoa);
                if (emp.Count == 1)
                {
                    await DisplayAlert("Operação Inválida", "Este funcionario se encontra associado à um empréstimo!", "Ok");
                }
                else
                {
                    await App.Database.funcionarioH.SaveFuncionarioAsync(funcAGravar);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await App.Database.funcionarioH.SaveFuncionarioAsync(funcAGravar);
                await Navigation.PopAsync();
            }
        }

        private async void btnApagar_Clicked(object sender, EventArgs e)
        {
            var funcADeletar = BindingContext as Funcionario;

            // Verifica se o Funcionário se encontra associoado à um empréstimo e, caso esteja, impede que o funcionário seja deletado
            var emp = await App.Database.emprestimoH.GetEmprestimoByFuncionarioFKAsync(funcADeletar.GuidPessoa);
            if (emp.Count == 1)
            {
                await DisplayAlert("Operação Inválida", "Este funcionario se encontra associado à um empréstimo!", "Ok");
            }
            else
            {
                await App.Database.funcionarioH.DeleteFuncionarioAsync(funcADeletar);
                await Navigation.PopAsync();
            }
            
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
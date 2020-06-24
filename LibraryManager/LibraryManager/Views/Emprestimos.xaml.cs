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
    public partial class Emprestimos : ContentPage
    {
        public Emprestimos()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lstEmprestimo.ItemsSource = await App.Database.emprestimoH.GetEmprestimoByEstadoAsync(1);
            swEstado.IsToggled = true;
        }

        private async void addEmprestimo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EmprestimoCRUD(new Emprestimo()));
        }

        private async void lstEmprestimo_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var esteEmprestimo = e.Item as Emprestimo;

            var resposta = await DisplayActionSheet("Opções", "Cancelar", "Editar", "Detalhes");

            //Opção Escolhida
            switch (resposta)
            {
                case "Editar":
                    await Navigation.PushAsync(new EmprestimoCRUD(esteEmprestimo));
                    break;

                case "Detalhes":
                    await DisplayAlert("Informações do Empréstimo", esteEmprestimo.getInfo(), "Ok");
                    break;

                default:
                    break;
            }
        }

        
        //Altera a listagem de empréstimos entre Ativos e Inativos de acordo com um switch
        private async void swEstado_Toggled(object sender, ToggledEventArgs e)
        {
            var estado = e.Value;
            if (estado == false)
            {
                lstEmprestimo.ItemsSource = null;
                lstEmprestimo.ItemsSource = await App.Database.emprestimoH.GetEmprestimoByEstadoAsync(0);
            }
            else
            {
                lstEmprestimo.ItemsSource = null;
                lstEmprestimo.ItemsSource = await App.Database.emprestimoH.GetEmprestimoByEstadoAsync(1);
            }
        }
    }
}
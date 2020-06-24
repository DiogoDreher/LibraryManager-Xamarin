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
    public partial class EmprestimoCRUD : ContentPage
    {
        public EmprestimoCRUD(Emprestimo emp)
        {
            InitializeComponent();
            BindingContext = emp as Emprestimo;

            //Verifica se é uma nova entrada ou update e altera o UI de acordo
            if (emp.uidEmprestimo == "")
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Popula os pickers de Client, Livro e Funcionário 
            pickerCliente.ItemsSource = await App.Database.clienteH.GetClienteAtivoAsync();
            pickerLivro.ItemsSource = await App.Database.livroH.GetLivroDisponivelAsync();
            pickerFuncionario.ItemsSource = await App.Database.funcionarioH.GetFuncionarioAtivoAsync();

        }

        private async void btnGravar_Clicked(object sender, EventArgs e)
        {
            var emprestimoAGravar = BindingContext as Emprestimo;

            //Verifica se a data de empréstimo selecionada não está no "futuro"
            if (emprestimoAGravar.DataEmp > emprestimoAGravar.Data.AddSeconds(1))
            {
                await DisplayAlert("Operação inválida!", "Data inválida.", "Ok");
            }
            else
            {
                //Verifica se é um novo empréstimo ou uma edição
                if (emprestimoAGravar.uidEmprestimo == "")
                {
                    //Verifica se algum picker foi deixado vazio
                    if (pickerCliente.SelectedItem == null || pickerLivro.SelectedItem == null || pickerFuncionario.SelectedItem == null)
                    {
                        await DisplayAlert("Operação inválida!", "Todos os campos devem ser preenchidos.", "Ok");
                    }
                    else
                    {
                        //Cria um objeto Cliente com as informações de objeto contidas no picker e extrai o Nome e Guid
                        Cliente c = pickerCliente.SelectedItem as Cliente;
                        emprestimoAGravar.NomeCliente = c.Nome;
                        emprestimoAGravar.GuidCliente = c.GuidPessoa;

                        //Cria um objeto Funcionário com as informações de objeto contidas no picker e extrai o Nome e Guid
                        Funcionario f = pickerFuncionario.SelectedItem as Funcionario;
                        emprestimoAGravar.NomeFuncionario = f.Nome;
                        emprestimoAGravar.GuidFuncionario = f.GuidPessoa;

                        //Cria um objeto Livro com as informações de objeto contidas no picker e extrai o Nome e Guid
                        Livro l = pickerLivro.SelectedItem as Livro;
                        emprestimoAGravar.NomeLivro = l.Nome;
                        emprestimoAGravar.GuidLivro = l.GuidLivro;

                        emprestimoAGravar.Color = "#4bb543";


                        //Verifica se o Cliente se encontra associoado à um empréstimo em atraso
                        var emps = await App.Database.emprestimoH.GetEmprestimoByClienteFKAsync(emprestimoAGravar.GuidCliente);
                        var atraso = 0;

                        if (emps.Count > 0)
                        {
                            foreach (var emp in emps)
                            {
                                if (emp.Color == "#f32013")
                                {
                                    atraso++;
                                }
                            }
                           
                        }

                        if (atraso > 0)
                        {
                            if (atraso == 1)
                            {
                                await DisplayAlert("Empréstimo Bloqueado", "Este cliente se encontra associado à " + atraso + " empréstimo em atraso!", "Ok");
                            }
                            else
                            {
                                await DisplayAlert("Empréstimo Bloqueado", "Este cliente se encontra associado à " + atraso + " empréstimos em atraso!", "Ok");
                            }
                            
                        }
                        else
                        {
                            //Faz update dos campos "Data de Empréstimo", "Estado" e "Cor" na tabela Livro de acordo com o Guid
                            await App.Database.livroH.UpdateDataEmp(emprestimoAGravar.DataEmp, 0, "#f32013", emprestimoAGravar.GuidLivro);

                            await App.Database.emprestimoH.SaveEmprestimoAsync(emprestimoAGravar);

                            await Navigation.PopAsync();
                        }

                        
                    }

                }
                else
                {


                    //Verifica se o Cliente foi alterado
                    if (pickerCliente.SelectedItem != null)
                    {
                        Cliente c = pickerCliente.SelectedItem as Cliente;
                        emprestimoAGravar.NomeCliente = c.Nome;
                        emprestimoAGravar.GuidCliente = c.GuidPessoa;
                    }

                    //Verifica se o Livro foi alterado
                    if (pickerLivro.SelectedItem != null)
                    {
                        Livro l = pickerLivro.SelectedItem as Livro;
                        emprestimoAGravar.NomeLivro = l.Nome;
                        emprestimoAGravar.GuidLivro = l.GuidLivro;
                    }

                    //Verifica se o Funcionario foi alterado
                    if (pickerFuncionario.SelectedItem != null)
                    {
                        Funcionario f = pickerFuncionario.SelectedItem as Funcionario;
                        emprestimoAGravar.NomeFuncionario = f.Nome;
                        emprestimoAGravar.GuidFuncionario = f.GuidPessoa;
                    }


                    //Verifica se o Cliente se encontra associoado à um empréstimo em atraso
                    var emps = await App.Database.emprestimoH.GetEmprestimoByClienteFKAsync(emprestimoAGravar.GuidCliente);
                    var atraso = 0;

                    if (emps.Count > 1)
                    {
                        foreach (var emp in emps)
                        {
                            if (emp.Color == "#f32013")
                            {
                                atraso++;
                            }
                        }

                    }

                    if (atraso > 0)
                    {
                        if (atraso > 1)
                        {
                            await DisplayAlert("Empréstimo Bloqueado", "Este cliente se encontra associado à " + atraso + " empréstimo em atraso!", "Ok");
                        }
                        else
                        {
                            await DisplayAlert("Empréstimo Bloqueado", "Este cliente se encontra associado à " + atraso + " empréstimos em atraso!", "Ok");
                        }

                    }
                    else
                    {
                        //Verifica se o Livro foi devolvido e altera o estado na tabela Livro
                        if (emprestimoAGravar.Estado == false)
                        {
                            await App.Database.livroH.UpdateDataEmp(DateTime.UtcNow, 1, "#4bb543", emprestimoAGravar.GuidLivro);
                        }
                        else
                        {
                            await App.Database.livroH.UpdateDataEmp(emprestimoAGravar.DataEmp, 0, "#f32013", emprestimoAGravar.GuidLivro);
                        }

                        await App.Database.emprestimoH.SaveEmprestimoAsync(emprestimoAGravar);

                        await Navigation.PopAsync();
                    }


                    
                }
            }
        }

        private async void btnApagar_Clicked(object sender, EventArgs e)
        {
            var emprestimoADeletar = BindingContext as Emprestimo;

            //Faz update dos campos "Data de Empréstimo", "Estado" e "Cor" na tabela Livro de acordo com o Guid
            await App.Database.livroH.UpdateDataEmp(DateTime.UtcNow, 1, "#4bb543", emprestimoADeletar.GuidLivro);

            await App.Database.emprestimoH.DeleteEmprestimoAsync(emprestimoADeletar);
            await Navigation.PopAsync();
        }

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
using LibraryManager.DomainModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Wrapper
{
    public class dbHelper
    {
        readonly SQLiteAsyncConnection myDB;
        public livroHelper livroH;
        public clienteHelper clienteH;
        public funcionarioHelper funcionarioH;
        public emprestimoHelper emprestimoH;

        public dbHelper(string dbPath)
        {
            myDB = new SQLiteAsyncConnection(dbPath);

            //Cria as tabelas Cliente, Livro, Funcionário e Empréstimo
            myDB.CreateTableAsync<Cliente>().Wait();
            myDB.CreateTableAsync<Livro>().Wait();
            myDB.CreateTableAsync<Funcionario>().Wait();
            myDB.CreateTableAsync<Emprestimo>().Wait();

            //Instancia os respectivos helpers e envia a conexão como argumento
            livroH = new livroHelper(myDB);
            clienteH = new clienteHelper(myDB);
            funcionarioH = new funcionarioHelper(myDB);
            emprestimoH = new emprestimoHelper(myDB);
        }


    }
}

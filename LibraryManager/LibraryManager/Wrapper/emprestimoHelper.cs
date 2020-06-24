using LibraryManager.DomainModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Wrapper
{
    public class emprestimoHelper
    {
        readonly SQLiteAsyncConnection myDB;

        public emprestimoHelper(SQLiteAsyncConnection DB)
        {
            myDB = DB;
        }

        #region GETS

        //Retorna os empréstimos ativos ou inativos de acordo com o input do slider existente na view Emprestimos
        public Task<List<Emprestimo>> GetEmprestimoByEstadoAsync(int estado)
        {
            return myDB.QueryAsync<Emprestimo>("SELECT * FROM [Emprestimo] WHERE [Estado] = " + estado + " ORDER BY [Data]");
        }

        //Verifica de o Guid do livro se encontra associado à algum empréstimo ativo
        public Task<List<Emprestimo>> GetEmprestimoByLivroFKAsync(string livroFK)
        {
            return myDB.QueryAsync<Emprestimo>("SELECT * FROM [Emprestimo] WHERE [GuidLivro] = '" + livroFK + "' AND " +
                                               "[Estado] = 1");
        }

        //Verifica de o Guid do cliente se encontra associado à algum empréstimo ativo
        public Task<List<Emprestimo>> GetEmprestimoByClienteFKAsync(string clienteFK)
        {
            return myDB.QueryAsync<Emprestimo>("SELECT * FROM [Emprestimo] WHERE [GuidCliente] = '" + clienteFK + "' AND " +
                                               "[Estado] = 1");
        }

        //Verifica de o Guid do funcionário se encontra associado à algum empréstimo ativo
        public Task<List<Emprestimo>> GetEmprestimoByFuncionarioFKAsync(string funcionarioFK)
        {
            return myDB.QueryAsync<Emprestimo>("SELECT * FROM [Emprestimo] WHERE [GuidFuncionario] = '" + funcionarioFK + "' AND " +
                                               "[Estado] = 1");
        }


        #endregion

        #region Save/Update
        public Task<int> SaveEmprestimoAsync(Emprestimo e)
        {
            if (e.uidEmprestimo != "")
            {
                if (e.Estado == false)
                {
                    e.DataDevolucao = DateTime.UtcNow;
                }
                return myDB.UpdateAsync(e);
            }
            else
            {
                e.setGuidEmprestimo();
                e.Data = DateTime.UtcNow;

                return myDB.InsertAsync(e);
            }
        }
        #endregion

        #region Delete
        public Task<int> DeleteEmprestimoAsync(Emprestimo e)
        {
            return myDB.DeleteAsync(e);
        }
        #endregion
    }
}

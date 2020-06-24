using LibraryManager.DomainModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Wrapper
{
    public class funcionarioHelper
    {
        readonly SQLiteAsyncConnection myDB;

        public funcionarioHelper(SQLiteAsyncConnection DB)
        {
            myDB = DB;
        }

        #region GETS

        public Task<List<Funcionario>> GetFuncionarioAsync()
        {
            return myDB.Table<Funcionario>().ToListAsync();
        }

        //Retorna apenas os Funcionários com estado ativo para popular o picker de empréstimo
        public Task<List<Funcionario>> GetFuncionarioAtivoAsync()
        {
            return myDB.QueryAsync<Funcionario>("SELECT * FROM [Funcionario] WHERE [Estado] = 1 ORDER BY [Data]");
        }

        //Recebe uma string e faz uma pesquisa a base de dados para campos que possuam, mesmo que parcialmente, essa string
        public Task<List<Funcionario>> GetFuncionarioBySearchAsync(string input)
        {
            return myDB.QueryAsync<Funcionario>("SELECT * FROM [Funcionario] WHERE [Nome] LIKE '" + input + "%' OR [Cargo] LIKE '" + input + "%' ORDER BY [Data]");
        }


        #endregion

        #region Save/Update
        public Task<int> SaveFuncionarioAsync(Funcionario f)
        {
            if (f.GuidPessoa != "")
            {
                if (f.Estado == false)
                {
                    f.Color = "#f32013";
                }
                else
                {
                    f.Color = "#4bb543";
                }
                return myDB.UpdateAsync(f);
            }
            else
            {
                f.setGuidPessoa();
                f.Data = DateTime.UtcNow;
                return myDB.InsertAsync(f);
            }
        }
        #endregion

        #region Delete
        public Task<int> DeleteFuncionarioAsync(Funcionario f)
        {
            return myDB.DeleteAsync(f);
        }
        #endregion
    }
}

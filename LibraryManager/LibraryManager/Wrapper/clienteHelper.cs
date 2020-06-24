
using LibraryManager.DomainModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Wrapper
{
    public class clienteHelper 
    {
        readonly SQLiteAsyncConnection myDB;

        public clienteHelper(SQLiteAsyncConnection DB)
        {
            myDB = DB;
        }

        #region GETS

        public Task<List<Cliente>> GetClienteAsync()
        {
            return myDB.Table<Cliente>().ToListAsync();
        }

        //Retorna apenas os Clientes com estado ativo para popular o picker de empréstimo
        public Task<List<Cliente>> GetClienteAtivoAsync()
        {
            return myDB.QueryAsync<Cliente>("SELECT * FROM [Cliente] WHERE [Estado] = 1 ORDER BY [Data]");
        }

        //Recebe uma string e faz uma pesquisa a base de dados para campos que possuam, mesmo que parcialmente, essa string
        public Task<List<Cliente>> GetClienteByNameAsync(string input)
        {
            return myDB.QueryAsync<Cliente>("SELECT * FROM [Cliente] WHERE [Nome] LIKE '" + input + "%' ORDER BY [Data]");
        }

        #endregion

        #region Save/Update
        public Task<int> SaveClienteAsync(Cliente c)
        {
            if (c.GuidPessoa != "")
            {                
                if (c.Estado == false)
                {
                    c.Color = "#f32013";
                }
                else
                {
                    c.Color = "#4bb543";
                }
                return myDB.UpdateAsync(c);
            }
            else
            {
                c.setGuidPessoa();
                c.Data = DateTime.UtcNow;
                return myDB.InsertAsync(c);
            }
        }
        #endregion

        #region Delete
        public Task<int> DeleteClienteAsync(Cliente c)
        {
            return myDB.DeleteAsync(c);
        }
        #endregion
    }
}

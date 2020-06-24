using LibraryManager.DomainModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryManager.Wrapper
{
    public class livroHelper
    {
        readonly SQLiteAsyncConnection myDB;
        public livroHelper(SQLiteAsyncConnection DB)
        {
            myDB = DB;
        }

        #region GETS

        public Task<List<Livro>> GetLivroAsync()
        {
            return myDB.Table<Livro>().ToListAsync();
        }

        //Retorna apenas os Livros com estado disponível para popular o picker de empréstimo
        public Task<List<Livro>> GetLivroDisponivelAsync()
        {
            return myDB.QueryAsync<Livro>("SELECT * FROM [Livro] WHERE [Estado] = 1 ORDER BY [Data]");
        }

        //Recebe uma string e faz uma pesquisa a base de dados para campos que possuam, mesmo que parcialmente, essa string
        public Task<List<Livro>> GetLivroBySearchAsync(string input)
        {
            return myDB.QueryAsync<Livro>("SELECT * FROM [Livro] WHERE [Nome] LIKE '" + input + "%' OR " +
                                                                      "[Autor] LIKE '" + input + "%' OR " +
                                                                      "[Genero] LIKE '" + input + "%' OR " +
                                                                      "[Editora] LIKE '" + input + "%' " +
                                                                      "ORDER BY [Data]");
        }


        #endregion

        #region Save/Update
        public Task<int> SaveLivroAsync(Livro l)
        {
            if (l.GuidLivro != "")
            {
                return myDB.UpdateAsync(l);
            }
            else
            {
                l.setGuidLivro();
                l.Data = DateTime.UtcNow;
                return myDB.InsertAsync(l);
            }
        }

        public Task<List<Livro>> UpdateDataEmp(DateTime dataEmp, int estado, string cor, string id)
        {
            return myDB.QueryAsync<Livro>($"UPDATE [Livro] SET [Emprestimo] = ' {dataEmp} ', [Color] = '" + cor + "', [Estado] = " + estado + $" WHERE [GuidLivro] = '{id}'" );
        }
        #endregion

        #region Delete
        public Task<int> DeleteLivroAsync(Livro l)
        {
            return myDB.DeleteAsync(l);
        }
        #endregion
    }
}

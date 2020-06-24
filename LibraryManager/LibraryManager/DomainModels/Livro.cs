using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.DomainModels
{
    public class Livro
    {
        #region "Atributos"
        private string _guidLivro;
        private string _nome;
        private string _autor;
        private int _ano;
        private string _genero;
        private string _editora;
        private bool _status;
        private string _color;
        private DateTime _dataRegisto;
        private string _dataEmprestimo;
        #endregion

        #region "Propriedades"
        [PrimaryKey]
        public string GuidLivro
        {
            get
            {
                return _guidLivro;
            }
            set
            {
                _guidLivro = value;
            }
        }

        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
            }
        }

        public string Autor
        {
            get
            {
                return _autor;
            }
            set
            {
                _autor = value;
            }
        }

        public int Ano
        {
            get
            {
                return _ano;
            }
            set
            {
                _ano = value;
                if (_ano > 2020)
                {
                    _ano = Convert.ToInt32(2019);
                }
            }
        }

        public string Genero
        {
            get
            {
                return _genero;
            }
            set
            {
                _genero = value;
            }
        }

        public string Editora
        {
            get
            {
                return _editora;
            }
            set
            {
                _editora = value;
            }
        }

        public bool Estado
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return _dataRegisto;
            }
            set
            {
                _dataRegisto = value;
            }
        }

        public string Emprestimo
        {
            get
            {
                return _dataEmprestimo;
            }
            set
            {
                _dataEmprestimo = value;

            }
        }

       
        #endregion

        #region "Construtores"
        public Livro()
        {
            _guidLivro = "";
            Nome = "";
            Autor = "";
            Ano = 1500;
            Genero = "";
            Editora = "";
            Estado = true;
            Color = "#4bb543";
            Data = Convert.ToDateTime("1900/01/01");
            Emprestimo = "";
        }
        #endregion

        #region "Outros Métodos"

        public void setGuidLivro()
        {
            if (_guidLivro == "")
            {
                _guidLivro = Guid.NewGuid().ToString();
            }
        }
        public string getInfo()
        {
            return //$"ID: {GuidLivro}" + Environment.NewLine +
                    $"Nome: {Nome}" + Environment.NewLine +
                    $"Autor: {Autor}" + Environment.NewLine +
                    $"Ano: {Ano}" + Environment.NewLine +
                    $"Gênero: {Genero}" + Environment.NewLine +
                    $"Editora: {Editora}" + Environment.NewLine +
                    ((Estado == true) ? "Estado: Disponível " : "Estado: Emprestado " + Environment.NewLine + "Empréstimo: " + Emprestimo.Substring(0,10)) + Environment.NewLine +
                    $"Data de Registo: {Data.ToShortDateString()}";

        }
        public override string ToString()
        {
            return Nome;
        }
        #endregion
    }
}

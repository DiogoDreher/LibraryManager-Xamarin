using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.DomainModels
{
    public class Emprestimo
    {
        #region: "Atributos"
        private string _uidEmprestimo;
        private string _guidCliente;
        private string _nomeCliente;
        private string _guidLivro;
        private string _nomeLivro;
        private string _guidFuncionario;
        private string _nomeFuncionario;
        private string _color;
        private DateTime _dataEmprestimo;
        private DateTime _dataRegisto;
        private DateTime _dataDevolucao;
        private bool _estado;
        #endregion

        #region: "Propriedades/Metodos"
        [PrimaryKey]
        public string uidEmprestimo
        {
            get
            {
                return _uidEmprestimo;
            }
            set
            {
                _uidEmprestimo = value;
            }
        }

        public string GuidCliente
        {
            get
            {
                return _guidCliente;
            }
            set
            {
                _guidCliente = value;
            }
        }
        
        public string NomeCliente
        {
            get
            {
                return _nomeCliente;
            }
            set
            {
                _nomeCliente = value;
            }
        }

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

        public string NomeLivro
        {
            get
            {
                return _nomeLivro;
            }
            set
            {
                _nomeLivro = value;
            }
        }

        public string GuidFuncionario
        {
            get
            {
                return _guidFuncionario;
            }
            set
            {
                _guidFuncionario = value;
            }
        }

        public string NomeFuncionario
        {
            get
            {
                return _nomeFuncionario;
            }
            set
            {
                _nomeFuncionario = value;
            }
        }

        public DateTime DataEmp
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

        public DateTime DataDevolucao
        {
            get
            {
                return _dataDevolucao;
            }
            set
            {
                _dataDevolucao = value;
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

        public bool Estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
                if (_estado == false)
                {
                    Color = "#C0C0C0";
                }
                else
                {
                    if (DateTime.Now > DataEmp.AddDays(30))
                    {
                        Color = "#f32013";
                    }
                    else
                    {
                        Color = "#4bb543";
                    }
                }
            }
        }

        #endregion

        #region: "Construtores"
        public Emprestimo()
        {
            _uidEmprestimo = "";
            GuidCliente = "";
            NomeCliente = "";
            GuidFuncionario = "";
            NomeFuncionario = "";
            GuidLivro = "";
            NomeLivro = "";
            Color = "";
            Data = DateTime.UtcNow;
            DataEmp = DateTime.UtcNow;
            Estado = true;
        }


        #endregion

        #region: "Outros Metodos"

        public void setGuidEmprestimo()
        {
            if (_uidEmprestimo == "")
            {
                _uidEmprestimo = Guid.NewGuid().ToString();
            }
        }
        public string getInfo()
        {
            return //$"ID: {uidEmprestimo}" + Environment.NewLine +
                    //$"LivroID: {GuidLivro}" + Environment.NewLine +
                    //$"ClienteID: {GuidCliente}" + Environment.NewLine +
                    //$"FuncionárioID: {GuidFuncionario}" + Environment.NewLine +
                    $"Livro: {NomeLivro}" + Environment.NewLine +
                    $"Cliente: {NomeCliente}" + Environment.NewLine +
                    $"Funcionário: {NomeFuncionario}" + Environment.NewLine +
                    ((Estado == true) ? "Estado: Emprestado " : "Estado: Devolvido ") + Environment.NewLine +
                    $"Data do Empréstimo: {DataEmp.ToShortDateString()}" + Environment.NewLine +
                    ((Estado == false) ? $"Data da Devolução: { DataDevolucao.ToShortDateString()}" + Environment.NewLine  : "") +
                    $"Data de Registo: {Data.ToShortDateString()}";
        }
        #endregion
    }
}

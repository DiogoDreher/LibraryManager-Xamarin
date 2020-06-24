using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.DomainModels
{
    public class Funcionario : Pessoa
    {
        #region "Atributos"
        private string _cargo;
        private DateTime _dtRegisto;
        #endregion

        #region "Propriedades"
        public string Cargo
        {
            get
            {
                return _cargo;
            }
            set
            {
                _cargo = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return _dtRegisto;
            }
            set
            {
                _dtRegisto = value;
            }
        }

        #endregion

        #region "Construtores"
        public Funcionario() : base()
        {
            Cargo = "";
            Data = Convert.ToDateTime("1900/01/01");
        }
        #endregion

        #region "Outros Métodos"
        public override string getInfo()
        {
            return //$"ID: {GuidPessoa}" + Environment.NewLine +
                    $"Nome: {Nome}" + Environment.NewLine +
                    $"Endereço: {Endereco}" + Environment.NewLine +
                    $"Telefone: {Telefone}" + Environment.NewLine +
                    $"Cargo: {Cargo}" + Environment.NewLine +
                    ((Estado == true) ? "Estado: Ativo " : "Estado: Inativo ") + Environment.NewLine +
                    $"Data de Registo: {Data.ToShortDateString()}";
        }
        #endregion
    }
}

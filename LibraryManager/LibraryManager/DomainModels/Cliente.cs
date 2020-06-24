using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.DomainModels
{
    public class Cliente : Pessoa
    {
        #region "Atributos"
        private DateTime _dtRegisto;
        #endregion

        #region "Propriedades"
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
        public Cliente() : base()
        {
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
                    ((Estado == true) ? "Estado: Ativo " : "Estado: Inativo ") + Environment.NewLine +
                    $"Data de Registo: {Data.ToShortDateString()}";
        }

        public override string ToString()
        {
            return Nome;
        }


        #endregion
    }
}

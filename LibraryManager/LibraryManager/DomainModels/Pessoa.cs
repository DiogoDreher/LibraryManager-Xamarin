using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.DomainModels
{
    public class Pessoa
    {

        #region "Atributos"        
        private string _guidPessoa;
        private string _nome;
        private string _endereco;
        private string _telefone;
        private bool _status;
        private string _color;
        #endregion


        #region "Propriedades"
        [PrimaryKey]
        public string GuidPessoa
        {
            get
            {
                return _guidPessoa;
            }
            set
            {
                _guidPessoa = value;
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

        public string Endereco
        {
            get
            {
                return _endereco;
            }
            set
            {
                _endereco = value;
            }
        }

        public string Telefone
        {
            get
            {
                return _telefone;
            }
            set
            {
                _telefone = value;
                if (_telefone.Length > 16)
                {
                    _telefone = "+000 000 000 000";
                }
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

        #endregion

        #region "Construtores"
        public Pessoa()
        {
            _guidPessoa = "";
            Nome = "";
            Endereco = "";
            Telefone = "";
            Estado = true;
            Color = "#4bb543";
        }

        #endregion

        #region "Outros Métodos"
        public void setGuidPessoa()
        {
            if (_guidPessoa == "")
            {
                _guidPessoa = Guid.NewGuid().ToString();
            }
        }
        public virtual string getInfo()
        {
            return $"ID: {GuidPessoa}" + Environment.NewLine + 
                    $"Nome: {Nome}" + Environment.NewLine + 
                    $"Endereço: {Endereco}" + Environment.NewLine + 
                    $"Telefone: {Telefone}";
        }

        public override string ToString()
        {
            return Nome;
        }
        #endregion
    }
}

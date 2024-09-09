using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long AdicionarBeneficiario(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.AdicionarBeneficiario(beneficiario);
        }

        public void EditarBeneficiario(long id, string nome)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            benef.EditarBeneficiario(id, nome);
        }

        public void ExcluirBeneficiario(long idBeneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            benef.ExcluirBeneficiario(idBeneficiario);
        }

        public DML.Beneficiario ObterBeneficiario(long idBeneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.ObterBeneficiario(idBeneficiario);
        }

        public List<DML.Beneficiario> ObterBeneficiarios(long idCliente)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.ObterBeneficiarios(idCliente);
        }

        public bool VerificarAssociacaoAoCliente(string cpf, long idCliente)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.VerificarAssociacaoAoCliente(cpf, idCliente);
        }

        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.VerificarExistencia(CPF);
        }
    }
}

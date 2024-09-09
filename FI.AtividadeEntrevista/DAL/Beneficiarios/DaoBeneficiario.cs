using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiarios
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void AdicionarBeneficiario(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCliente));

            base.Executar("FI_SP_IncBenef", parametros);
        }

        internal void EditarBeneficiario(long id, string nome)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", nome));
            

            base.Executar("FI_SP_EditarBeneficiario", parametros);
        }

        internal void ExcluirBeneficiario(long idBeneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", idBeneficiario));

            base.Executar("FI_SP_ExcluirBeneficiario", parametros);
        }

        internal DML.Beneficiario ObterBeneficiario(long idBeneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", idBeneficiario));

            var ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            var beneficiarios = Converter(ds);

            return beneficiarios.FirstOrDefault();
        }

        internal List<Beneficiario> ObterBeneficiarios(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiariosCliente", parametros);
            List<DML.Beneficiario> beneficiarios = Converter(ds);

            return beneficiarios;
        }

        internal bool VerificarAssociacaoAoCliente(string CPF, long IdCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", IdCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarioAssociadoAoCliente", parametros);

            return (int)ds.Tables[0].Rows[0].ItemArray[0] > 0;
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario benef = new DML.Beneficiario();
                    benef.Id = row.Field<long>("Id");
                    benef.IdCliente = row.Field<long>("IdCliente");
                    benef.Nome = row.Field<string>("Nome");
                    benef.CPF = row.Field<string>("CPF");
                    lista.Add(benef);
                }
            }

            return lista;
        }
    }
}

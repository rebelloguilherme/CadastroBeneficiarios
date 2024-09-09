using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.BLL.Validators;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private const string _MENSAGEM_BENEFICIARIO_NAO_ENCONTRADO = "Beneficiário não encontrado.";
        private const string _MENSAGEM_BENEFICIARIO_EXCLUIDO_SUCESSO = "Beneficiário excluído com sucesso.";
        private const string _MENSAGEM_ID_BENEFICIARIO_INVALIDO = "ID do beneficiário inválido";
        private const string _MENSAGEM_ID_CLIENTE_INVALIDO = "ID do cliente inválido";
        private const string _MENSAGEM_BENEFICIARIO_JAH_CADASTRADO = "Beneficiario já cadastrado";
        private const string _MENSAGEM_BENEFICIARIO_JAH_CADASTRADO_PARA_CLIENTE = "Beneficiário já cadastrado para este cliente";
        private const string _MENSAGEM_CADASTRO_BENEFICIARIO_EFETUADO_SUCESSO = "Cadastro beneficiário efetuado com sucesso";
        private const string _MENSAGEM_BENEFICIARIO_EDITADO_SUCESSO = "Beneficiário editado com sucesso.";

        [HttpPost]
        public JsonResult Obter(long id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException(_MENSAGEM_ID_BENEFICIARIO_INVALIDO);
                }

                var boBeneficiario = new BoBeneficiario();

                var beneficiario = boBeneficiario.ObterBeneficiario(id);

                if (beneficiario is null)
                {
                    return Json(new { sucesso = false, mensagem = _MENSAGEM_BENEFICIARIO_NAO_ENCONTRADO });
                }

                return Json(new { sucesso = true, beneficiario });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json($"Erro: {ex.Message}");
            }
        }


        [HttpPost]
        public JsonResult ObterLista(long idCliente)
        {
            try
            {
                if (idCliente <= 0)
                {
                    throw new ArgumentException(_MENSAGEM_ID_CLIENTE_INVALIDO);
                }

                var boBeneficiario = new BoBeneficiario();

                var beneficiarios = boBeneficiario.ObterBeneficiarios(idCliente);

                return Json(new { sucesso = true, beneficiarios });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json($"Erro: {ex.Message}");
            }
        }


        [HttpPost]
        public JsonResult Adicionar(BeneficiarioModel model)
        {
            var boBeneficiario = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            try
            {
                CpfValidador.ValidarCPF(model.CPF);
                var beneficiarioJahCadastrado = boBeneficiario.VerificarExistencia(model.CPF);
                if (beneficiarioJahCadastrado)
                {
                    throw new Exception(_MENSAGEM_BENEFICIARIO_JAH_CADASTRADO);
                }

                var beneficiarioJahAssociadoAoCliente = boBeneficiario.VerificarAssociacaoAoCliente(model.CPF, model.IdCliente);
                if (beneficiarioJahAssociadoAoCliente)
                {
                    throw new Exception(_MENSAGEM_BENEFICIARIO_JAH_CADASTRADO_PARA_CLIENTE);
                }

                boBeneficiario.AdicionarBeneficiario(new Beneficiario()
                {
                    IdCliente = model.IdCliente,
                    Nome = model.Nome,
                    CPF = model.CPF
                });

                return Json(_MENSAGEM_CADASTRO_BENEFICIARIO_EFETUADO_SUCESSO);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json($"Erro:{ex.Message}");
            }
        }

        [HttpPost]
        public JsonResult Excluir(long id)
        {
            try
            {
                var boBeneficiario = new BoBeneficiario();

                var beneficiario = boBeneficiario.ObterBeneficiario(id);
                if (beneficiario == null)
                {
                    throw new Exception(_MENSAGEM_BENEFICIARIO_NAO_ENCONTRADO);
                }

                boBeneficiario.ExcluirBeneficiario(id);

                return Json(new { sucesso = true, mensagem = _MENSAGEM_BENEFICIARIO_EXCLUIDO_SUCESSO });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public JsonResult Editar(long id, string nome)
        {
            try
            {
                var boBeneficiario = new BoBeneficiario();

                var beneficiario = boBeneficiario.ObterBeneficiario(id);
                if (beneficiario == null)
                {
                    throw new Exception(_MENSAGEM_BENEFICIARIO_NAO_ENCONTRADO);
                }

                boBeneficiario.EditarBeneficiario(id, nome);

                return Json(new { sucesso = true, mensagem = _MENSAGEM_BENEFICIARIO_EDITADO_SUCESSO });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json($"Erro: {ex.Message}");
            }
        }


    }
}
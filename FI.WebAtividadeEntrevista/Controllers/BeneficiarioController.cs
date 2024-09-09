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

        [HttpPost]
        public JsonResult Obter(long id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("ID do beneficiário inválido");
                }

                var boBeneficiario = new BoBeneficiario();

                var beneficiario = boBeneficiario.ObterBeneficiario(id);

                if (beneficiario is null)
                {
                    return Json(new { sucesso = false, mensagem = "Beneficiário não encontrado." });
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
                    throw new ArgumentException("ID do cliente inválido");
                }

                var boBeneficiario = new BoBeneficiario();

                var beneficiarios = boBeneficiario.ObterBeneficiarios(idCliente);

                if (!beneficiarios.Any())
                {
                    return Json(new { sucesso = false, mensagem = "Nenhum beneficiário encontrado para o cliente." });
                }

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
                    throw new Exception("Beneficiario já cadastrado");
                }

                var beneficiarioJahAssociadoAoCliente = boBeneficiario.VerificarAssociacaoAoCliente(model.CPF, model.IdCliente);
                if (beneficiarioJahAssociadoAoCliente)
                {
                    throw new Exception("Beneficiário já cadastrado para este cliente");
                }

                model.Id = boBeneficiario.AdicionarBeneficiario(new Beneficiario()
                {
                    IdCliente = model.IdCliente,
                    Nome = model.Nome,
                    CPF = model.CPF
                });

                return Json("Cadastro beneficiário efetuado com sucesso");
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
                    throw new Exception("Beneficiário não encontrado.");
                }

                boBeneficiario.ExcluirBeneficiario(id);

                return Json(new { sucesso = true, mensagem = "Beneficiário excluído com sucesso." });
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
                    throw new Exception("Beneficiário não encontrado.");
                }

                boBeneficiario.EditarBeneficiario(id, nome);

                return Json(new { sucesso = true, mensagem = "Beneficiário excluído com sucesso." });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json($"Erro: {ex.Message}");
            }
        }


    }
}
// Defina a fun��o salvarBeneficiario como uma fun��o global
//function salvarBeneficiario() {
//    var cpf = $('#cpfBeneficiario').val();
//    var nome = $('#nomeBeneficiario').val();
//    var idCliente = $('#idCliente').val(); // Certifique-se que esse campo existe ou pegue o ID corretamente

//    $.ajax({
//        url: '@Url.Action("AdicionarBeneficiario", "Cliente")',
//        type: 'POST',
//        data: { cpf: cpf, nome: nome, idCliente: idCliente },
//        success: function (response) {
//            if (response.sucesso) {
//                alert('Benefici�rio adicionado com sucesso!');
//                $('#modalBeneficiario').modal('hide');
//                location.reload(); // Exemplo para recarregar a p�gina ap�s a inser��o
//            } else {
//                alert(response.mensagem);
//            }
//        }
//    });
//}

//$(document).ready(function () {
//    // Outras funcionalidades que precisem do document ready
//});
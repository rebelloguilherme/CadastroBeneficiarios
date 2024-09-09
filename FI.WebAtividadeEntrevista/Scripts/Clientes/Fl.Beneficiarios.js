// Defina a função salvarBeneficiario como uma função global
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
//                alert('Beneficiário adicionado com sucesso!');
//                $('#modalBeneficiario').modal('hide');
//                location.reload(); // Exemplo para recarregar a página após a inserção
//            } else {
//                alert(response.mensagem);
//            }
//        }
//    });
//}

//$(document).ready(function () {
//    // Outras funcionalidades que precisem do document ready
//});
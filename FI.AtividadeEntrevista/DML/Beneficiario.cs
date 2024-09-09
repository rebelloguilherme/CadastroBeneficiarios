namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de beneficiario que representa o registo na tabela Beneficiario do Banco de Dados
    /// </summary>
    public class Beneficiario
    {
        public long Id { get; set; }
        public long IdCliente { get; set; }
        public string CPF { get; set;}
        public string Nome { get; set; }
    }
}

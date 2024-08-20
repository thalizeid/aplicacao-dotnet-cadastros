namespace sistemaDivtech.Models;

public class ClienteModel
{
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; } = string.Empty; 
    public byte[]? Foto { get; set; }
    public int FornecedorId { get; set; }
    public FornecedorModel? Fornecedor { get; set; }

}

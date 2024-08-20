using Microsoft.AspNetCore.Http;
using sistemaDivtech.Models;
using System.ComponentModel.DataAnnotations;


namespace sistemaDivtech.ViewModels
{
    public class ClienteCreateViewModel
    {
        public int ClienteId { get; set; }
        [Display(Name = "Nome")]
        public string ClienteNome { get; set; } = string.Empty;
        
        public IFormFile? Foto { get; set; } // Para upload de novos arquivos
        public byte[]? FotoBytes { get; set; } // Para armazenar a foto enviada
        [Display(Name = "Fornecedor")]
        public int FornecedorId { get; set; }
        public FornecedorModel? Fornecedor { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace sistemaDivtech.ViewModels
{
    public class FornecedorCreateViewModel
    {
        public int FornecedorId { get; set; }

        
        [Required(ErrorMessage = "O nome do fornecedor é obrigatório!")]
        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage= "O nome pode conter até 100 caracteres.")]
        public string? FornecedorNome { get; set; }


        [Required(ErrorMessage = "O CNPJ do fornecedor é obrigatório!")]
        [Display(Name = "CNPJ")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter exatamente 14 dígitos.")]
        public string? Cnpj { get; set; }


        [Required(ErrorMessage = "O CEP do fornecedor é obrigatório!")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter exatamente 8 dígitos.")]
        [Display(Name = "CEP")]
        public string? Cep { get; set; }
    

        [Display(Name = "Endereço")]
        [StringLength(255, ErrorMessage= "O nome pode conter até 255 caracteres.")]
        public string? Endereco { get; set; }


        // Esta propriedade será usada para armazenar o segmento selecionado
        [Display(Name = "Segmento")]
        public string? SegmentoSelecionado { get; set; }

        // Esta propriedade será usada para popular a lista de segmentos na View
        public SelectList Segmento { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());


    }
}

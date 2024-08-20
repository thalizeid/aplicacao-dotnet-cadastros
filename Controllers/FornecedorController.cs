using Microsoft.AspNetCore.Mvc;
using sistemaDivtech.Models;
using sistemaDivtech.ViewModels;
using sistemaDivtech.Data.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace sistemaDivtech.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly DatabaseContext _context;

        public FornecedorController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var fornecedores = _context.Fornecedores
                .Select(f => new FornecedorCreateViewModel
                {
                    FornecedorId = f.FornecedorId,
                    FornecedorNome = f.FornecedorNome,
                    Cnpj = f.Cnpj,
                    Cep = f.Cep,
                    Endereco = f.Endereco,
                    SegmentoSelecionado = f.Segmento
                }).ToList();
            
            return View(fornecedores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var segmentos = new List<string>
            {
                "Comércio",
                "Serviço",
                "Indústria"
            };

            var viewModel = new FornecedorCreateViewModel
            {
                Segmento = new SelectList(segmentos)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(FornecedorCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var segmentos = new List<string>
                {
                    "Comércio",
                    "Serviço",
                    "Indústria"
                };
                viewModel.Segmento = new SelectList(segmentos);
                return View(viewModel);
            }

            var fornecedorModel = new FornecedorModel
            {
                FornecedorId = viewModel.FornecedorId,
                FornecedorNome = viewModel.FornecedorNome,
                Cnpj = viewModel.Cnpj,
                Segmento = viewModel.SegmentoSelecionado,
                Cep = viewModel.Cep,
                Endereco = viewModel.Endereco
            };

            _context.Fornecedores.Add(fornecedorModel);
            _context.SaveChanges();

            TempData["mensagemSucesso"] = $"O fornecedor {fornecedorModel.FornecedorNome} foi cadastrado com sucesso";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var fornecedor = _context.Fornecedores
                .Where(f => f.FornecedorId == id)
                .Select(f => new FornecedorCreateViewModel
                {
                    FornecedorId = f.FornecedorId,
                    FornecedorNome = f.FornecedorNome,
                    Cnpj = f.Cnpj,
                    Cep = f.Cep,
                    Endereco = f.Endereco,
                    SegmentoSelecionado = f.Segmento
                })
                .FirstOrDefault();

            if (fornecedor == null)
            {
                return NotFound();
            }

            var segmentos = new List<string>
            {
                "Comércio",
                "Serviço",
                "Indústria"
            };
            fornecedor.Segmento = new SelectList(segmentos);

            return View(fornecedor);
        }

        [HttpPost]
        public IActionResult Edit(FornecedorCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var segmentos = new List<string>
                {
                    "Comércio",
                    "Serviço",
                    "Indústria"
                };
                viewModel.Segmento = new SelectList(segmentos);
                return View(viewModel);
            }

            var fornecedor = _context.Fornecedores.Find(viewModel.FornecedorId);
            if (fornecedor == null)
            {
                return NotFound();
            }

            fornecedor.FornecedorNome = viewModel.FornecedorNome;
            fornecedor.Cnpj = viewModel.Cnpj;
            fornecedor.Segmento = viewModel.SegmentoSelecionado;
            fornecedor.Cep = viewModel.Cep;
            fornecedor.Endereco = viewModel.Endereco;

            _context.Update(fornecedor);
            _context.SaveChanges();

            TempData["mensagemSucesso"] = $"Os dados do fornecedor {fornecedor.FornecedorNome} foram alterados com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var fornecedor = _context.Fornecedores
                .Select(f => new FornecedorCreateViewModel
                {
                    FornecedorId = f.FornecedorId,
                    FornecedorNome = f.FornecedorNome
                })
                .FirstOrDefault(f => f.FornecedorId == id);

            if (fornecedor == null)
            {
                TempData["mensagemSucesso"] = "OPS !!! Fornecedor inexistente.";
                return RedirectToAction(nameof(Index));
            }

            _context.Fornecedores.Remove(new FornecedorModel { FornecedorId = id });
            _context.SaveChanges();
            
            TempData["mensagemSucesso"] = $"Os dados do fornecedor {fornecedor.FornecedorNome} foram removidos com sucesso";
            return RedirectToAction(nameof(Index));
        }
    }
}

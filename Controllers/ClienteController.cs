using Microsoft.AspNetCore.Mvc;
using sistemaDivtech.Models;
using sistemaDivtech.ViewModels;
using sistemaDivtech.Data.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace sistemaDivtech.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DatabaseContext _context;

        public ClienteController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clientes = _context.Clientes
                                   .Include(c => c.Fornecedor)
                                   .Select(c => new ClienteCreateViewModel
                                   {
                                       ClienteId = c.ClienteId,
                                       ClienteNome = c.ClienteNome,
                                       FotoBytes = c.Foto,
                                       FornecedorId = c.FornecedorId,
                                       Fornecedor = c.Fornecedor
                                   })
                                   .ToList();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Fornecedores =
                new SelectList(_context.Fornecedores.ToList(), "FornecedorId", "FornecedorNome");
            return View(new ClienteCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(ClienteCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                byte[]? fotoData = null;

                if (viewModel.Foto != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        viewModel.Foto.CopyTo(memoryStream);
                        fotoData = memoryStream.ToArray();
                    }
                }

                var clienteModel = new ClienteModel
                {
                    ClienteId = viewModel.ClienteId,
                    ClienteNome = viewModel.ClienteNome,
                    Foto = fotoData,
                    FornecedorId = viewModel.FornecedorId
                };

                _context.Clientes.Add(clienteModel);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"O cliente {clienteModel.ClienteNome} foi cadastrado com sucesso";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Fornecedores =
                new SelectList(_context.Fornecedores.ToList(), "FornecedorId", "FornecedorNome", viewModel.FornecedorId);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cliente = _context.Clientes
                                  .Include(c => c.Fornecedor)
                                  .FirstOrDefault(c => c.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            var viewModel = new ClienteCreateViewModel
            {
                ClienteId = cliente.ClienteId,
                ClienteNome = cliente.ClienteNome,
                FotoBytes = cliente.Foto, // Passa os bytes existentes
                FornecedorId = cliente.FornecedorId,
                Fornecedor = cliente.Fornecedor
            };

            ViewBag.Fornecedores =
                new SelectList(_context.Fornecedores.ToList(), "FornecedorId", "FornecedorNome", cliente.FornecedorId);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ClienteCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = _context.Clientes.Find(viewModel.ClienteId);
                if (cliente == null)
                {
                    return NotFound();
                }

                if (viewModel.Foto != null) // Se um novo arquivo foi carregado
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        viewModel.Foto.CopyTo(memoryStream);
                        cliente.Foto = memoryStream.ToArray();
                    }
                }
                else
                {
                    cliente.Foto = viewModel.FotoBytes; // Mantém a foto existente
                }

                cliente.ClienteNome = viewModel.ClienteNome;
                cliente.FornecedorId = viewModel.FornecedorId;

                _context.Update(cliente);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"Os dados do cliente {cliente.ClienteNome} foram alterados com sucesso";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Fornecedores =
                new SelectList(_context.Fornecedores.ToList(), "FornecedorId", "FornecedorNome", viewModel.FornecedorId);
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Detail(int id)
        {
            var cliente = _context.Clientes
                                  .Include(c => c.Fornecedor)
                                  .FirstOrDefault(c => c.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            var viewModel = new ClienteCreateViewModel
            {
                ClienteId = cliente.ClienteId,
                ClienteNome = cliente.ClienteNome,
                FotoBytes = cliente.Foto, // Passa os bytes para exibir a foto
                FornecedorId = cliente.FornecedorId,
                Fornecedor = cliente.Fornecedor
            };

            return View(viewModel);
        }

        // Nova ação para fornecer a imagem
        public IActionResult GetImage(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == id);
            if (cliente?.Foto != null)
            {
                return File(cliente.Foto, "image/jpeg"); // ou "image/png" dependendo do tipo da imagem
            }
            return NotFound(); // ou você pode retornar uma imagem padrão indicando que não há imagem disponível
        }
    }
}

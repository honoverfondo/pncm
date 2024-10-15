using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using pncm.Data;
using pncm.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace pncm.Controllers
{
    
    public class ModeloController : Controller
    {
        readonly private ApplicationBdContext _db;

        public ModeloController(ApplicationBdContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ModeloModel> modelos = _db.Modelo;
            return View(modelos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ModeloModel modelo)
        {
            if (ModelState.IsValid)
            {
                _db.Modelo.Add(modelo);
                _db.SaveChanges();
                TempData["mensagemSucesso"] = "Dados registado com sucesso";

                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ModeloModel modelo = _db.Modelo.FirstOrDefault(x => x.Id == id);

            if (modelo == null)
            {
                return NotFound();
            }
            return View(modelo);
        }


        [HttpPost]
        public IActionResult Editar(ModeloModel modelo)
        {

            if (ModelState.IsValid)
            {
                _db.Modelo.Update(modelo);
                _db.SaveChanges();

                TempData["mensagemSucesso"] = "Dados editado com sucesso";

                return RedirectToAction("Index");
            }
            return View(modelo);
        }



        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ModeloModel modelo = _db.Modelo.FirstOrDefault(x => x.Id == id);

            if (modelo == null)
            {
                return NotFound();
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult Eliminar(ModeloModel modelo)
        {
            if (modelo == null)
            {
                return NotFound();
            }
            _db.Remove(modelo);
            _db.SaveChanges();

            TempData["mensagemSucesso"] = "Dados Eliminados com sucesso";

            return RedirectToAction("Index");

        }


        public IActionResult Exportar()
        {
            var dados = GetDados();
            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Modelo");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vmd.openxmlformat.spredsheetml.sheet", "Marca.xls");
                }
            }

        }

        private DataTable GetDados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Modelo", typeof(string));
            dt.Columns.Add("Data", typeof(string));

            var dados = _db.Modelo.ToList();
            if (dados.Count > 0)
            {
                dados.ForEach(modelo =>
                dt.Rows.Add(modelo.Nome, modelo.DataReg));
            }
            return dt;
            ;
        }

        public IActionResult Detalhes()
        {
            return View();
        }

    }
}

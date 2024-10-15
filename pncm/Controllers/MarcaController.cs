using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using pncm.Data;
using pncm.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace pncm.Controllers
{
    public class MarcaController : Controller
    {
        readonly private ApplicationBdContext _db;

        public MarcaController(ApplicationBdContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<MarcaModel>marca=_db.Marca;
            return View(marca);
        }
        [HttpGet]
        public IActionResult Cadastrar() 
        { 
        
            return View();  
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id==0) 
            { 
                return NotFound();  
            }

            MarcaModel marca = _db.Marca.FirstOrDefault(x => x.Id==id);

            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }


        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            MarcaModel marca = _db.Marca.FirstOrDefault(x => x.Id == id);

            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [HttpPost]
        public IActionResult Eliminar(MarcaModel marca)
        {
            if (marca==null)
            {
                return NotFound();
            }
            _db.Remove(marca);
            _db.SaveChanges();

            TempData["mensagemSucesso"] = "Dados Eliminados com sucesso";

            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Cadastrar(MarcaModel marca)
        {
            if (ModelState.IsValid)
            {
                _db.Marca.Add(marca);
                _db.SaveChanges();
                TempData["mensagemSucesso"] = "Dados registado com sucesso";

                return RedirectToAction("Index");
            }
            return View();
            
        }

        [HttpPost]
        public IActionResult Editar(MarcaModel marca) 
        {

            if (ModelState.IsValid)
            {
                _db.Marca.Update(marca);
                _db.SaveChanges();

                TempData["mensagemSucesso"] = "Dados editado com sucesso";

                return RedirectToAction("Index");
            }
            return View(marca);
        }

        public IActionResult Exportar()
        {
            var dados = GetDados();
            using (XLWorkbook workbook= new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Marca");

                using (MemoryStream ms=new MemoryStream())
                {
                    workbook.SaveAs(ms);    
                    return File(ms.ToArray(),"application/vmd.openxmlformat.spredsheetml.sheet","Marca.xls");
                }
            }
              
        }

        private DataTable GetDados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Marca",typeof(string));
            dt.Columns.Add("Data",typeof(string));

            var dados=_db.Marca.ToList();
            if (dados.Count > 0) {
                dados.ForEach(marca =>
                dt.Rows.Add(marca.Descricao, marca.DataCadastro));
            }
            return dt;  
;        }

        public IActionResult Detalhes()
        {
            return View();
        }
    }
}

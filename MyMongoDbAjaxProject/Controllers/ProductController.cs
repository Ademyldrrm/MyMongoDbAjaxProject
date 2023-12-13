using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMongoDbAjaxProject.DAL.Entities;
using MyMongoDbAjaxProject.DAL.Settings;

namespace MyMongoDbAjaxProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMongoCollection<Product> _productsCollection;
        public ProductController(IDataBaseSettings _dataBaseSettings)
        {
            var client=new MongoClient(_dataBaseSettings.ConnectionStrings);
            var database = client.GetDatabase(_dataBaseSettings.DatabaseName);
            _productsCollection = database.GetCollection<Product>(_dataBaseSettings.ProductCollectionName);
        }
       
        public async Task<IActionResult> Index()
        {
            var values = await _productsCollection.Find(x => true).ToListAsync();
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productsCollection.InsertOneAsync(product);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productsCollection.DeleteOneAsync(x => x.ProductID == id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> UpdateProduct(string id)
        {
            var values = await _productsCollection.Find(x => x.ProductID == id).FirstOrDefaultAsync();
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await _productsCollection.FindOneAndReplaceAsync(x => x.ProductID == product.ProductID, product);
            return RedirectToAction("Index");
        }
    }
}

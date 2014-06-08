using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebStore.Controllers
{
    public class ProductsController : Controller
    {
        private WebStoreDbContext db = new WebStoreDbContext();

        // GET: Product
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int?id)
        {
            // add parameters
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ? "product_name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.SearchString = String.IsNullOrEmpty(searchString) ? "" : searchString;
            ViewBag.Category = id;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            // get products and categories to which they belong
            var products = db.Products.Include(p => p.Category);

            // get categories
            var categories = db.Categories;

            // search by category
            if (id != null)
            {
                products = products.Where(p => p.CategoryID == id.Value);
            }

            // search by product name and product description
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.ToUpper().Contains(searchString.ToUpper()) 
                    || p.FullDescription.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "product_name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    products = products.OrderBy(p => p.UnitPrice);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    // no query string -> default ascending order by product name
                    products = products.OrderBy(p => p.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var tuple = new Tuple<IEnumerable<Category>, PagedList.IPagedList<Product>>(categories.ToList(), products.ToPagedList(pageNumber, pageSize));
            return View(tuple);
        }

        // GET: Product/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        [Authorize(Roles = "canEditProducts")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditProducts")]
        public ActionResult Create([Bind(Include = "ID,ProductName,ShortDescription,FullDescription,UnitPrice,CategoryID")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                // is image passed?
                if (image != null)
                {
                    uploadAzureBlobStorage(product, image);
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "canEditProducts")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditProducts")]
        public ActionResult Edit([Bind(Include = "ID,ProductName,ShortDescription,ImageURL, FullDescription,UnitPrice,CategoryID")] Product product, HttpPostedFileBase image)
        {
            // is image passed?
            if (image != null)
            {
                uploadAzureBlobStorage(product, image);
            }

            // keep the current image
            else
            {
                Product productDB = db.Products.Find(product.ID);
                product.ImageURL = productDB.ImageURL;
                db.Entry(productDB).State = EntityState.Detached;
            }

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "canEditProducts")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEditProducts")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static void uploadAzureBlobStorage(Product product, HttpPostedFileBase image)
        {
            // get connection string for azure cloud storage
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // a client to storage access
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            // Create the container if it doesn't already exist.
            if (!container.Exists())
            {
                container.CreateIfNotExists();

                // set container to public
                container.SetPermissions(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    }
                );
            }

            // Retrieve reference to a blob named after product name.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(image.FileName);
            blockBlob.Properties.ContentType = image.ContentType;

            var data = new byte[image.ContentLength];
            image.InputStream.Read(data, 0, image.ContentLength);

            // Create or overwrite the product name blob with contents from a local file.
            blockBlob.UploadFromByteArray(data, 0, image.ContentLength);

            // set uri to product
            product.ImageURL = blockBlob.Uri.AbsoluteUri;
        }
    }
}

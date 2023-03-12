using ItAgency6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ItAgency6.Controllers
{
    public class ClientController : Controller
    {
        private readonly ItAgencyContext _context;

        public ClientController(ItAgencyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Client> clients = _context.Clients.ToList();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Details(string id) //не работает, не присылает id
        {
            int temp = Int32.Parse(id);
            Client client = _context.Clients.Where(client => client.Id == temp).FirstOrDefault();
            return View(client.Id);

        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            int temp = Int32.Parse(id);
            Client client = _context.Clients.Where(client => client.Id == temp).FirstOrDefault();
            return View(client);

        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            _context.Attach(client);
            _context.Entry(client).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            int temp = Int32.Parse(id);
            Client client = _context.Clients.Where(client => client.Id == temp).FirstOrDefault();
            return View(client);

        }

        [HttpPost]
        public IActionResult Delete(Client client)
        {
            _context.Attach(client);
            _context.Entry(client).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create()
        {
            Client client = new Client();
            return View(client);

        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            var lastCustomer = _context.Clients
                 .OrderByDescending(client => client.Id)
                 .FirstOrDefault();

            int nextCustomerId;
            if (lastCustomer == null)
            {
                throw new InvalidOperationException("No last ID found in the database.");
            }
            else
            {
                nextCustomerId = lastCustomer.Id + 1;
            }

            return View("Details", nextCustomerId);


        }

    }
}

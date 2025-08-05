using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamblingApp.Models;

namespace GamblingApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly GamblingContext _context;



        public ClientsController(GamblingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(ViewModels model)
        {
         
            var viewModel = new ViewModels
            {
                NameFilter = model.NameFilter,
                CommentFilter = model.CommentFilter,
                MaxBalance = model.MaxBalance,
                MinBalance = model.MinBalance,
                OrderBy = model.OrderBy ?? "Name",
                OrderDescending = model.OrderDescending,
                SelectedClientId = model.SelectedClientId
            };

            
            var clientsQuery = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(viewModel.NameFilter))
            {
                clientsQuery = clientsQuery.Where(c =>
                    c.Name.Contains(viewModel.NameFilter) ||
                    c.Surname.Contains(viewModel.NameFilter));
            }

            if (viewModel.MinBalance.HasValue)
            {
                clientsQuery = clientsQuery.Where(c => c.ClientBalance >= viewModel.MinBalance.Value);
            }

            if (viewModel.MaxBalance.HasValue)
            {
                clientsQuery = clientsQuery.Where(c => c.ClientBalance <= viewModel.MaxBalance.Value);
            }

            
            clientsQuery = viewModel.OrderBy switch
            {
                "Name" => viewModel.OrderDescending ?
                    clientsQuery.OrderByDescending(c => c.Name) :
                    clientsQuery.OrderBy(c => c.Name),
                "Surname" => viewModel.OrderDescending ?
                    clientsQuery.OrderByDescending(c => c.Surname) :
                    clientsQuery.OrderBy(c => c.Surname),
                "Balance" => viewModel.OrderDescending ?
                    clientsQuery.OrderByDescending(c => c.ClientBalance) :
                    clientsQuery.OrderBy(c => c.ClientBalance),
                _ => clientsQuery.OrderBy(c => c.Name)
            };

            viewModel.Clients = await clientsQuery.ToListAsync();

            
            if (viewModel.SelectedClientId.HasValue)
            {
                viewModel.SelectedClient = await _context.Clients
                    .FirstOrDefaultAsync(c => c.ClientId == viewModel.SelectedClientId.Value);

                var transactionsQuery = _context.Transactions
                    .Include(t => t.TransactionType)
                    .Where(t => t.ClientId == viewModel.SelectedClientId.Value);

                if (!string.IsNullOrEmpty(viewModel.CommentFilter))
                {
                    transactionsQuery = transactionsQuery.Where(t =>
                        t.Comment != null && t.Comment.Contains(viewModel.CommentFilter));
                }

                viewModel.Transactions = await transactionsQuery
                    .OrderByDescending(t => t.TransactionId)
                    .ToListAsync();
            }

            
            viewModel.TransactionTypes = await _context.TransactionTypes.ToListAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(long transactionId, string comment)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                transaction.Comment = comment;
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(ViewModels model)
        {
            if (ModelState.IsValid)
            {
                var client = await _context.Clients.FindAsync(model.NewTransaction.Client);
                if (client != null)
                {
                    
                    _context.Transactions.Add(model.NewTransaction);

                    
                    if (model.NewTransaction.TransactionTypeId == 1)
                    {
                        client.ClientBalance += model.NewTransaction.Amount;
                    }
                    else 
                    {
                        client.ClientBalance += model.NewTransaction.Amount; 
                    }

                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Transaction added successfully!";
                    return RedirectToAction("Index", new { selectedClientId = model.NewTransaction.ClientId });
                }
            }

            TempData["Error"] = "Failed to add transaction. Please check your input.";
            return RedirectToAction("Index", new { selectedClientId = model.NewTransaction.ClientId});
        }
    }
}


        //    public ClientsController(GamblingContext context)
        //    {
        //        _context = context;
        //    }

        //    // GET: Clients
        //    public async Task<IActionResult> Index()
        //    {
        //        return View(await _context.Clients.ToListAsync());
        //    }

        //    // GET: Clients/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var client = await _context.Clients
        //            .FirstOrDefaultAsync(m => m.ClientId == id);
        //        if (client == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(client);
        //    }

        //    // GET: Clients/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Clients/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("ClientId,Name,Surname,ClientBalance")] Client client)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(client);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(client);
        //    }

        //    // GET: Clients/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var client = await _context.Clients.FindAsync(id);
        //        if (client == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(client);
        //    }

        //    // POST: Clients/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,Surname,ClientBalance")] Client client)
        //    {
        //        if (id != client.ClientId)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(client);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ClientExists(client.ClientId))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(client);
        //    }

        //    // GET: Clients/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var client = await _context.Clients
        //            .FirstOrDefaultAsync(m => m.ClientId == id);
        //        if (client == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(client);
        //    }

        //    // POST: Clients/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var client = await _context.Clients.FindAsync(id);
        //        if (client != null)
        //        {
        //            _context.Clients.Remove(client);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ClientExists(int id)
        //    {
        //        return _context.Clients.Any(e => e.ClientId == id);
        //    }
        //}
    
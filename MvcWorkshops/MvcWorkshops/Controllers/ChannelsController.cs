using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWorkshops.Data;
using MvcWorkshops.Helpers;
using MvcWorkshops.Models;

namespace MvcWorkshops.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly ApplicationDbContext _context;

	    private readonly UserManager<ApplicationUser> _userManager;


		public ChannelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
	        _userManager = userManager;

        }

        // GET: Channels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Channels
	            .Include(c => c.CreatedBy)
	            .Include(q => q.ObservedChannels).ThenInclude(q => q.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Channels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Channel channel)
        {
	        var username = HttpContext.User.Identity.Name;
	        ApplicationUser user = await _userManager.FindByEmailAsync(username);

	        channel.CreatedById = user.Id;
	        channel.CreatedAt = DateTime.Now;
	        channel.IsDefault = false;
	        channel.ChannelColor = ColorHelper.GetColor();

	        if (ModelState.IsValid)
            {
                _context.Add(channel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(channel);
        }

        // GET: Channels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var channel = await _context.Channels
                .Include(c => c.CreatedBy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (channel == null)
            {
                return NotFound();
            }

            return View(channel);
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var channel = await _context.Channels.SingleOrDefaultAsync(m => m.Id == id);
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> Observe(int id)
		{
			var username = HttpContext.User.Identity.Name;
			ApplicationUser user = await _userManager.FindByEmailAsync(username);

			var channel = _context.Channels
				.Include(q => q.ObservedChannels)
				.Single(q => q.Id == id);

			channel.ObservedChannels.Add(new ObservedChannel
			{
				ApplicationUserId = user.Id
			});

			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

	    public async Task<IActionResult> StopObserving(int id)
	    {
		    var username = HttpContext.User.Identity.Name;
		    ApplicationUser user = await _userManager.FindByEmailAsync(username);

		    var channel = _context.Channels
			    .Include(q => q.ObservedChannels)
			    .Include(q => q.CreatedBy)
			    .Single(q => q.Id == id && (!q.IsDefault || q.CreatedById != user.Id));

		    var observedChannel = channel.ObservedChannels.Single(q => q.ApplicationUserId == user.Id);
		    channel.ObservedChannels.Remove(observedChannel);

		    await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		private bool ChannelExists(int id)
        {
            return _context.Channels.Any(e => e.Id == id);
        }
    }
}

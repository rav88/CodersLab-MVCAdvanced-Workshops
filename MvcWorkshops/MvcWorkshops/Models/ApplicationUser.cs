using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MvcWorkshops.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
		public ICollection<ObservedChannel> ObservedChannels { get; set; }

		public ICollection<Message> Messages { get; set; }

		public ICollection<Channel> Channels { get; set; }
    }
}

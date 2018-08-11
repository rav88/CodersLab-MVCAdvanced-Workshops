using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWorkshops.Models
{
    public class ObservedChannel
    {
	    public int ChannelId { get; set; }

		[ForeignKey("ChannelId")]
		public Channel Channel { get; set; }

		public string ApplicationUserId { get; set; }

		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }
    }
}

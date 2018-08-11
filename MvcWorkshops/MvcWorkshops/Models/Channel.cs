using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MvcWorkshops.Models
{
    public class Channel
    {
	    public int Id { get; set; }
		
	    public string CreatedById { get; set; }

	    [ForeignKey("CreatedById")]
		public ApplicationUser CreatedBy { get; set; }

	    public bool IsDefault { get; set; }

		[StringLength(6)]
	    public string ChannelColor { get; set; }

	    public DateTime CreatedAt { get; set; }

		public ICollection<Message> Messages { get; set; }

		public ICollection<ObservedChannel> ObservedChannels { get; set; }
    }
}

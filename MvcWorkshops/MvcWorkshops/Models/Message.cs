using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MvcWorkshops.Models
{
    public class Message
    {
		public int Id { get; set; }

		public string CreatedById { get; set; }

		[ForeignKey("CreatedById")]
		public ApplicationUser CreatedBy { get; set; }

		public DateTime CreatedAt { get; set; }

		public string Text { get; set; }

		public int ChannelId { get; set; }

		[ForeignKey("ChannelId")]
		public Channel Channel { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines order id interface.
    /// </summary>
    public interface IOrderId
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("OrderId")]
        [Display(Name = "OrderId")]
        Id<IOrderId> Id { get; set; }
    }
}
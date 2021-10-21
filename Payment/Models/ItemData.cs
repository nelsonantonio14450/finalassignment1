using System;
using System.ComponentModel.DataAnnotations;


namespace TodoApp.Models
{
    public class ItemData
    {
        [Key]
        public int paymentDetailId { get; set; }
        public string cardOwnerName { get; set; }
        public string cardNumber { get; set; }
        public DateTime expirationDate { get; set; }
        public DateTime paidAt { get; set; }
        public string securityCode { get; set; }
        public int Amount { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CryptoExchange.Models
{
    public class Crypto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        [Range(1000000, 1000000000, ErrorMessage = "Supply must be between 1,000,000 and 1,000,000,000")]
        public int Supply { get; set; }
        public string Image { get; set; }
    }
}
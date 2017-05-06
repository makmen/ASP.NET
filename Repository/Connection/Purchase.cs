using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        // имя и фамилия покупателя
        public string Person { get; set; }
        // адрес покупателя
        public string Address { get; set; }
        // ID книги
        public int BookId { get; set; }
        // дата покупки
        public DateTime Date { get; set; }
    }
}

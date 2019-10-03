using Payment.Domain.Repositories;
using Payment.Infrastructure.Contexts;
using System;
using System.Linq;
using Payment.Domain.Entities;
using System.Collections.Generic;

namespace Payment.Infrastructure.Repositories
{
    public class BuyerRespository : IBuyerRepository
    {
        private readonly PaymentContext _context;
        private static List<Buyer> _buyers = new List<Buyer>();
        public BuyerRespository(PaymentContext context)
        {
            _context = context;            
        }

        public bool DocumentExists(string document)
        {
            return _buyers.Any(x => x.Document.Number == document);
            //return _context.Buyers.Any(x => x.Document.Number == document);
        }

        public Buyer GetByCpf(string cpf)
        {
            return _buyers.FirstOrDefault(x => x.Document.Number.Equals(cpf));

            //return _context
            //    .Buyers
            //    //.Include(x => x.User)
            //    .FirstOrDefault(x => x.Document.Number.Equals(cpf));
        }

        public void Save(Buyer buyer)
        {
            _buyers.Add(buyer);
            //_context.Buyers.Add(buyer);
        }
    }
}

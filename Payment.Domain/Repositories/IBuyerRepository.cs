using Payment.Domain.Entities;

namespace Payment.Domain.Repositories
{
    public interface IBuyerRepository
    {        
        Buyer GetByCpf(string cpf);
        bool DocumentExists(string document);
        void Save(Buyer buyer);
    }
}

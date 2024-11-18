using Microsoft.EntityFrameworkCore;
using WebTechnologies.Core;

namespace WebTechnologies.Data
{
    public class DealerRepository : IDealerRepository
    {
        private readonly DataContext _context;

        public DealerRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Dealer dealer)
        {
            _context.Dealers.Add(dealer);
        }

        public void Delete(int id)
        {
            var car = _context.Dealers.Find(id);
            if (car != null)
                _context.Dealers.Remove(car);
        }

        public Dealer? GetDealer(int id)
        {
            return _context.Dealers.Include(e => e.Cars).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Dealer> GetDealers()
        {
            return _context.Dealers.Include(e => e.Cars);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Dealer dealer)
        {
            _context.Entry(dealer).State = EntityState.Modified;
        }
    }
}

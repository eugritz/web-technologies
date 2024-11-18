using WebTechnologies.Core;

namespace WebTechnologies.Data
{
    public interface IDealerRepository
    {
        public IEnumerable<Dealer> GetDealers();
        public Dealer? GetDealer(int id);
        public void Create(Dealer dealer);
        public void Update(Dealer dealer);
        public void Delete(int id);
        public void Save();
    }
}

namespace Stefanini.Domain.CityAggregate
{
    public interface ICityRepository : IBaseRepository<CityDomain>
    {
        Task<IEnumerable<CityDomain>> GetAllCities();
        Task<CityDomain> GetCityById(int id);
        Task<CityDomain> AddCity(CityDomain city);
    }
}

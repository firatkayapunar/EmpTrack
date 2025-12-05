using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;

namespace EmpTrack.Infrastructure.Persistence.Repositories
{
    public class TitleRepository : GenericRepository<Title>, ITitleRepository
    {
        public TitleRepository(ApplicationDbContext context) : base(context)
        { }
    }
}

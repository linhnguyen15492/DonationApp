using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}

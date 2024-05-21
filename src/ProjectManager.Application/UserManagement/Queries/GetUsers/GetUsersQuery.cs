using MediatR;
using ProjectManager.Application.DTOs.User;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        //public string SearchTerm { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IAppDbContext _context;
        public GetUsersQueryHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CreatedDate = user.Created,
                    RoleId = user.RoleId,
                    IsEnabled = user.IsEnabled,
                };
                userDtos.Add(userDto);
            }

            return userDtos;
            //var usersQuery = _context.Users.AsQueryable();

            //// Filter
            //var search = request.SearchTerm;
            //if (!string.IsNullOrEmpty(search))
            //{
            //    usersQuery = usersQuery.Where(p => p.UserName.Contains(search)
            //        || p.FirstName.ToLower().Contains(search)
            //        || p.Email.ToLower().Contains(search)
            //        || p.RoleId.ToString().Contains(search)
            //    );
            //}

            //// Sort
            //var sortDirection = request.Parameters?.SortDirection;
            //if (!string.IsNullOrEmpty(sortDirection))
            //{
            //    switch (sortDirection.ToLower())
            //    {
            //        case "asc":
            //            usersQuery = usersQuery.OrderBy(p => p.UserName);
            //            break;
            //        case "desc":
            //            usersQuery = usersQuery.OrderByDescending(p => p.UserName);
            //            break;
            //    }
            //}
            //else
            //{
            //    usersQuery = usersQuery.OrderBy(p => p.UserName);
            //}

            //// Pagination
            //var pageNumber = request.Parameters?.PageNumber ?? 1;
            //var pageSize = request.Parameters?.PageSize ?? 10;
            //var users = await usersQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            //var userDtos = users.Select(user => new UserDto
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    CreatedDate = user.Created,
            //    RoleId = user.RoleId,
            //    IsEnabled = user.IsEnabled,
            //}).ToList();

            //return userDtos;
        }
    }
}

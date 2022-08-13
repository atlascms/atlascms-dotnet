using Atlas.Core.Models;
using Atlas.Core.Models.Collections;
using Atlas.Core.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Core
{
    public interface IAtlasUserClient
    {
        Task ChangeUserPassword(string id, string newPassword, CancellationToken cancellation = default);
        Task<User> GetUser(string id, CancellationToken cancellation = default);
        Task<User<T>> GetUser<T>(string id, CancellationToken cancellation = default) where T : class;
        Task<PagedList<User>> GetUsers(UserQuery query, CancellationToken cancellation = default);
        Task<PagedList<User<T>>> GetUsers<T>(UserQuery query, CancellationToken cancellation = default) where T : class;
        Task<string> RegisterUser(RegisterUser user, CancellationToken cancellation = default);
        Task<string> RegisterUser<T>(RegisterUser<T> user, CancellationToken cancellation = default) where T : class;
        Task UpdateUser(User user, CancellationToken cancellation = default);
        Task UpdateUser<T>(User<T> user, CancellationToken cancellation = default) where T : class;
        Task DeleteUser(string id, CancellationToken cancellation = default);

        Task<List<Role>> GetRoles(CancellationToken cancellation = default);
        Task<string> CreateRole(Role role, CancellationToken cancellation = default);
        Task UpdateRole(Role role, CancellationToken cancellation = default);
        Task DeleteRole(string id, CancellationToken cancellation = default);
    }
}

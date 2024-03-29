﻿using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository {
    public class RoleRepository: RepositoryBase<long, Role>, IRoleRepository {
        private readonly AccountContext _context;

        public RoleRepository (AccountContext context) : base(context) {
            _context = context;
        }

        public EditRole GetDetails (long id) {
            var role= _context.Roles.Select(x => new EditRole {
                Name = x.Name,
                Id = x.Id,
                MappedPermissions = MapPermissions(x.Permissions),
            }).AsNoTracking().FirstOrDefault(x => x.Id == id)!;
            role.Permissions = role.MappedPermissions!.Select(x => x.Code).ToList();
            return role;
        }

        private static List<PermissionDto> MapPermissions(List<Permission> permissions) {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }
    }
}

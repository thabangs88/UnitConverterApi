using Microsoft.EntityFrameworkCore;
using System;
using UnitConverterAPI.Context;
using UnitConverterAPI.Model.DbContext;

namespace UnitConverterAPI.Interface
{
    public interface IAuthenticationDbContextService
    {
        DbSet<UserModel> User { get; set; }
        DbSet<AppModel> Apps { get; set; }
        DbSet<CompanyModel> Companies { get; set; }
    }
}

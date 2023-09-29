using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthApi.Models{
    [CollectionName("roles")]
    public class Role:MongoIdentityRole<Guid>{}
}
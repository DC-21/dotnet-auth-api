using System;
using AspNetCore.Identity.MongoDbCore;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthApi.Models
{
    [CollectionName("role")]
    public class Role: MongoIdentityRole<Guid>{}
}
using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthApi{
    [CollectionName("appusers")]
    public class User: MongoIdentityUser<Guid>{
        public String FullName {get; set;} = String.Empty;
    }
}
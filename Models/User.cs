using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace AuthApi{
    [CollectionName("appuser")]
    public class User : MongoIdentityUser<Guid>{
        public string Fullname {get;set;} = string.Empty;
    }
}
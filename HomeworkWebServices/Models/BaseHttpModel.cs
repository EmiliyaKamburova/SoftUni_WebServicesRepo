using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace HomeworkWebServices.Models

{  
    public static class JsonExtensions
    {
        public static string ToJson(this Household self) => JsonConvert.SerializeObject(self, Household.Converter.Settings);

        public static string ToJson(this User self) => JsonConvert.SerializeObject(self, User.Converter.Settings);

        public static string ToJson(this Book self) => JsonConvert.SerializeObject(self, Book.Converter.Settings);
        public static string ToJson(this Wishlist self) => JsonConvert.SerializeObject(self, Wishlist.Converter.Settings);

    }
}



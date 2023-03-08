using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Handlers.Helpers
{
    public static class Converter
    {
        public static string computeHash(this string key)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                if (key == null) return null;
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(key));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static List<PropertyInfo> GetPropertyInfo(object l)
        {
            var x = l.GetType().GetProperties()
                .Where(p =>
                {
                    var val = p.GetValue(l);
                    if (val == null) return false;
                    var defaultVal = !val.GetType().IsDefaultValue(val);
                    return defaultVal;
                }).ToList();
            return x;
        }

        public static JsonPatchDocument patchDocument(this object l)
        {
            JsonPatchDocument body = new JsonPatchDocument();
            GetPropertyInfo(l).ForEach(p => {
                body.Replace(p.Name, p.GetValue(l));
            });
            return body;
        }
    }
}

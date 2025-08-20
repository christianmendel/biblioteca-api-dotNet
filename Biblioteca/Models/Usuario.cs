using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; private set; } = string.Empty;
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string SenhaHash { get; private set; } = string.Empty;

        public Usuario SetSenha(string hash)
        {
            SenhaHash = hash;
            Senha = null;
            return this;
        }


        public static string Hash(string input)
        {
            using var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}

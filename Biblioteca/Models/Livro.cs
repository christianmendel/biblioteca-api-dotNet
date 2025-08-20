using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Biblioteca.Models
{
    public class Livro
    {
        public Livro(string nome, string descricao, TipoLivro tipo)
        {
            Nome = nome;
            Descricao = descricao;
            Tipo = tipo;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoLivro Tipo { get; set; }
        public string UserId { get; set; }

        public Livro SetUser(string userId)
        {
            UserId = userId;
            return this;
        }


    }
}

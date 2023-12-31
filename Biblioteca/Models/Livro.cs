using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

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

        public TipoLivro Tipo;
       
    }
}

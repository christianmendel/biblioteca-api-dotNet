using Biblioteca.Settings.Validations;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dto.Livro
{
    public class LivroResponse: Notifiable
    {
        public string _id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Tipo { get; set; }

    }

}

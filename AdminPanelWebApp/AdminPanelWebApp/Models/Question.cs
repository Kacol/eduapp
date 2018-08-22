using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanelWebApp.Models
{   
    public class Question
    {
        //zza a EnittyFramework
        [Key]        // adnotacja jak w javie @Overide
        public int ID { get;set; }

        [Required(ErrorMessage = "Podaj przedmiot")]
        [Display(Name = "Temat")]
        public Subject Subject { get; set; }

        [Range(1,6,ErrorMessage = "Nieprawidlowa wartosc"),Required(ErrorMessage = "Podaj stopien trudnosci")]
        [Display(Name = "Poziom trudności")]
        public int Difficulty { get; set; } // 1-6

        [Required(ErrorMessage = "Podaj treść")]
        [Display(Name = "Pytanie")]
        public string QuestionContent { get; set; }

        [Required(ErrorMessage = "Podaj odpowiedź")]
        [Display(Name = "Odpowiedź")]
        public string ResponseContent { get; set; }    
    }
   
}

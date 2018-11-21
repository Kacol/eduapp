using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanelWebApp.Models;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdminPanelWebApp.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]   
    public class ExternalController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public ExternalController(DatabaseContext context, IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(context));
            _context = context ?? throw new ArgumentNullException(nameof(context)); //??- jesli context bez pokr. nie jest null to zostanie przypisany do lewej strony;
                                                                                    //jesli będzie null to zostanie rzucony wyjatek
                                                                                       //nameof-zwraca nazwe parametru w nawiasie
        }
        // aby wystawic daną metode bedziemy podawac api/[controller]/[action] gdzie controler to external a action to nazwa metody
        [HttpGet]
        [AllowAnonymous]
        public int GetTimeoutSetting()
        {
            return _configuration.GetValue<int>("AdminPanelSettings:Timeout");
        }
        [HttpGet]
        public int GetWrongAnswerTimeoutSetting()
        {
            return _configuration.GetValue<int>("AdminPanelSettings:WrongAnswerTimeout");
        }

        [HttpGet]
        public GetNextQuestionResult GetNextQuestion()
        {            
            var questions = _context.Questions
                .Where(question => question.IsEnabled)
                .Select(question => new {question.QuestionContent,question.ID})
                .ToList();
            //wybieramy select aby wyciagnąć interesujące nas kolumny
            //var "uniwesalny" typ context to baza danych where wybierax=>-takie jak where w sql;
                       
            //tolist wywyla zapytanie do serwera i zamienia wymik zapytania do listy w pamieci
            if (questions.Count == 0)                            
                return null;
            
            Random random = new Random();
            int index = random.Next(questions.Count);
            var pair = questions[index];
            return new GetNextQuestionResult(pair.QuestionContent, pair.ID);         
        }

        [HttpGet]
        [Route("{userAnswer}/{questionId}")]
        public bool IsAnswerCorrect(string userAnswer,int questionId)
        {
            String odp = _context.Questions.Where(question => question.ID == questionId)
                .Select(question => question.ResponseContent).Single();

            return userAnswer.ToLower().Trim().Equals(odp.Trim().ToLower());
        }
    }
}
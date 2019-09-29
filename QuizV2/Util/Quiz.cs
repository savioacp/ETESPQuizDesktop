using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace QuizV2.Util
{
    public class Quiz
    {
        public Pergunta Pergunta { get; private set; }
        public Queue<Pergunta> Perguntas { get; private set; }
        public List<EquipeParticipando> Equipes { get; private set; }
        public int Counter { get; private set; } = 1;
        private bool _TopQuiz { get; set; }
        

        public Quiz()
        {
            Perguntas = new Queue<Pergunta>(Data.Cache.Perguntas.Where(eq => !eq.TopQuiz).ToArray().Randomize());
            Equipes = new List<EquipeParticipando>();
            foreach (Equipe eq in Data.Cache.Equipes)
                Equipes.Add(new EquipeParticipando
                {
                    Id = eq.Id,
                    Nome = eq.Nome,
                    Cor = eq.Cor,
                    Integrantes = eq.Integrantes,
                    Pontos = 0,
                    Erros = 0
                });
        }
        
        public ValueTuple<Pergunta, int> NextPergunta()
        {
            try
            {
                var a = new ValueTuple<Pergunta, int>(Perguntas.Dequeue(), Counter++);
                Pergunta = a.Item1;
                return a;
            }
            catch (InvalidOperationException)
            { 
                return new ValueTuple<Pergunta, int>(null, Counter);
            }
        }

        public void FinalizarPergunta(EquipeParticipando[] acertaram)
        {
            if (acertaram.Length == 1)
            {
                foreach (EquipeParticipando e in Equipes)
                    if (e.Id == acertaram[0].Id)
                        e.Acerto(true);
                    else
                        e.Erro();
            }
            else
            {
                foreach(EquipeParticipando e in Equipes)
                    if (acertaram.Select(eq => eq.Id).Contains(e.Id))
                        e.Acerto(false);
                    else
                        e.Erro();
            }
        }

        public void Recuperação(EquipeParticipando[] acertaram)
        {
            foreach (var eq in acertaram.Where(eq => eq.Eliminada))
            {
                eq.Repescou();
            }
        }

        public void TopQuiz()
        {
            Perguntas = new Queue<Pergunta>(Data.Cache.Perguntas.Where(eq => eq.TopQuiz).ToArray().Randomize());
            Equipes = new List<EquipeParticipando>(Equipes.Where(eq => !eq.Eliminada));
            _TopQuiz = true;
        }

        public EquipeParticipando[] GetParcialRanking()
        {
            return Equipes.OrderByDescending(e => e.Pontos).ToArray();
        }
    }
}

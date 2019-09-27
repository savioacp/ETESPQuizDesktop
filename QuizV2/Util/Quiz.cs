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
        public Queue<Pergunta> Perguntas { get; private set; }
        public List<EquipeParticipando> Equipes { get; private set; }
        public int Counter { get; private set; } = 1;
        
        public Quiz(Pergunta[] perguntas, Equipe[] equipes)
        {
            Perguntas = new Queue<Pergunta>(perguntas.Randomize());
            Equipes = new List<EquipeParticipando>();
            foreach (Equipe eq in equipes)
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
                return new ValueTuple<Pergunta, int>(Perguntas.Dequeue(), Counter++);
            }
            catch (InvalidOperationException)
            { 
                return new ValueTuple<Pergunta, int>(null, Counter);
            }
        }

        public void FinalizarPergunta(Equipe[] acertaram)
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
                    foreach (Equipe eq in acertaram)
                        if (eq.Id == e.Id)
                            e.Acerto(false);
                        else
                            e.Erro();
            }
        }

        public EquipeParticipando[] GetParcialRanking()
        {
            return Equipes.OrderBy(e => e.Pontos).ToArray();
        }
    }
}

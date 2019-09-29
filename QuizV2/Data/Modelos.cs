using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizV2
{
    public class Equipe
    {
        public int Id;
        public string Nome;
        public string Cor;
        public string[] Integrantes;
    }

    public class Pergunta
    {
        public int Id;
        public string Texto;
        public bool TopQuiz;
		public bool TemImagem
        {
            get
            {
                return Imagem != null;
            }
        }
        public byte[] Imagem;
        public string Correta = null;
        public string[] Respostas;

		public bool Dissertativa
		{
			get
			{
				return Respostas.Length == 1;
			}
		}
	}

    public class EquipeParticipando : Equipe
    {
        public int Erros;
        public int Pontos;
        public bool Eliminada => Erros >= Data.Cache.ErrosEliminantes;

        public void Acerto(bool sozinha)
        {
            if (sozinha)
                Pontos += 2;
            else
                Pontos++;
        }

        public void Erro()
        {
            Erros++;
        }

        public void Repescou()
        {
            Erros = 0;
        }
    }

    

}

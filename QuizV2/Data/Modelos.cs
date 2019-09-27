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
        public bool Eliminada
        {
            get
            {
                return Erros > Data.Cache.ErrosEliminantes;
            }
            set
            {
                if (value) Erros = Data.Cache.ErrosEliminantes;
            }
        }
        public void Acerto(bool sozinha)
        {
            Erros = sozinha ? Erros + 1 : Erros + 2;
        }

        public void Erro()
        {
            Erros++;
        }
    }

    

}

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
        public int Pontuação;
    }

    public class Pergunta
    {
        public int Id;
        public string Texto;
        public bool TopQuiz;
		public bool TemImagem;
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

    

}

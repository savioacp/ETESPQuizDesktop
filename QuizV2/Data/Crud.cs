using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizV2.Util;

namespace QuizV2.Data
{
    public static class DataManager
    {
		static SqlCommand cmd;
		static Conexao conexao = new Conexao();

        public static void AddUsuario(string Nome, string Senha)
        {
			cmd = new SqlCommand
			{
				CommandText = "insert tblUsuario values (@Nome, @Senha)"
			};
			cmd.Parameters.AddWithValue("@Nome", Nome);
            cmd.Parameters.AddWithValue("@Senha", Senha);
			cmd.Connection = conexao.Conectar();
			cmd.ExecuteNonQuery();
		}

        public static void AddEquipe(Equipe equipe)
        {
			cmd = new SqlCommand
			{
				CommandText = "insert tblEquipe output INSERTED.IdEquipe values (@Nome, @Cor)"
            };
			cmd.Parameters.AddWithValue("@Nome", equipe.Nome);
            cmd.Parameters.AddWithValue("@Cor", equipe.Cor);
            cmd.Connection = conexao.Conectar();
			equipe.Id = (int)cmd.ExecuteScalar();
            foreach(string integrante in equipe.Integrantes)
            {
                AddIntegrante(equipe.Id, integrante);
            }
		}

        public static void AddIntegrante(int Id, string Nome)
        {
			cmd = new SqlCommand
			{
				CommandText = "insert tblIntegrante values (@IdEquipe, @Nome)"
			};
			cmd.Parameters.AddWithValue("@IdEquipe", Id);
            cmd.Parameters.AddWithValue("@Nome", Nome);
			cmd.Connection = conexao.Conectar();
			cmd.ExecuteNonQuery();
		}

        public static void AddPergunta(Pergunta pergunta)
        {
			cmd = new SqlCommand
			{
				CommandText = "insert tblPergunta output INSERTED.IdPergunta values (@Texto, @Imagem, @TopQuiz)"
            };
			cmd.Parameters.AddWithValue("@Texto", pergunta.Texto);
            cmd.Parameters.AddWithValue("@Imagem", Serializa.GetBytesFromImage(pergunta.Imagem));
            cmd.Parameters.Add("@TopQuiz", SqlDbType.Bit).Value = pergunta.TopQuiz;
			cmd.Connection = conexao.Conectar();
			int id = (int)cmd.ExecuteScalar();

            foreach (var resposta in pergunta.Respostas)
            {
                AddResposta(id, resposta, pergunta.Correta == resposta);
            }
        }

        private static void AddResposta(int Id, string Texto, bool Correta)
        {
			cmd = new SqlCommand
			{
				CommandText = "insert tblResposta values (@IdPergunta, @Texto, @Correta)"
			};
			cmd.Parameters.AddWithValue("@IdPergunta", Id);
            cmd.Parameters.AddWithValue("@Texto", Texto);
            cmd.Parameters.AddWithValue("@Correta", Correta);
            cmd.Connection = conexao.Conectar();
            cmd.ExecuteNonQuery();
        }

        public static Equipe[] GetEquipes()
        {
			cmd = new SqlCommand
			{
				CommandText = "select * from tblEquipe"
			};
			List<Equipe> equipes = new List<Equipe>();
            cmd.Connection = conexao.Conectar();
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            foreach(DataRow row in dt.Rows)
            {
                Equipe equipe = new Equipe()
                {
                    Id = int.Parse(row["IdEquipe"].ToString()),
                    Nome = row["NomeEquipe"].ToString(),
                    Cor = row["CorEquipe"].ToString(),
                    Integrantes = GetIntegrantes(int.Parse(row["IdEquipe"].ToString()))
                };
				equipes.Add(equipe);
            }
            return equipes.ToArray();
        }

        private static string[] GetIntegrantes(int Id)
        {
			cmd = new SqlCommand
			{
				CommandText = "select * from tblIntegrante where IdEquipe=@Id"
			};
			cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
			List<string> integrantes = new List<string>();
            cmd.Connection = conexao.Conectar();
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            foreach (DataRow row in dt.Rows)
            {
                integrantes.Add(row["NomeIntegrante"].ToString());
            }
            return integrantes.ToArray();
        }

        public static Pergunta[] GetPerguntas()
        {
			cmd = new SqlCommand
			{
				CommandText = "select * from tblPergunta"
			};
			List<Pergunta> perguntas = new List<Pergunta>();
            cmd.Connection = conexao.Conectar();
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            foreach (DataRow row in dt.Rows)
            {
                string correta = "";
                string[] respostas = GetRespostas(int.Parse(row["IdPergunta"].ToString()), ref correta);
                Pergunta pergunta = new Pergunta()
                {
                    Id = int.Parse(row["IdPergunta"].ToString()),
                    Texto = row["Texto"].ToString(),
                    Imagem = Serializa.GetImageFromBytes(row["Imagem"] as byte[]),
                    Respostas = respostas,
                    Correta = correta,
                    TopQuiz = row["TopQuiz"] as bool? ?? false
                };
                perguntas.Add(pergunta);
            }
            return perguntas.ToArray();
        }

        private static string[] GetRespostas(int Id, ref string correta)
        {
			cmd = new SqlCommand
			{
				CommandText = "select * from tblResposta where IdPergunta=@Id"
			};
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            cmd.Connection = conexao.Conectar();
			List<string> respostas = new List<string>();
            var dr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            foreach (DataRow row in dt.Rows)
            {
                respostas.Add(row["Texto"].ToString());
                if ((bool?)row["Correta"] == true)
                    correta = row["Texto"].ToString();
            }
            return respostas.ToArray();
        }


        public static void DeleteEquipe(int Id)
        {
            DeleteIntegrantes(Id);
            cmd = new SqlCommand
            {
                CommandText = "delete from tblEquipe where IdEquipe=@Id"
            };
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
            cmd.Connection = conexao.Conectar();
            cmd.ExecuteNonQuery();
        }

        private static void DeleteIntegrantes(int IdEquipe)
        {
            cmd = new SqlCommand
            {
                CommandText = "delete from tblIntegrante where IdEquipe=@Id"
            };
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = IdEquipe;
            cmd.Connection = conexao.Conectar();
            cmd.ExecuteNonQuery();
        }

        public static void UpdateEquipe(int IdEquipe, Equipe newEquipe)
        {
            cmd = new SqlCommand
            {
                CommandText = "update tblEquipe set NomeEquipe=@Nome where IdEquipe=@Id"
            };
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = newEquipe.Nome;
			cmd.Parameters.Add("@Id", SqlDbType.Int).Value = IdEquipe;

			cmd.Connection = conexao.Conectar();
            cmd.ExecuteNonQuery();

			DeleteIntegrantes(IdEquipe);
			cmd = new SqlCommand
			{
				CommandText = @"insert tblIntegrante values  (@IdEquipe, @Nome1), " +
															"(@IdEquipe, @Nome2), " +
															"(@IdEquipe, @Nome3), " +
															"(@IdEquipe, @Nome4), " +
															"(@IdEquipe, @Nome5)"
			};
			cmd.Parameters.Add("@IdEquipe", SqlDbType.Int).Value = IdEquipe;
			cmd.Parameters.Add("@Nome1", SqlDbType.VarChar).Value = newEquipe.Integrantes[0];
			cmd.Parameters.Add("@Nome2", SqlDbType.VarChar).Value = newEquipe.Integrantes[1];
			cmd.Parameters.Add("@Nome3", SqlDbType.VarChar).Value = newEquipe.Integrantes[2];
			cmd.Parameters.Add("@Nome4", SqlDbType.VarChar).Value = newEquipe.Integrantes[3];
			cmd.Parameters.Add("@Nome5", SqlDbType.VarChar).Value = newEquipe.Integrantes[4];
			cmd.Connection = conexao.Conectar();
			cmd.ExecuteNonQuery();

        }

    }


}

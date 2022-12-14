using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _211077.Models
{
    internal class Cliente
    {
        public int Id { get; set; }

        public string nome { get; set; }

        public int idCidade { get; set; }

        public DateTime dataNasc { get; set; }

        public double renda { get; set; }

        public string cpf { get; set; }

        public string foto { get; set; }

        public bool venda { get; set; }


        public void Incluir()
        {
            try
            {
                Banco.Conexao.Open();
                Banco.Comando = new MySqlCommand
                    ("INSERT INTO clientes (nome, idCidade, datanasc, renda, cpf, foto, venda) " +
                    "VALUES (@nome, @idCidade, @dataNasc, @renda, @cpf, @foto, @venda)", Banco.Conexao);

                Banco.Comando.Parameters.AddWithValue("@nome", nome);
                Banco.Comando.Parameters.AddWithValue("@idCidade", idCidade);
                Banco.Comando.Parameters.AddWithValue("@dataNasc", dataNasc);
                Banco.Comando.Parameters.AddWithValue("@renda", renda);
                Banco.Comando.Parameters.AddWithValue("@cpf", cpf);
                Banco.Comando.Parameters.AddWithValue("@foto", foto);
                Banco.Comando.Parameters.AddWithValue("@venda", venda);
                Banco.Comando.ExecuteNonQuery();
                Banco.Conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void Alterar()
        {
            try
            {
                Banco.Conexao.Open();
                Banco.Comando = new MySqlCommand
                ("UPDATE clientes SET nome = @nome, idCidade = @idCidade, dataNasc = @dataNasc, renda = @renda, cpf = @cpf, foto = @foto, venda = @venda where id + @id", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@nome", nome);
                Banco.Comando.Parameters.AddWithValue("@idCidade", idCidade);
                Banco.Comando.Parameters.AddWithValue("@dataNasc", dataNasc);
                Banco.Comando.Parameters.AddWithValue("@renda", renda);
                Banco.Comando.Parameters.AddWithValue("@cpf", cpf);
                Banco.Comando.Parameters.AddWithValue("@foto", foto);
                Banco.Comando.Parameters.AddWithValue("@venda", venda);
                Banco.Comando.Parameters.AddWithValue("@id", Id);
                Banco.Comando.ExecuteNonQuery();
                Banco.Conexao.Close();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

                public void Excluir()
                {
                  try
                  {
                    Banco.Conexao.Open();
                    Banco.Comando = new MySqlCommand("delete from clientes whre id = @id", Banco.Conexao);
                    Banco.Comando.Parameters.AddWithValue("@id", Id);
                    Banco.Comando.ExecuteNonQuery();
                    Banco.Conexao.Close();
                  }
                  catch (Exception e)
                  {
                   MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                }


        public DataTable Consultar()
        { 
            try
            {
                Banco.Comando = new MySqlCommand("SELECT cl.*, ci.nome cidade, ci.uf FROM Clientes cl inner join CIdades ci on (ci.id = cl.idCidades) where cl.nome like ?Nome order by cl.nome", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@nome", nome + "%");
                Banco.Adptador = new MySqlDataAdapter(Banco.Comando);
                Banco.Adptador.Fill(Banco.datTabela);
                return Banco.datTabela;

            }
            catch(Exception e)
            {

                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        
        }
            
        
    }
}


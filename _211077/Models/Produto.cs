using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _211077.Models
{
    internal class Produto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int id_categoria { get; set; }
        public int id_marca { get; set; }
        public double estoque { get; set; }
        public double valor_venda { get; set; }
        public string foto { get; set; }

        public void insert()
        {
            try
            {
                Banco.AbrirConexao();

                Banco.Comando = new MySqlCommand("INSERT INTO produtos (descricao, id_categoria, id_marca, estoque, valor_venda, foto) VALUES (@descricao, @id_categoria, @id_marca, @estoque, @valor_venda, @foto)", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.Comando.Parameters.AddWithValue("@id_categoria", id_categoria);
                Banco.Comando.Parameters.AddWithValue("@id_marca", id_marca);
                Banco.Comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.Comando.Parameters.AddWithValue("@valor_venda", valor_venda);
                Banco.Comando.Parameters.AddWithValue("@foto", foto);

                Banco.Comando.ExecuteNonQuery();

                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void update()
        {
            try
            {
                Banco.AbrirConexao();

                Banco.Comando = new MySqlCommand("UPDATE produtos set descricao=@descricao, id_categoria=@id_categoria, id_marca=@id_marca, estoque=@estoque, valor_venda=@valor_venda, foto=@foto WHERE id = @id", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.Comando.Parameters.AddWithValue("@id_categoria", id_categoria);
                Banco.Comando.Parameters.AddWithValue("@id_marca", id_marca);
                Banco.Comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.Comando.Parameters.AddWithValue("@valor_venda", valor_venda);
                Banco.Comando.Parameters.AddWithValue("@foto", foto);
                Banco.Comando.Parameters.AddWithValue("@id", id);

                Banco.Comando.ExecuteNonQuery();

                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void delete()
        {
            try
            {
                Banco.AbrirConexao();

                Banco.Comando = new MySqlCommand("DELETE FROM produtos WHERE id = @id", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@id", id);

                Banco.Comando.ExecuteNonQuery();

                Banco.FecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable consultar()
        {
            try
            {
                Banco.AbrirConexao();

                Banco.Comando = new MySqlCommand("SELECT p.*, m.marca, c.categoria FROM produtos p" +
                                                 " INNER JOIN marcas m on (m.id = p.id_marca) " +
                                                 " INNER JOIN categorias c on (c.id = p.id_categoria) " +
                                                 " WHERE p.descricao like @descricao " +
                                                                                "order by p.id", Banco.Conexao);
                Banco.Comando.Parameters.AddWithValue("@descricao", descricao + "%");

                Banco.Adptador = new MySqlDataAdapter(Banco.Comando);
                Banco.datTabela = new DataTable();
                Banco.Adptador.Fill(Banco.datTabela);

                Banco.FecharConexao();
                return Banco.datTabela;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
}

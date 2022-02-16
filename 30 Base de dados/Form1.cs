using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Import dpo MYSQL
using MySql.Data.MySqlClient;

namespace _30_Base_de_dados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            #region MYSQL

            /*string de conexão serve para linkar o projeto ao bd, na 2, eu ja tenho um banco
             * na 1 eu aidna não tenho. */

            string strconnetion1 = "server=127.0.0.1; User Id=root;password=";
            //string strConnection2 = "server=127.0.0.1; User Id=root;database=curso_db;password=";

            //CRIANDO A CONEXÃO COM O BD

            MySqlConnection conexão = new MySqlConnection(strconnetion1);

            try
            {
                conexão.Open();
                labelResultado.Text = "Conectado MySQL";

                //Fazer comandos sql dentro do c# no meu local host phpmyadmin

                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexão;
                comando.CommandText = "CREATE DATABASE IF NOT EXISTS curso_db";
                comando.ExecuteNonQuery();
                labelResultado.Text = "Base de dados criada com sucesso";

                comando.Dispose(); // limpar o comando dps que ele for executado

            }
            catch (Exception ex)
            {
                labelResultado.Text = "Erro ao conectar MySQL";

            }
            finally
            {
                
                conexão.Close();
            }
            // TESTAR SE O BD EXSTE, CASO NÃO EXISTE, VOU CRIÁ-LA NO SERVIDOR, VOU FAZER ISSO DENTRO DO TRY, NA LINHA 36
                

            #endregion
        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {
            string strConnection = "server=127.0.0.1; User Id=root;database=curso_db;password="; // string de conexao com bd
            MySqlConnection conexao = new MySqlConnection(strConnection);
            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "CREATE TABLE pessoas ( id INT NOT NULL , nome VARCHAR(50), email VARCHAR(60), PRIMARY KEY(id))";
                comando.ExecuteNonQuery();

                labelResultado.Text = "Tabela Criada com sucesso.";
                comando.Dispose();
            }
            catch (Exception ex)
            {

                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {

            string strConnection = "server=127.0.0.1; User Id=root;database=curso_db;password="; // string de conexao com bd
            MySqlConnection conexao = new MySqlConnection(strConnection);

            int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string nome = txtNome.Text;
            string email = txtEmail.Text;

            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "INSERT INTO pessoas VALUES(" + id + ", '" + nome + "','" + email + "')";
                comando.ExecuteNonQuery();

                labelResultado.Text = "registro inserido com sucesso";
                comando.Dispose();
            }
            catch (Exception ex)
            {

                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            string strConnection = "server=127.0.0.1; User Id=root;database=curso_db;password="; // string de conexao com bd
            MySqlConnection conexao = new MySqlConnection(strConnection);

            try
            {
                string query = "SELECT * FROM pessoas";
                if(txtNome.Text != "")
                {
                    query = "SELECT * FROM pessoas WHERE nome LIKE'" + txtNome.Text+"'";
                }

                DataTable dados = new DataTable();

                MySqlDataAdapter adaptador = new MySqlDataAdapter(query, strConnection);

                conexao.Open();
                adaptador.Fill(dados);

                foreach (DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }


            }
            catch (Exception ex)
            {
                lista.Rows.Clear();
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }



        }

        private void btnExcluie_Click(object sender, EventArgs e)
        {
            
            string strConnection = "server=127.0.0.1; User Id=root;database=curso_db;password="; // string de conexao com bd
            MySqlConnection conexao = new MySqlConnection(strConnection);


            
            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;

                int id = (int)lista.SelectedRows[0].Cells[0].Value;

                comando.CommandText = "DELETE FROM pessoas WHERE id = '"+id+"'";// o (int) no inicio foi para realizar uma conversao explicita para int
                comando.ExecuteNonQuery();

                labelResultado.Text = "Registro excluido com sucesso";
                comando.Dispose();
            }
            catch (Exception ex)
            {

                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string strConnection = "server=127.0.0.1; User Id=root;database=curso_db;password="; // string de conexao com bd
            MySqlConnection conexao = new MySqlConnection(strConnection);



            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;

                int id = (int)lista.SelectedRows[0].Cells[0].Value;// o (int) no inicio foi para realizar uma conversao explicita para int
                string nome = txtNome.Text;
                string email = txtEmail.Text;
                string query = "UPDATE pessoas SET nome = '" + nome + "', email = ' " + email + " ' WHERE id LIKE '" + id + "'";



                comando.CommandText = query;
                comando.ExecuteNonQuery();

                labelResultado.Text = "Registro alterado com sucesso";
                comando.Dispose();
            }
            catch (Exception ex)
            {

                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}

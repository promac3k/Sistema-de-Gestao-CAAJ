using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace CAAJ.Utilitarios
{
    internal class Usuario
    {
        internal string Nome { get; set; }
        internal string Password { get; set; }
        internal int Id { get; set; }
        internal int Admin { get; set; }
        internal string Ip { get; set; }
        internal string Data { get; set; }

        //internal List<string> ListaNomes = new List<string>();
        internal Dictionary<int, string> ListaUsuarios = new Dictionary<int, string>();

        private DB ligacao { get; set; }

        internal Usuario(DB ligacao)
        {
            this.ligacao = ligacao;
            BuscarNomes();
        }

        internal void BuscarNomes()
        {
            try
            {
                //ListaNomes.Clear();
                ListaUsuarios.Clear();

                const string query = "SELECT * FROM Login";
                var command = new MySqlCommand(query, ligacao.connection);

                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    var usu = rdr["user_id"].ToString();
                    var id = int.Parse(rdr["id"].ToString());
                    //ListaNomes.Add(usu);
                    try
                    {
                        ListaUsuarios.Add(Id, usu);
                    }
                    catch
                    {

                    }
                }
                command.Dispose();
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal int BuscarUsuario(string usuario, string senha)
        {
            try
            {
                var command = new MySqlCommand("SELECT * FROM Login WHERE Login.user_id='" + usuario + " ' AND Login.password='" + senha + "';", ligacao.connection);
                MySqlDataReader rdr = command.ExecuteReader();

                int resultado = -1; // -1 e usuario errado
                string usu = "";

                while (rdr.Read())
                {
                    usu = rdr["user_id"].ToString();
                    var sen = rdr["password"].ToString();
                    var admin = rdr["admin"].ToString();
                    Console.WriteLine(usu, sen, admin);
                    int ad = int.Parse(admin);
                    if (usu.Equals(usuario) && sen.Equals(senha))
                    {
                        FormLogin.LOG.Text += "logging feito com sucesso...\n";
                        resultado = 0;
                        if (ad == 1)
                        {
                            Program.usuario = false;
                            Program.usuarioAdmin = true;
                            FormLogin.LOG.Text += "Usuario Administrador...\n";
                            resultado = 1;
                        }
                        if (ad == 0)
                        {
                            Program.usuarioAdmin = false;
                            Program.usuario = true;
                            FormLogin.LOG.Text += "Usuario normal...\n";
                            resultado = 0;
                        }
                    }
                }
                // dispose faz uma libertacao de recursos.
                command.Dispose();
                // fechar a ligaçao para se poder usar novamente.
                rdr.Close();
                if (resultado == 0 || resultado == 1)
                {
                    AtualizarIP(usu);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        private bool AtualizarIP(string user)
        {
            try
            {
                var Ip = IpMaquina() + "::" + IpInternet();
                // Mysql dateTime usa um formato de yyyy-MM-dd HH:mm:ss
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var command = new MySqlCommand("UPDATE Login SET last_login='" + date + "', last_ip='" + Ip + "' WHERE user_id='" + user + "';", ligacao.connection);
                int result = command.ExecuteNonQuery();
                command.Dispose();
                BuscarNomes();
                //Console.WriteLine(">>>>>> " + result);
                return result > 0;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        internal bool AtualizarUsuario(int Linha, string Usuario, string Senha, int admin)
        {
            try
            {
                // Mysql dateTime usa um formato de yyyy-MM-dd HH:mm:ss

                var command = new MySqlCommand("UPDATE Login SET user_id='" + Usuario + "', password='" + Senha + "', admin='" + admin + "' WHERE id='" + Linha + "';", ligacao.connection);
                int result = command.ExecuteNonQuery();
                command.Dispose();
                BuscarNomes();
                //Console.WriteLine(">>>>>> " + result);
                return result > 0;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        internal bool CriarUsuario(string usuario, string senha, int admin)
        {
            try
            {
                var command = new MySqlCommand("INSERT INTO Login (user_id, password, admin) VALUES ('" + usuario + "','" + senha + "','" + admin + "')", ligacao.connection);
                int result = command.ExecuteNonQuery();
                BuscarNomes();
                return result >= 0;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        internal bool ElininarUsuario(string usuario, string senha)
        {
            try
            {
                var command = new MySqlCommand("DELETE FROM Login WHERE user_id='" + usuario + "' AND password='" + senha + "'", ligacao.connection);
                int result = command.ExecuteNonQuery();
                //Console.WriteLine(">>>>>>> " + result);
                return result > 0;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        internal DataTable BuscarTodosUsuarios()
        {
            try
            {
                const string query = "SELECT * FROM Login";
                var command = new MySqlCommand(query, ligacao.connection);
                MySqlDataAdapter sqlDataAdap = new MySqlDataAdapter(command);

                DataTable dtResult = new DataTable();

                sqlDataAdap.Fill(dtResult);

                sqlDataAdap.Dispose();
                command.Dispose();
                return dtResult;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        internal DataTable PesquisarUsuario(string usuario)
        {
            try
            {
                var command = new MySqlCommand("SELECT * FROM Login WHERE Login.user_id LIKE '%" + usuario + "%';", ligacao.connection);
                MySqlDataAdapter sqlDataAdap = new MySqlDataAdapter(command);

                DataTable dtResult = new DataTable();

                sqlDataAdap.Fill(dtResult);

                sqlDataAdap.Dispose();
                command.Dispose();
                return dtResult;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        private string IpMaquina()
        {
            // Busca o IP da maquina, mas intranet (rede interna)
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            return localIP;
        }

        private string IpInternet()
        {
            // Busca o IP da maquina, mas Internet (porta de saida do local)
            WebClient wc = new WebClient();
            string strIP = wc.DownloadString("http://checkip.dyndns.org");
            strIP = (new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")).Match(strIP).Value;
            wc.Dispose();
            return strIP;
        }
    }
}
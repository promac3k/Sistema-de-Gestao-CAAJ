using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace CAAJ.Utilitarios
{
    internal class DB
    {
        private string ServidorIP { get; set; }
        private string ServidorPorta { get; set; }
        private string Usuario { get; set; }
        private string senha { get; set; }
        private string tabela { get; set; }
        internal MySqlConnection connection { get; set; }

        internal DB()
        {
            // NOTA: Em ambiente de produção, estas credenciais devem estar num ficheiro de configuração
            // ou variáveis de ambiente, nunca hardcoded no código fonte!
            this.tabela = "database_name";
            this.ServidorIP = "your_server_host";
            this.ServidorPorta = "3306";
            this.Usuario = "your_username";
            this.senha = "your_password";
        }

        // ATENÇÂO as exceptions ,  é necessario usar try/catch em tudo que tenha ligações ao MySQL

        internal bool Ligado()
        {
            if (connection?.Ping() == true)
                return true;
            connection = new MySqlConnection("Server=" + ServidorIP + ";Port=" + this.ServidorPorta + ";User ID=" + Usuario + ";Password=" + senha + ";Database=" + this.tabela);
            connection.Open();
            return connection.Ping();
        }

        internal void Desligado()
        {
            connection?.Close();
        }
    }
}
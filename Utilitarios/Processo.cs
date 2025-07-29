using MySqlConnector;
using System;
using System.Data;

namespace CAAJ.Utilitarios
{
    internal class Processo
    {
        internal string EntidadeAEL { get; set; }
        internal string Estado { get; set; }
        internal string Estado_Liquidacao { get; set; }
        internal string Estado_Notificacao_RFL { get; set; }
        internal string Razao_Interdicao { get; set; }
        internal string AE_Liquidatario { get; set; }
        internal string Constar_Site_CAAJ { get; set; }
        internal string CpAe_Liquidatario { get; set; }
        internal int Cp_Ae { get; set; }
        internal string Data_Bloqueio { get; set; }
        internal string Data_Notificacao_RGL { get; set; }
        internal string MP_DIAP_Seccao { get; set; }
        internal string Nome_Ae { get; set; }
        internal string N_Processo_inquerito_DIAP { get; set; }
        internal string Observacoes_Importantes { get; set; }
        internal string PessoaResp_Proximas_Tarefas { get; set; }
        internal string Proximas_Tarefas { get; set; }
        internal string SaldoContas { get; set; }
        internal string Saldo_Apurado { get; set; }
        internal DB ligacao { get; private set; }

        internal Processo(DB ligacao)
        {
            this.ligacao = ligacao;
        }

        internal DataTable BuscarTodosProcessos()
        {
            try
            {
                const string query = "SELECT * FROM base_liquidacoes";
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

        internal DataTable PesquisarProcesso(int Cp_Ae)
        {
            try
            {
                var command = new MySqlCommand("SELECT * FROM base_liquidacoes WHERE base_liquidacoes.cp_ae=" + Cp_Ae + ";", ligacao.connection);
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

        internal bool CriarProcessos()
        {
            try
            {
                var command = new MySqlCommand("INSERT INTO base_liquidacoes (cp_ae, nome_ae, estado, razao_interdicao, data_bloqueio," +
                    "saldo_contas_cliente, entidade, cpae_liqui, ae_liqui," +
                    "estado_liqui, saldo_conclusao_liqui, oficio_rgi, estado_notificacao," +
                    "processos_crime_CDAJ, processo_inquerito, constar_site, obs_importante," +
                    "prox_tarefas, prox_resp_tarefas)" +
                    " VALUES ('" + Cp_Ae + "','" + Nome_Ae + "','" + Estado + "','" + Razao_Interdicao + "','" + Data_Bloqueio + "'," +
                    "'" + SaldoContas + "', '" + EntidadeAEL + "', '" + CpAe_Liquidatario + "', '" + AE_Liquidatario + "', '" + Estado_Liquidacao + "'," +
                    "'" + Saldo_Apurado + "','" + Data_Notificacao_RGL + "','" + Estado_Notificacao_RFL + "','" + MP_DIAP_Seccao + "'," +
                    "'" + N_Processo_inquerito_DIAP + "', '" + Constar_Site_CAAJ + "', '" + Observacoes_Importantes + "', '" + Proximas_Tarefas + "', '" + PessoaResp_Proximas_Tarefas + "')", ligacao.connection);
                int result = command.ExecuteNonQuery();

                return result >= 0;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        internal bool EliminarProcessos()
        {
            try
            {
                var command = new MySqlCommand("DELETE FROM base_liquidacoes WHERE cp_ae='" + Cp_Ae + "'", ligacao.connection);
                int result = command.ExecuteNonQuery();
                //Console.WriteLine(">>>>>>> " + result);
                return result > 0;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        internal bool AtualizarProcesso(int Linha)
        {
            try
            {
                // Mysql dateTime usa um formato de yyyy-MM-dd HH:mm:ss

                var command = new MySqlCommand("UPDATE base_liquidacoes SET cp_ae='" + Cp_Ae + "',nome_ae ='" + Nome_Ae + "', estado ='" + Estado + "', razao_interdicao='" + Razao_Interdicao + "'" +
                    ", data_bloqueio='" + Data_Bloqueio + "', saldo_contas_cliente='" + SaldoContas + "',entidade='" + EntidadeAEL + "',cpae_liqui='" + CpAe_Liquidatario + "',ae_liqui='" + AE_Liquidatario + "'" +
                    ",estado_liqui='" + Estado_Liquidacao + "',saldo_conclusao_liqui='" + Saldo_Apurado + "',oficio_rgi='" + Data_Notificacao_RGL + "',estado_notificacao='" + Estado_Notificacao_RFL + "'" +
                    ",processos_crime_CDAJ='" + MP_DIAP_Seccao + "',processo_inquerito='" + N_Processo_inquerito_DIAP + "',constar_site='" + Constar_Site_CAAJ + "',obs_importante='" + Observacoes_Importantes + "',prox_tarefas='" + Proximas_Tarefas + "'" +
                    ",prox_resp_tarefas='" + PessoaResp_Proximas_Tarefas + "' WHERE id='" + Linha + "';", ligacao.connection);
                int result = command.ExecuteNonQuery();
                command.Dispose();
                //Console.WriteLine(">>>>>> " + result);
                return result > 0;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
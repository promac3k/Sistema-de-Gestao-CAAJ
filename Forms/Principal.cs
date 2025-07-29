using CAAJ.Utilitarios;
using System;
using System.Windows.Forms;

namespace CAAJ
{
    public partial class MainForm : Form
    {
        internal DB ligacao;

        internal Processo Processo;
        internal Usuario Usuario;

        private int LinhaSelecionada { get; set; }

        public MainForm()
        {
            InitializeComponent();
            ligacao = new DB();
            if (!ligacao.Ligado())
            {
                Console.WriteLine("Ligação falhou!!!!!");
            };
            Usuario = new Usuario(ligacao);
            Processo = new Processo(ligacao);
            if (Program.usuarioAdmin == false)
                tabPrincipal.TabPages.Remove(tabAdmin);
        }

        private void Btn_TabelaUsuario_Click(object sender, EventArgs e)
        {
            //dgv_TabelaUsuario.DataSource = ligacao.BuscarTodosUsuarios();
            dgv_TabelaUsuario.DataSource = Usuario.BuscarTodosUsuarios();
        }

        private void Btn_NovoUsuario_Click(object sender, EventArgs e)

        {
            foreach (var name in Usuario.ListaUsuarios)
            {
                if (name.Value == txt_Usuario_Usuario.Text)
                {
                    MessageBox.Show("Usuario já existe, por favor use outro...");
                    return;
                }
            }
            if (VerificarCampos() == false)
            {
                return;
            }
            int admin = 0;

            if (chk_Usuario_ContaAdmin.Checked)
                admin = 1;
            if (Usuario.CriarUsuario(txt_Usuario_Usuario.Text, txt_Usuario_Senha.Text, admin))
            {
                MessageBox.Show("Usuario criado com sucesso.");
                Usuario.BuscarNomes();
                dgv_TabelaUsuario.DataSource = Usuario.BuscarTodosUsuarios();
            }
        }

        private void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos() == false)
            {
                return;
            }
            if (Usuario.ElininarUsuario(txt_Usuario_Usuario.Text, txt_Usuario_Senha.Text))
            {
                MessageBox.Show("usuario elininado com sucesso.");
                Usuario.BuscarNomes();
                dgv_TabelaUsuario.DataSource = Usuario.BuscarTodosUsuarios();
            }
            else
            {
                MessageBox.Show("usuario não foi elininado.");
            }
        }

        private bool VerificarCampos()
        {
            if (string.IsNullOrEmpty(txt_Usuario_Usuario.Text))
            {
                MessageBox.Show("campo usuario vazio!");
                return false;
            }
            if (string.IsNullOrEmpty(txt_Usuario_Senha.Text))
            {
                MessageBox.Show("campo senha vazio!");
                return false;
            }
            return true;
        }

        private void Btn_PesquisaUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Pesquisa.Text))
            {
                MessageBox.Show("campo usuario vazio!");
                return;
            }

            var result = Usuario.PesquisarUsuario(txt_Pesquisa.Text);

            dgv_TabelaUsuario.DataSource = result;
        }

        private void Btn_Atualizar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
            {
                foreach (var name in Usuario.ListaUsuarios)
                {
                    if (name.Value == txt_Usuario_Usuario.Text && LinhaSelecionada != name.Key )
                    {
                        MessageBox.Show("Usuario já existe, altere para outro.");
                        return;
                    }
                }
                int admin = 0;

                if (chk_Usuario_ContaAdmin.Checked)
                    admin = 1;

                if (Usuario.AtualizarUsuario(LinhaSelecionada, txt_Usuario_Usuario.Text, txt_Usuario_Senha.Text, admin))
                {
                    MessageBox.Show("Conta atualizado com sucesso.");
                    Usuario.BuscarNomes();
                    dgv_TabelaUsuario.DataSource = Usuario.BuscarTodosUsuarios();
                }
                else
                {
                    MessageBox.Show("Conta nao foi atualizada.");
                }
            }
        }

        private void dgv_TabelaUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verifica se foi seleccionado alguma celula!!
                if (dgv_TabelaUsuario.SelectedCells.Count > 0)
                {
                    // Primeiro Verifica que celula foi clicada
                    var linha = dgv_TabelaUsuario.SelectedCells[0].RowIndex;
                    // depois guarda dados dessa linha , inteira
                    DataGridViewRow LinhaSelecionada = dgv_TabelaUsuario.Rows[linha];

                    //´saca todos so dados dessa linha!
                    this.LinhaSelecionada = int.Parse(Convert.ToString(LinhaSelecionada.Cells["id"].Value));
                    Console.WriteLine("id :: -> " + this.LinhaSelecionada);

                    string usuario = Convert.ToString(LinhaSelecionada.Cells["user_id"].Value);
                    string senha = Convert.ToString(LinhaSelecionada.Cells["password"].Value);
                    string data = Convert.ToString(LinhaSelecionada.Cells["last_login"].Value);
                    string ip = Convert.ToString(LinhaSelecionada.Cells["last_ip"].Value);
                    int admin = int.Parse(Convert.ToString(LinhaSelecionada.Cells["admin"].Value));
                    // nossa Form
                    txt_Usuario_Usuario.Text = usuario;
                    txt_Usuario_Senha.Text = senha;
                    chk_Usuario_ContaAdmin.Checked = admin == 1;
                    txt_Usuario_IP.Text = ip;
                    txt_Usuario_Data.Text = data;
                }
                else
                {
                    Console.WriteLine("<<<<<<<<<< " + e.ToString());
                }
            }
            catch
            {
            }
        }

        private void Btn_Limpar_Click(object sender, EventArgs e)
        {
            // ComboBox
            cmb_EntidadeAEL.SelectedIndex = -1;
            cmb_estado.SelectedIndex = -1;
            cmb_Estado_Liquidacao.SelectedIndex = -1;
            cmb_Estado_Notificacao_RFL.SelectedIndex = -1;
            cmb_Razao_Interdicao.SelectedIndex = -1;

            // Textos
            txt_AE_Liquidatario.Text = "";
            txt_Constar_Site_CAAJ.Text = "";
            txt_CpAe_Liquidatario.Text = "";
            txt_Cp_Ae.Text = "";
            txt_Data_Bloqueio.Text = "";
            txt_Data_Notificacao_RGL.Text = "";
            txt_MP_DIAP_Seccao.Text = "";
            txt_Nome_Ae.Text = "";
            txt_N_Processo_inquerito_DIAP.Text = "";
            txt_Observacoes_Importantes.Text = "";
            txt_PessoaResp_Proximas_Tarefas.Text = "";
            txt_Proximas_Tarefas.Text = "";
            txt_SaldoContas.Text = "";
            txt_Saldo_Apurado.Text = "";
        }

        private void Atualizar_Form()
        {
            
            // ComboBox
            cmb_EntidadeAEL.Text = Processo.EntidadeAEL;
            cmb_estado.Text = Processo.Estado;
            cmb_Estado_Liquidacao.Text = Processo.Estado_Liquidacao;
            cmb_Estado_Notificacao_RFL.Text = Processo.Estado_Notificacao_RFL;
            cmb_Razao_Interdicao.Text = Processo.Razao_Interdicao;

            // Textos
            txt_AE_Liquidatario.Text = Processo.AE_Liquidatario;
            txt_Constar_Site_CAAJ.Text = Processo.Constar_Site_CAAJ;
            txt_CpAe_Liquidatario.Text = Processo.CpAe_Liquidatario;
            txt_Cp_Ae.Text = Processo.Cp_Ae.ToString();
            txt_Data_Bloqueio.Text = Processo.Data_Bloqueio;
            txt_Data_Notificacao_RGL.Text = Processo.Data_Notificacao_RGL;
            txt_MP_DIAP_Seccao.Text = Processo.MP_DIAP_Seccao;
            txt_Nome_Ae.Text = Processo.Nome_Ae;
            txt_N_Processo_inquerito_DIAP.Text = Processo.N_Processo_inquerito_DIAP;
            txt_Observacoes_Importantes.Text = Processo.Observacoes_Importantes;
            txt_PessoaResp_Proximas_Tarefas.Text = Processo.PessoaResp_Proximas_Tarefas;
            txt_Proximas_Tarefas.Text = Processo.Proximas_Tarefas;
            txt_SaldoContas.Text = Processo.SaldoContas;
            txt_Saldo_Apurado.Text = Processo.Saldo_Apurado;
        }

        private void Atualizar_Processo()
        {
            int AP=int.Parse(txt_Cp_Ae.Text);
            // ComboBox
            Processo.EntidadeAEL = cmb_EntidadeAEL.Text;
            Processo.EntidadeAEL = cmb_EntidadeAEL.Text;
            Processo.Estado = cmb_estado.Text;
            Processo.Estado_Liquidacao = cmb_Estado_Liquidacao.Text;
            Processo.Estado_Notificacao_RFL = cmb_Estado_Notificacao_RFL.Text;
            Processo.Razao_Interdicao = cmb_Razao_Interdicao.Text;

            // Textos
            Processo.AE_Liquidatario = txt_AE_Liquidatario.Text;
            Processo.Constar_Site_CAAJ = txt_Constar_Site_CAAJ.Text;
            Processo.CpAe_Liquidatario = txt_CpAe_Liquidatario.Text;
            Processo.Cp_Ae = AP;
            Processo.Data_Bloqueio = txt_Data_Bloqueio.Text;
            Processo.Data_Notificacao_RGL = txt_Data_Notificacao_RGL.Text;
            Processo.MP_DIAP_Seccao = txt_MP_DIAP_Seccao.Text;
            Processo.Nome_Ae = txt_Nome_Ae.Text;
            Processo.N_Processo_inquerito_DIAP = txt_N_Processo_inquerito_DIAP.Text;
            Processo.Observacoes_Importantes = txt_Observacoes_Importantes.Text;
            Processo.PessoaResp_Proximas_Tarefas = txt_PessoaResp_Proximas_Tarefas.Text;
            Processo.Proximas_Tarefas = txt_Proximas_Tarefas.Text;
            Processo.SaldoContas = txt_SaldoContas.Text;
            Processo.Saldo_Apurado = txt_Saldo_Apurado.Text;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void Btn_Ver_Tabela_Liquidacoes_Click(object sender, EventArgs e)
        {
            dgv_Tabela_Liquidacoes.DataSource = Processo.BuscarTodosProcessos();
        }

        private void Btn_Pesquisar_CpAe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Pesquisa_Liquidacao.Text))
            {
                MessageBox.Show("campo CP AE vazio!");
                return;
            }
            int AP=int.Parse(txt_Pesquisa_Liquidacao.Text);

            var result = Processo.PesquisarProcesso(AP);

            dgv_Tabela_Liquidacoes.DataSource = result;
        }

        private void Btn_Criar_Processos_Click(object sender, EventArgs e)
        {
            
            Atualizar_Processo();
            
            if (Processo.CriarProcessos())
            {
                MessageBox.Show("Processo criado com sucesso.");
               
                dgv_Tabela_Liquidacoes.DataSource = Processo.BuscarTodosProcessos();
            }
        }

        private void dgv_Tabela_Liquidacoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verifica se foi seleccionado alguma celula!!
                if (dgv_Tabela_Liquidacoes.SelectedCells.Count > 0)
                {
                    // Primeiro Verifica que celula foi clicada
                    var linha = dgv_Tabela_Liquidacoes.SelectedCells[0].RowIndex;
                    // depois guarda dados dessa linha , inteira
                    DataGridViewRow LinhaSelecionada = dgv_Tabela_Liquidacoes.Rows[linha];

                    //Â´saca todos so dados dessa linha!
                    this.LinhaSelecionada = int.Parse(Convert.ToString(LinhaSelecionada.Cells["id"].Value));
                    Console.WriteLine("id :: -> " + this.LinhaSelecionada);

                    int cp_ae = int.Parse(Convert.ToString(LinhaSelecionada.Cells["cp_ae"].Value));
                    string nome_ae = Convert.ToString(LinhaSelecionada.Cells["nome_ae"].Value);
                    string estado = Convert.ToString(LinhaSelecionada.Cells["estado"].Value);
                    string razao_interdicao = Convert.ToString(LinhaSelecionada.Cells["razao_interdicao"].Value);
                    string data_bloqueio = Convert.ToString(LinhaSelecionada.Cells["data_bloqueio"].Value);
                    string saldo_contas_cliente = Convert.ToString(LinhaSelecionada.Cells["saldo_contas_cliente"].Value);
                    string entidade = Convert.ToString(LinhaSelecionada.Cells["entidade"].Value);
                    string cpae_liqui = Convert.ToString(LinhaSelecionada.Cells["cpae_liqui"].Value);
                    string ae_liqui = Convert.ToString(LinhaSelecionada.Cells["ae_liqui"].Value);
                    string estado_liqui = Convert.ToString(LinhaSelecionada.Cells["estado_liqui"].Value);
                    string saldo_conclusao_liqui = Convert.ToString(LinhaSelecionada.Cells["saldo_conclusao_liqui"].Value);
                    string oficio_rgi = Convert.ToString(LinhaSelecionada.Cells["oficio_rgi"].Value);
                    string estado_notificacao = Convert.ToString(LinhaSelecionada.Cells["estado_notificacao"].Value);
                    string processos_crime_CDAJ = Convert.ToString(LinhaSelecionada.Cells["processos_crime_CDAJ"].Value);
                    string processo_inquerito = Convert.ToString(LinhaSelecionada.Cells["processo_inquerito"].Value);
                    string constar_site = Convert.ToString(LinhaSelecionada.Cells["constar_site"].Value);
                    string obs_importante = Convert.ToString(LinhaSelecionada.Cells["obs_importante"].Value);
                    string prox_tarefas = Convert.ToString(LinhaSelecionada.Cells["prox_tarefas"].Value);
                    string prox_resp_tarefas = Convert.ToString(LinhaSelecionada.Cells["prox_resp_tarefas"].Value);


                    // nossa Form
                    txt_Cp_Ae.Text = cp_ae.ToString();
                    txt_Nome_Ae.Text = nome_ae;

                    cmb_estado.Text = estado;
                    cmb_Razao_Interdicao.Text = razao_interdicao;

                    txt_Data_Bloqueio.Text = data_bloqueio;
                    txt_SaldoContas.Text = saldo_contas_cliente;

                    cmb_EntidadeAEL.Text = entidade;

                    txt_CpAe_Liquidatario.Text = cpae_liqui;
                    txt_AE_Liquidatario.Text = ae_liqui;

                    cmb_Estado_Liquidacao.Text = estado_liqui;

                    txt_Saldo_Apurado.Text = saldo_conclusao_liqui;
                    txt_Data_Notificacao_RGL.Text = oficio_rgi;

                    cmb_Estado_Notificacao_RFL.Text = estado_notificacao;

                    txt_MP_DIAP_Seccao.Text = processos_crime_CDAJ;
                    txt_N_Processo_inquerito_DIAP.Text = processo_inquerito;
                    txt_Constar_Site_CAAJ.Text = constar_site;
                    txt_Observacoes_Importantes.Text = obs_importante;
                    txt_Proximas_Tarefas.Text = prox_tarefas;
                    txt_PessoaResp_Proximas_Tarefas.Text = prox_resp_tarefas;


                }
                else
                {
                    Console.WriteLine("<<<<<<<<<< " + e.ToString());
                }
            }
            catch
            {
            }
        }

        private void Btn_Eliminar_Processos_Click(object sender, EventArgs e)
        {
            Atualizar_Processo();

            if (Processo.EliminarProcessos())
            {
                MessageBox.Show("Processo eliminado com sucesso.");
               dgv_Tabela_Liquidacoes.DataSource = Processo.BuscarTodosProcessos();
            }
            else
            {
                MessageBox.Show("Processo não foi eliminado.");
            }
        }

        private void Btn_Atualizar_Processos_Click(object sender, EventArgs e)
        {
            Atualizar_Processo();

            if (Processo.AtualizarProcesso(LinhaSelecionada))
            {
                MessageBox.Show("Conta atualizado com sucesso.");
            
                dgv_Tabela_Liquidacoes.DataSource = Processo.BuscarTodosProcessos();
            }
            else
            {
                MessageBox.Show("Conta nao foi atualizada.");
            }
        }
    }

}
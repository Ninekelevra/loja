using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Loja.DTO;
using Loja.BLL;


namespace loja
{
    public partial class Cadastro_usuario : Form
    {
        string modo = "";
        int codUsuSelecionado = -1;
        public Cadastro_usuario()
        {
            InitializeComponent();
        }
        private void Cadastro_usuario_Load(object sender, EventArgs e)
        {
            carregarGrid();
            lblMensagem.Text = "";
        }
        private void carregarGrid()
        { 
            try
            {
                IList<usuario_DTO> listUsuario_DTO = new List<usuario_DTO>();
                listUsuario_DTO = new UsuarioBLL().cargaUsuario();

                dataGridView1.DataSource = listUsuario_DTO;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sel = dataGridView1.CurrentRow.Index;

            txtNome.Text = Convert.ToString(dataGridView1["nome", sel].Value);
            txtLogin.Text = Convert.ToString(dataGridView1["login", sel].Value);
            txtEmail.Text = Convert.ToString(dataGridView1["email", sel].Value);
            txtSenha.Text = Convert.ToString(dataGridView1["senha", sel].Value);
            txtCadastro.Text = Convert.ToString(dataGridView1["cadastro", sel].Value);
            codUsuSelecionado = Convert.ToInt32(dataGridView1["cod_usuario", sel].Value);

            if (Convert.ToString(dataGridView1["situacao", sel].Value) == "A")
            {
                cboSituacao.Text = "Ativo";
            }
            else
            {
                cboSituacao.Text = "Inativo";
            }
            switch(Convert.ToString(dataGridView1["perfil", sel].Value))
            {
                case "1":
                    cboPerfil.Text = "Administrador";
                    break;
                case "2":
                    cboPerfil.Text = "Operador";
                    break;
                case "3":
                    cboPerfil.Text = "Gerencial";
                    break;

            }
            modo = "";
            lblMensagem.Text = "";
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            limpar_campos();

            txtCadastro.Text = Convert.ToString(System.DateTime.Now);

            modo = "Novo";
            lblMensagem.Text = "";
        }
        private void limpar_campos()
        {
            txtNome.Text = "";
            txtLogin.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            txtCadastro.Text = "";
            cboPerfil.Text = "";
            cboSituacao.Text = "";
        }

       

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (codUsuSelecionado < 0)
            {
                lblMensagem.Text = "Selecione um usuario antes de prosseguir";
                return;
            }
            else
            {
                lblMensagem.Text = "";
                modo = "Alterar";
                MessageBox.Show("Para concluir a alteração, clique em Confirmar");
            }
        }

        private void BtnDeletar_Click(object sender, EventArgs e)
        {
            if (codUsuSelecionado < 0)
            {
                lblMensagem.Text = "Selecione um usuario antes de prosseguir";
                return;
            }
            else
            {
                lblMensagem.Text = "";
                modo = "Excluir";
                MessageBox.Show("Para excluir o usuario, clique em Confirmar");
            }

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            modo = "";
            lblMensagem.Text = "";
            codUsuSelecionado = -1;
            limpar_campos();

        }
        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            if (modo == "Novo")
            {
                try
                {
                    usuario_DTO USU = new usuario_DTO();
                    USU.nome = txtNome.Text;
                    USU.login = txtLogin.Text;
                    USU.email = txtEmail.Text;
                    USU.senha = txtSenha.Text;
                    USU.cadastro = System.DateTime.Now;
                    if (cboSituacao.Text == "Ativo")
                    {
                        USU.situacao = "A";
                    }
                    else
                    {
                        USU.situacao = "I";
                    }
                    switch (cboPerfil.Text)
                    {
                        case "Administrador":
                            USU.perfil = 1;
                            break;
                        case "Operador":
                            USU.perfil = 2;
                            break;
                        case "Gerencial":
                            USU.perfil = 3;
                            break;
                    }
                    int x = new UsuarioBLL().insereUsuario(USU);
                    if (x > 0)
                    {
                        MessageBox.Show("Gravado com Suceso!");
                    }
                    carregarGrid();
                    limpar_campos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado!" + ex.Message);
                }
            }
            if (modo == "Alterar")
            {
                /*Tratamento de Erros, exibe msg*/
                try
                {
                    if (codUsuSelecionado < 0)
                    {
                        lblMensagem.Text = "Selecione um usuario antes de prosseguir";
                        return;
                    }
                    /*Objeto USU, assim como feito no modo="novo"
                    Lê os textbox com os dados alterados*/
                    usuario_DTO USU = new usuario_DTO();
                    USU.cod_usuario = codUsuSelecionado;
                    USU.nome = txtNome.Text;
                    USU.login = txtLogin.Text;
                    USU.email = txtEmail.Text;
                    USU.cadastro = System.DateTime.Now;
                    USU.senha = txtSenha.Text;
                    if (cboSituacao.Text == "Ativo")
                    {
                        USU.situacao = "A";
                    }
                    else
                    {
                        USU.situacao = "I";
                    }
                    switch (cboPerfil.Text)
                    {
                        case "Administrador":
                            USU.perfil = 1;
                            break;
                        case "Operador":
                            USU.perfil = 2;
                            break;
                        case "Gerencial":
                            USU.perfil = 3;
                            break;
                    }
                    int x = new UsuarioBLL().alteraUsuario(USU);
                    /*Verifica se houve alguma gravação*/
                    if (x > 0)
                    {
                        MessageBox.Show("Alterado com Sucesso!");
                    }
                    /*Recarrega o Grid após os dados serem gravados*/
                    carregarGrid();
                    limpar_campos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado" + ex.Message);
                }
            }
            if (modo == "Excluir")
            {
                try
                {
                    if (codUsuSelecionado < 0)
                    {
                        lblMensagem.Text = "Selecione um usuario antes de prosseguir";
                        return;
                    }
                    usuario_DTO USU = new usuario_DTO();
                    USU.cod_usuario = codUsuSelecionado;
                    int x = new UsuarioBLL().excluiUsuario(USU);
                    if (x > 0)
                    {
                        MessageBox.Show("Excluido com Sucesso!");
                    }
                    /*Recarrega o Grid após os dados serem gravados*/
                    carregarGrid();
                    limpar_campos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado" + ex.Message);
                }
            }

            modo = "";
            lblMensagem.Text = "";

        }

    }

}

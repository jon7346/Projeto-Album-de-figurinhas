using Album_copa_do_mundo.Controllers;
using Album_copa_do_mundo.Models;
using Album_copa_do_mundo.Services;

namespace Album_copa_do_mundo.Views;

public partial class PgCadastroFigurinhas : ContentPage
{
    FigurinhaController _controller;
    string _imgSelecionada = "";

    public PgCadastroFigurinhas()
    {
        InitializeComponent();

        _controller = new FigurinhaController();
    }

    private void OnVoltarClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PopAsync();
    }

    private async void OnSelecionarFotoClicked(object sender, EventArgs e)
    {
        _imgSelecionada = await ImageService.SelecionarImagem();

        // Verifica se o usuário realmente selecionou algo
        if (!string.IsNullOrEmpty(_imgSelecionada))
        {
            imgPreview.Source = _imgSelecionada;
            imgPreview.IsVisible = true;
            btnRemoverImagem.IsVisible = true;
        }
    }

    private void OnRemoverFotoClicked(object sender, EventArgs e)
    {
        RemoverImagem();
    }

    private async void OnSalvarRegistroClicked(object sender, EventArgs e)
    {
        if (!CamposEstaoPreenchidos())
        {
            await DisplayAlert("Atenção", "Por favor, preencha todos os campos.", "Ok");
            return;
        }

        Figurinha figurinha = new Figurinha();
        figurinha.NomeJogador = txtNomeJogador.Text.Trim();
        figurinha.Selecao = txtSelecao.Text.Trim();
        figurinha.Tipo = pickerTipoFigurinha.SelectedItem?.ToString() ?? "";
        figurinha.DirImagem = ImageService.CopiarImagem(_imgSelecionada);
        figurinha.Obtido = switchObtido.IsToggled;
        figurinha.Desejado = switchDesejado.IsToggled;

        // Tenta inserir no banco
        if (_controller.Insert(figurinha))
        {
            // Usamos await aqui para realmente esperar o usuário clicar em "Ok"
            // na mensagem e só depois limpar os campos, pois da maneira que estava
            // (limpando tudo e exibindo a mensagem ao mesmo tempo) estava estranho visualmente
            await DisplayAlert("Informação", "Cadastro realizado com sucesso!", "Ok");
            LimparCampos();
        }
        else
        {
            await DisplayAlert("Erro", "Ocorreu uma falha ao salvar o cadastro da figurinha.", "Ok");
        }
    }

    private void RemoverImagem()
    {
        imgPreview.Source = "";
        _imgSelecionada = "";

        imgPreview.IsVisible = false;
        btnRemoverImagem.IsVisible = false;
    }

    private bool CamposEstaoPreenchidos()
    {
        return !string.IsNullOrWhiteSpace(txtNomeJogador.Text) &&
               !string.IsNullOrWhiteSpace(txtSelecao.Text) &&
               !string.IsNullOrWhiteSpace(pickerTipoFigurinha.SelectedItem?.ToString()) &&
               !string.IsNullOrWhiteSpace(_imgSelecionada);
    }

    private void LimparCampos()
    {
        txtNomeJogador.Text = "";
        txtSelecao.Text = "";
        pickerTipoFigurinha.SelectedIndex = -1;
        switchObtido.IsToggled = false;
        switchDesejado.IsToggled = false;
        RemoverImagem();
    }
}
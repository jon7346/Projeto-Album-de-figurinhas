using Album_copa_do_mundo.Controllers;
using Album_copa_do_mundo.Models;

namespace Album_copa_do_mundo.Views;

public partial class PgListagemFigurinhas : ContentPage
{
    FigurinhaController _controller;

    public PgListagemFigurinhas()
    {
        InitializeComponent();

        _controller = new FigurinhaController();

        // Seleciona a opção "todos" por padrão nos filtros
        pickerFiltroObtidas.SelectedIndex = 2;
        pickerFiltroDesejadas.SelectedIndex = 2;

        // Ao abrir a tela, já carrega a lista de figurinhas
        // com todos os registros cadastrados
        AtualizarListaRegistros();
    }

    private void OnVoltarClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PopAsync();
    }

    private void OnBuscarClicked(object sender, EventArgs e)
    {
        AtualizarListaRegistros();
    }

    private void OnObtidoClicked(object sender, EventArgs e)
    {
        if (sender is Button botao &&
            botao.CommandParameter is Figurinha figurinha)
        {
            // Alterna o status da propriedade "obtido"
            figurinha.Obtido = !figurinha.Obtido;

            // Tenta salvar no banco
            if (_controller.Update(figurinha))
            {
                // Se conseguiu salvar, atualiza a lista para refletir a mudança
                AtualizarListaRegistros();
            }
            else
            {
                DisplayAlert("Erro", "Falha ao alterar status de 'obtido' da figurinha", "Ok");
            }
        }
    }

    private void OnDesejadoClicked(object sender, EventArgs e)
    {
        if (sender is Button botao &&
            botao.CommandParameter is Figurinha figurinha)
        {
            // Alterna o status da propriedade "desejado"
            figurinha.Desejado = !figurinha.Desejado;

            // Tenta salvar no banco
            if (_controller.Update(figurinha))
            {
                // Se conseguiu salvar, atualiza a lista para refletir a mudança
                AtualizarListaRegistros();
            }
            else
            {
                DisplayAlert("Erro", "Falha ao alterar status de 'desejado' da figurinha", "Ok");
            }
        }
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        // Verifica se o sender é um botão e se o parâmetro é o objeto
        // de figurinha, pois se não for já podemos parar a execução aqui
        if (sender is not Button botao ||
            botao.CommandParameter is not Figurinha figurinha)
        {
            return;
        }

        bool podeExcluir = await DisplayAlert(
            "Confirmação",
            "Tem certeza que deseja excluir esta figurinha?",
            "Sim",
            "Não");

        if (podeExcluir)
        {
            // Tenta excluir
            if (_controller.Delete(figurinha))
            {
                // Se conseguiu excluir, atualiza a lista para refletir a mudança
                AtualizarListaRegistros();
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao excluir figurinha.", "Ok");
            }
        }
    }

    private void AtualizarListaRegistros()
    {
        var nomeJogador = txtFiltroNomeJogador.Text?.Trim() ?? "";
        var obtidas = ObterValorFiltroObtidas();
        var desejadas = ObterValorFiltroDesejadas();

        var figurinhas = _controller.GetByFilters(nomeJogador, obtidas, desejadas);
        lsvFigurinhas.ItemsSource = figurinhas;

        // Exibe o texto de "nenhuma figurinha encontrada"
        // se a lista retornada for vazia
        txtNenhumaFigurinha.IsVisible = !figurinhas.Any();

        // Esconde a lista (para não ocupar espaço) se a lista
        // for vazia
        lsvFigurinhas.IsVisible = figurinhas.Any();
    }

    private bool? ObterValorFiltroObtidas()
    {
        switch (pickerFiltroObtidas.SelectedIndex)
        {
            case 0: // Somente "obtidas"
                return true;
            case 1: // Somente "não obtidas"
                return false;
            default: // Todos
                return null;
        }
    }

    private bool? ObterValorFiltroDesejadas()
    {
        switch (pickerFiltroDesejadas.SelectedIndex)
        {
            case 0: // Somente "desejadas"
                return true;
            case 1: // Somente "não desejadas"
                return false;
            default: // Todos
                return null;
        }
    }

    // Atualiza automaticamente a lista ao mudar o valor dos filtros
    // de "obtidas" ou "desejadas" para que o usuário não precise ficar
    // clicando no botão "Buscar" toda hora
    private void OnFiltroObtidasChanged(object sender, EventArgs e)
    {
        AtualizarListaRegistros();
    }

    private void OnFiltroDesejadasChanged(object sender, EventArgs e)
    {
        AtualizarListaRegistros();
    }
}
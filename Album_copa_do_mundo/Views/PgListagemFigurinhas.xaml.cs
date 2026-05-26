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

        // Ao abrir a tela, já carrega a lista de figurinhas
        // sem filtro de nome mas incluindo figurinhas obtidas e desejadas
        AtualizarListaRegistros(
            nomeJogador: "",
            somenteObtidas: switchObtidas.IsToggled,
            somenteDesejadas: switchDesejadas.IsToggled
            );
    }

    private void OnVoltarClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PopAsync();
    }

    private void OnBuscarClicked(object sender, EventArgs e)
    {
        AtualizarListaRegistros(
            nomeJogador: txtFiltroNomeJogador.Text?.Trim() ?? "",
            somenteObtidas: switchObtidas.IsToggled,
            somenteDesejadas: switchDesejadas.IsToggled
            );
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
                AtualizarListaRegistros(
                    nomeJogador: txtFiltroNomeJogador.Text?.Trim() ?? "",
                    somenteObtidas: switchObtidas.IsToggled,
                    somenteDesejadas: switchDesejadas.IsToggled
                    );
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
                AtualizarListaRegistros(
                    nomeJogador: txtFiltroNomeJogador.Text?.Trim() ?? "",
                    somenteObtidas: switchObtidas.IsToggled,
                    somenteDesejadas: switchDesejadas.IsToggled
                    );
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
                AtualizarListaRegistros(
                    nomeJogador: txtFiltroNomeJogador.Text?.Trim() ?? "",
                    somenteObtidas: switchObtidas.IsToggled,
                    somenteDesejadas: switchDesejadas.IsToggled
                    );
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao excluir figurinha.", "Ok");
            }
        }
    }

    private void AtualizarListaRegistros(
        string nomeJogador,
        bool somenteObtidas,
        bool somenteDesejadas
        )
    {
        var figurinhas = _controller.GetByFilters(nomeJogador, somenteObtidas, somenteDesejadas);
        lsvFigurinhas.ItemsSource = figurinhas;

        // Exibe o texto de "nenhuma figurinha encontrada"
        // se a lista retornada for vazia
        txtNenhumaFigurinha.IsVisible = !figurinhas.Any();

        // Esconde a lista (para não ocupar espaço) se a lista
        // for vazia
        lsvFigurinhas.IsVisible = figurinhas.Any();
    }
}
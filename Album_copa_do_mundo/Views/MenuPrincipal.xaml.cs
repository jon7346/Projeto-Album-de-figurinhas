namespace Album_copa_do_mundo.Views;

public partial class MenuPrincipal : ContentPage
{
    public MenuPrincipal()
    {
        InitializeComponent();
    }

    private void OnCadastroFigurinhasClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PgCadastroFigurinhas());
    }

    private void OnListagemFigurinhasClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PgListagemFigurinhas());
    }
}
namespace Album_copa_do_mundo.Views;

public partial class MenuPrincipal : ContentPage
{
	public MenuPrincipal()
	{
		InitializeComponent();
	}

    private void OnCadastroClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PgCadastro());
    }

    private void OnListaClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ListaFigurinhas());
    }
}
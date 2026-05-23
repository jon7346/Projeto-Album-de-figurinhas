namespace App_Copa.Views;

public partial class MenuPrincipal : ContentView
{
	public MenuPrincipal()
	{
		InitializeComponent();
	}

    private void OnCadastroClicked(object sender, EventArgs e)
    {
        Application.Current.PgCadastro.Navigation.PopAsync();
    }
}
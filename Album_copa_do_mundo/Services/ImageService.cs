namespace Album_copa_do_mundo.Services
{
    public static class ImageService
    {
        public static async Task<string> SelecionarImagem()
        {
            string diretorio = "";

            var imgSelecionada = await MediaPicker.PickPhotoAsync();

            if (imgSelecionada != null)
                diretorio = imgSelecionada.FullPath;

            return diretorio;
        }

        public static string CopiarImagem(string dirOriginal)
        {
            string dirDestino = "";

            if (!string.IsNullOrEmpty(dirOriginal))
            {
                var dirPasta = Path.Combine(AppContext.BaseDirectory, "Imagens");

                if (!Directory.Exists(dirPasta))
                    Directory.CreateDirectory(dirPasta);

                string nomeOriginal = Path.GetFileName(dirOriginal);
                dirDestino = Path.Combine(dirPasta, nomeOriginal);

                File.Copy(dirOriginal, dirDestino, overwrite: true);
            }

            return dirDestino;
        }
    }
}

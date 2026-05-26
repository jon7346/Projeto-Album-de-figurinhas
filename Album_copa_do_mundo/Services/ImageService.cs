namespace Album_copa_do_mundo.Services
{
    public static class ImageService
    {
        public static async Task<string> SelecionarImagem()
        {
            string diretorioImg = "";

            var imgSelecionada = await MediaPicker.PickPhotoAsync();

            if (imgSelecionada != null)
                diretorioImg = imgSelecionada.FullPath;

            return diretorioImg;
        }

        public static string CopiarImagem(string dirOriginal)
        {
            string dirDestino = "";

            if (!string.IsNullOrEmpty(dirOriginal))
            {
                var dirPastaImagens = Path.Combine(AppContext.BaseDirectory, "Imagens");

                if (!Directory.Exists(dirPastaImagens))
                    Directory.CreateDirectory(dirPastaImagens);

                string nomeOriginal = Path.GetFileName(dirOriginal);
                dirDestino = Path.Combine(dirPastaImagens, nomeOriginal);

                File.Copy(dirOriginal, dirDestino, overwrite: true);
            }

            return dirDestino;
        }
    }
}

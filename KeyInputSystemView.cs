namespace FineGameDesign.Utils
{
    public sealed class KeyInputSystemView : ASingletonView<KeyInputSystem>
    {
        private void Update()
        {
            controller.Update();
        }
    }
}

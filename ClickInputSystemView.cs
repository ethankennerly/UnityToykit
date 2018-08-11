namespace FineGameDesign.Utils
{
    public sealed class ClickInputSystemView : ASingletonView<ClickInputSystem>
    {
        private void Update()
        {
            controller.Update();
        }
    }
}

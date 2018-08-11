namespace FineGameDesign.Utils
{
    public sealed class ClickSystemView : ASingletonView<ClickSystem>
    {
        private void Update()
        {
            controller.Update();
        }
    }
}

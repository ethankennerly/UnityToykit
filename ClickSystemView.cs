namespace Finegamedesign.Utils
{
    public sealed class ClickSystemView : ASingletonView<ClickSystem>
    {
        private void Update()
        {
            Controller.Update();
        }
    }
}

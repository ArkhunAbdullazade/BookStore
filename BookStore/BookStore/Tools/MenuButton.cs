using System.Windows.Controls;
using System.Windows;

namespace BookStore.Tools
{
    public class MenuButton : RadioButton
    {
        static MenuButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuButton), new FrameworkPropertyMetadata(typeof(MenuButton)));
        }
    }
}

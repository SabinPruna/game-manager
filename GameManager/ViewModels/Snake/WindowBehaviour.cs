using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using GameManager.ViewModels.Snake;
namespace GameManager.ViewModels.Snake
{
    public class WindowBehaviour : Behavior<Window>
    {
        SnakeViewModel vm;
        protected override void OnAttached()
        {
            Window window = this.AssociatedObject;
            if (window != null)
            {
                window.PreviewKeyDown += Window_PreviewKeyDown;
                vm = (SnakeViewModel)window.DataContext;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;

            switch (e.Key)
            {
                case Key.Down:
                    if (vm.Snake.Head.Direction != GameManager.Models.Direction.Up)
                    {
                        vm.Snake.CurrentDirection = GameManager.Models.Direction.Down;
                    }
                    break;
                case Key.Up:
                    if (vm.Snake.Head.Direction != GameManager.Models.Direction.Down)
                    {
                        vm.Snake.CurrentDirection = GameManager.Models.Direction.Up;
                    }
                    break;
                case Key.Left:
                    if (vm.Snake.Head.Direction != GameManager.Models.Direction.Right)
                    {
                        vm.Snake.CurrentDirection = GameManager.Models.Direction.Left;
                    }
                    break;
                case Key.Right:
                    if (vm.Snake.Head.Direction != GameManager.Models.Direction.Left)
                    {
                        vm.Snake.CurrentDirection = GameManager.Models.Direction.Right;
                    }
                    break;
                case Key.Space:
                    if (vm.Run)
                    {
                        vm.Move(vm.Snake.CurrentDirection);
                    }
                    break;
                case Key.Escape:
                    window.Close();
                    break;
            }
        }
    }
}

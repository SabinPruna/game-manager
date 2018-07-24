using System.Windows.Input;
using GameManager.BussinessLayer;
using GameManager.Commands;

namespace GameManager.ViewModels.Money
{
    public class MoneyViewModel : BaseViewModel
    {
        private string _money;
        private readonly PlayerManager _playerManager;


        public MoneyViewModel()
        {
            _playerManager = new PlayerManager();

            AddMoneyCommand = new RelayCommand(param =>
            {
                _playerManager.AddMoney(App.CurrentApp.MainViewModel.LoginViewModel.Player.Id ,int.Parse(Money));
                App.CurrentApp.MainViewModel.Refresh();
            });
        }


        #region  Properties

        public string Money
        {
            get => null ==_money ? _money : _money.Substring(38);
            set => SetProperty(ref _money, value);
        }

        public ICommand AddMoneyCommand { get; }

        #endregion
    }
}
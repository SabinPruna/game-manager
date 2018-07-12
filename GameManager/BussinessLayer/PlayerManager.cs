using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManager.DataAccessLayer;
using GameManager.Models;

namespace GameManager.BussinessLayer
{
    public class PlayerManager
    {
        private PlayerRepository _playerRepository;

        public PlayerManager()
        {
            _playerRepository = new PlayerRepository();
        }

        public Player Login(Player player)
        {
            return player != null ? _playerRepository.Login(player) : null;
        }

        public void Register(Player player)
        {
            _playerRepository.Register(player);
        }
    }
}

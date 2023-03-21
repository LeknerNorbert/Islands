using BLL.Exceptions;
using DAL.DTOs;
using DAL.Models;
using DAL.Models.Enums;
using DAL.Repositories.ExchangeRepository;
using DAL.Repositories.PlayerRepository;

namespace BLL.Services.ExchangeService
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IPlayerRepository _playerRepository;

        public ExchangeService(
            IExchangeRepository exchangeRepository, 
            IPlayerRepository playerRepository)
        {
            _exchangeRepository = exchangeRepository;
            _playerRepository = playerRepository;
        }

        public async Task AddExchangeAsync(string username, CreateExchangeDto exchange)
        {
            Player advertiser = await _playerRepository.GetPlayerByUsernameAsync(username);

            switch (exchange.Item)
            {
                case Item.Coin:
                    if (advertiser.Coins >= exchange.Amount)
                    {
                        advertiser.Coins -= exchange.Amount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
                case Item.Wood:
                    if (advertiser.Woods >= exchange.Amount)
                    {
                        advertiser.Woods -= exchange.Amount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
                case Item.Stone:
                    if (advertiser.Stones >= exchange.Amount)
                    {
                        advertiser.Stones -= exchange.Amount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
                case Item.Iron:
                    if (advertiser.Irons >= exchange.Amount)
                    {
                        advertiser.Irons -= exchange.Amount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
            }

            Exchange createdExchange = new()
            {
                Item = exchange.Item,
                Amount = exchange.Amount,
                ReplacementItem = exchange.ReplacementItem,
                ReplacementAmount = exchange.ReplacementAmount,
                PublishDate = DateTime.Now,
                Player = advertiser
            };

            await _exchangeRepository.AddExchangeAsync(createdExchange);
            await _playerRepository.UpdatePlayerAsync(advertiser);
        }

        public async Task BuyExchangeAsync(string username, int id)
        {
            Exchange exchange = await _exchangeRepository.GetExchangeByIdAsync(id);
            Player buyer = await _playerRepository.GetPlayerByUsernameAsync(username);
            Player advertiser = await _playerRepository.GetPlayerByForAdAsync(id);

            switch (exchange.ReplacementItem)
            {
                case Item.Coin:
                    if (buyer.Coins >= exchange.ReplacementAmount)
                    {
                        buyer.Coins -= exchange.ReplacementAmount;
                        advertiser.Coins += exchange.ReplacementAmount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
                case Item.Wood:
                    if (buyer.Woods >= exchange.ReplacementAmount)
                    {
                        buyer.Woods -= exchange.ReplacementAmount;
                        advertiser.Woods += exchange.ReplacementAmount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
                case Item.Stone:
                    if (buyer.Stones >= exchange.ReplacementAmount)
                    {
                        buyer.Stones -= exchange.ReplacementAmount;
                        advertiser.Stones += exchange.ReplacementAmount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
                case Item.Iron:
                    if (buyer.Irons >= exchange.ReplacementAmount)
                    {
                        buyer.Irons -= exchange.ReplacementAmount;
                        advertiser.Irons += exchange.ReplacementAmount;
                    }
                    else
                    {
                        throw new NotEnoughItemsException("Not enough items.");
                    }

                    break;
            }

            switch (exchange.Item)
            {
                case Item.Coin:
                    buyer.Coins += exchange.Amount;

                    break;
                case Item.Wood:
                    buyer.Woods += exchange.Amount;

                    break;
                case Item.Stone:
                    buyer.Stones += exchange.Amount;

                    break;
                case Item.Iron:
                    buyer.Irons += exchange.Amount;

                    break;
            }

            await _playerRepository.UpdatePlayerAsync(buyer);
            await _playerRepository.UpdatePlayerAsync(advertiser);
            await _exchangeRepository.RemoveExchangeAsync(exchange);
        }

        public async Task<List<ExchangeDto>> GetAllExchangesAsync(string username)
        {
            return await _exchangeRepository.GetAllExchangesAsync(username);
        }

        public async Task<List<ExchangeDto>> GetAllExchangesByUsernameAsync(string username)
        {
            return await _exchangeRepository.GetAllExchangesByUsernameAsync(username);
        }

        public async Task RemoveExchangeAsync(int id)
        {
            Exchange exchange = await _exchangeRepository.GetExchangeByIdAsync(id);
            await _exchangeRepository.RemoveExchangeAsync(exchange);
        }
    }
}
